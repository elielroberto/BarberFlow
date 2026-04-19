using BarberFlow.Application.DTOs.Appointment;
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
        public async Task<bool> CreateAsync(CreateAppointmentDto dto)
        {
            //  Buscar serviço
            var service = await _context.Services
                .FirstOrDefaultAsync(x => x.Id == dto.ServiceId && x.IsActive);

            if (service == null)
                return false;

            // Não permitir passado
            if (dto.StartTime < DateTime.UtcNow)
                return false;

            //  Gerar slots necessários
            var requiredSlots = service.SlotCount;

            var slots = Enumerable.Range(0, requiredSlots)
                .Select(i => dto.StartTime.AddMinutes(i * SLOT_DURATION))
                .ToList();

            //  Validar conflito
            var hasConflict = await _context.Appointments
                .AnyAsync(x =>
                    x.ProfessionalId == dto.ProfessionalId &&
                    x.Status == AppointmentStatus.Scheduled &&
                    slots.Any(slot =>
                        slot >= x.StartTime && slot < x.EndTime
                    )
                );

            if (hasConflict)
                return false;

            //  Calcular EndTime baseado nos slots
            var endTime = dto.StartTime.AddMinutes(requiredSlots * SLOT_DURATION);

            //  Criar agendamento
            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                ClientId = dto.ClientId,
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
                    x.StartTime.Date == date.Date
                )
                .OrderBy(x => x.StartTime)
                .Select(x => new AppointmentResponseDto
                {
                    Id = x.Id,
                    ClientId = x.ClientId,
                    ProfessionalId = x.ProfessionalId,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    Status = x.Status
                })
                .ToListAsync();
        }

    }
}
