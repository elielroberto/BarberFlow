using BarberFlow.Application.DTOs.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberFlow.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<bool> CreateAsync(CreateAppointmentDto dto);
        Task<List<AvailableSlotDto>> GetAvailableSlotsAsync(Guid professionalId, DateTime date);
        Task<bool> CancelAsync(Guid appointmentId);
        Task<List<AppointmentResponseDto>> GetByDayAsync(Guid professionalId, DateTime date);
    }
}
