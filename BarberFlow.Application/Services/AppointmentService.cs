using BarberFlow.Application.DTOs.Appointment;
using BarberFlow.Application.DTOs.BlockedTime;
using BarberFlow.Application.Interfaces;
using BarberFlow.Domain.Entities;
using BarberFlow.Domain.Enums;
using BarberFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberFlow.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly AppDbContext _context;
        const int SLOT_DURATION = 30;

        public AppointmentService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateAsync(Guid userId, CreateAppointmentDto dto)
        {
            const int SLOT_DURATION = 30;

            var client = await _context.Clients
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (client == null)
                return false;

            var service = await _context.Services
                .FirstOrDefaultAsync(x => x.Id == dto.ServiceId && x.IsActive);

            if (service == null)
                return false;

            //  valida tempo
            var now = DateTime.Now;
            now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);

            if (dto.StartTime < now)
                return false;

            if (dto.StartTime.Minute % SLOT_DURATION != 0 || dto.StartTime.Second != 0)
                return false;

            dto.StartTime = new DateTime(
                dto.StartTime.Year,
                dto.StartTime.Month,
                dto.StartTime.Day,
                dto.StartTime.Hour,
                dto.StartTime.Minute,
                0
            );

            var requiredSlots = service.SlotCount;

            //  calcula UMA vez
            var endTime = dto.StartTime.AddMinutes(requiredSlots * SLOT_DURATION);

            //  CONFLICTO COM APPOINTMENTS
            var appointments = await _context.Appointments
                .Where(a => a.ProfessionalId == dto.ProfessionalId)
                .ToListAsync();

            var hasConflict = appointments.Any(a =>
                dto.StartTime < a.EndTime &&
                endTime > a.StartTime
            );

            if (hasConflict)
                return false;

            //  CONFLICTO COM BLOCKEDTIME
            var blockedTimes = await _context.BlockedTimes
                .Where(b => b.ProfessionalId == dto.ProfessionalId)
                .ToListAsync();

            var hasBlockedConflict = blockedTimes.Any(b =>
                dto.StartTime < b.End &&
                endTime > b.Start
            );

            if (hasBlockedConflict)
                return false;

            //  cria agendamento
            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                ClientId = client.Id,
                ProfessionalId = dto.ProfessionalId,
                ServiceId = dto.ServiceId,
                StartTime = dto.StartTime,
                EndTime = endTime,
                Status = AppointmentStatus.Scheduled,
                CreatedAt = DateTime.UtcNow
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<AvailableSlotDto>> GetAvailableSlotsAsync(Guid professionalId, DateTime date)
        {
            const int SLOT_DURATION = 30;

            var startDay = date.Date.AddHours(9);   // abertura
            var endDay = date.Date.AddHours(18);    // fechamento

            // gerar todos os slots do dia
            var allSlots = new List<DateTime>();

            for (var time = startDay; time < endDay; time = time.AddMinutes(SLOT_DURATION))
            {
                allSlots.Add(time);
            }

            // pegar agendamentos do dia
            var appointments = await _context.Appointments
                .Where(x =>
                    x.ProfessionalId == professionalId &&
                    x.Status == AppointmentStatus.Scheduled &&
                    x.StartTime.Date == date.Date
                )
                .ToListAsync();

            // pegar slots ocupados
            var occupiedSlots = new List<DateTime>();

            foreach (var appt in appointments)
            {
                for (var time = appt.StartTime; time < appt.EndTime; time = time.AddMinutes(SLOT_DURATION))
                {
                    occupiedSlots.Add(time);
                }
            }

            //  remover ocupados
            var availableSlots = allSlots
                .Where(slot => !occupiedSlots.Contains(slot))
                .Select(slot => new AvailableSlotDto
                {
                    Time = slot
                })
                .ToList();

            return availableSlots;
        }

        public async Task<bool> CancelAsync(Guid appointmentId)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(x => x.Id == appointmentId);

            if (appointment == null)
                return false;

            if (appointment.Status != AppointmentStatus.Scheduled)
                return false;

            appointment.Status = AppointmentStatus.Cancelled;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<AppointmentResponseDto>> GetByDayAsync(Guid professionalId, DateTime date)
        {
            return await _context.Appointments
                .Where(x =>
                    x.ProfessionalId == professionalId &&
                    x.StartTime.Date == date.Date &&
                    x.Status == AppointmentStatus.Scheduled
                )
                .OrderBy(x => x.StartTime)
                .Select(x => new AppointmentResponseDto
                {
                    Id = x.Id,
                    ClientName = x.Client.Name,
                    ProfessionalName = x.Professional.Name,
                    ServiceName = x.Service.Name,
                    StartTime = x.StartTime.ToString("yyyy-MM-ddTHH:mm"),
                    EndTime = x.EndTime.ToString("yyyy-MM-ddTHH:mm"),
                    Status = x.Status.ToString()
                })
                .ToListAsync();
        }

        public async Task<List<AppointmentResponseDto>> GetMyAppointmentsAsync(Guid userId)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (client == null)
                return new List<AppointmentResponseDto>();

            var appointments = await _context.Appointments
                .Where(x => x.ClientId == client.Id)
                .Include(x => x.Client)
                .Include(x => x.Professional)
                .Include(x => x.Service)
                .OrderByDescending(x => x.StartTime)
                .Select(x => new AppointmentResponseDto
                {
                    Id = x.Id,
                    ClientName = x.Client.Name,
                    ProfessionalName = x.Professional.Name,
                    ServiceName = x.Service.Name,
                    StartTime = x.StartTime.ToString("yyyy-MM-ddTHH:mm"),
                    EndTime = x.EndTime.ToString("yyyy-MM-ddTHH:mm"),
                    Status = x.Status.ToString()
                })
                .ToListAsync();

            return appointments;
        }

        public async Task<List<AppointmentResponseDto>> GetMyScheduleAsync(Guid userId)
        {
            //  Buscar o Professional pelo UserId
            var professional = await _context.Professionals
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (professional == null)
                return new List<AppointmentResponseDto>();

            //  Buscar agenda dele
            var appointments = await _context.Appointments
                .Where(x =>
                    x.ProfessionalId == professional.Id &&
                    x.Status == AppointmentStatus.Scheduled
                )
                .OrderBy(x => x.StartTime)
                .Select(x => new AppointmentResponseDto
                {
                    Id = x.Id,
                    ClientName = x.Client.Name,
                    ProfessionalName = x.Professional.Name,
                    ServiceName = x.Service.Name,
                    StartTime = x.StartTime.ToString("yyyy-MM-ddTHH:mm"),
                    EndTime = x.EndTime.ToString("yyyy-MM-ddTHH:mm"),
                    Status = x.Status.ToString()
                })
                .ToListAsync();

            return appointments;
        }

        public async Task<bool> CreateBlockedTimeAsync(Guid userId, CreateBlockedTimeDto dto)
        {
            var professional = await _context.Professionals
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (professional == null)
                return false;

            if (dto.End <= dto.Start)
                return false;

            var blocked = new BlockedTime
            {
                Id = Guid.NewGuid(),
                ProfessionalId = professional.Id,
                Start = dto.Start,
                End = dto.End,
                Reason = dto.Reason,
                CreatedAt = DateTime.UtcNow
            };

            _context.BlockedTimes.Add(blocked);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
