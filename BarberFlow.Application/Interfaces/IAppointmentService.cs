using BarberFlow.Application.DTOs.Appointment;
using BarberFlow.Application.DTOs.BlockedTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberFlow.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<bool> CreateAsync(Guid userId, CreateAppointmentDto dto);
        Task<List<AvailableSlotDto>> GetAvailableSlotsAsync(Guid professionalId, DateTime date);
        Task<bool> CancelAsync(Guid appointmentId);
        Task<List<AppointmentResponseDto>> GetByDayAsync(Guid professionalId, DateTime date);
        Task<List<AppointmentResponseDto>> GetMyAppointmentsAsync(Guid userId);
        Task<List<AppointmentResponseDto>> GetMyScheduleAsync(Guid userId);
        Task<bool> CreateBlockedTimeAsync(Guid userId, CreateBlockedTimeDto dto);
    }
}
