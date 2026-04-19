using BarberFlow.Application.DTOs.Appointment;
using BarberFlow.Application.DTOs.Service;
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
    public class ServiceService : IServiceService
    {
        private readonly AppDbContext _context;

        public ServiceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(CreateServiceDto dto)
        {
            var service = new Service
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                SlotCount = dto.SlotCount,
                Price = dto.Price,
                IsActive = true
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return service.Id;
        }
        public async Task<List<ServiceResponseDto>> GetAllAsync()
        {
            var services = _context.Services
                .Where(x => x.IsActive) 
                .Select(x => new ServiceResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    SlotCount = x.SlotCount,
                    Price = x.Price
                });

            return await services.ToListAsync();
        }

        public async Task<ServiceResponseDto> GetByIdAsync(Guid id)
        {
            var service = await _context.Services
                .Where(x => x.Id == id && x.IsActive)
                .Select(x => new ServiceResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    SlotCount = x.SlotCount,
                    Price = x.Price
                })
                .FirstOrDefaultAsync();

            return service;
        }

        public async Task<ServiceResponseDto?> UpdateAsync (Guid id, UpdateServiceDto dto)
        {
            var service = await _context.Services.FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            if (service == null)
            {
                return null;
            }
            service.Name = dto.Name;
            service.SlotCount = dto.SlotCount;
            service.Price = dto.Price;

            await _context.SaveChangesAsync();

            return new ServiceResponseDto
            {
                Id = service.Id,
                Name = service.Name,
                SlotCount = service.SlotCount,
                Price = service.Price
            };
        }

        public async Task <bool> DesactivateAsync(Guid id)
        {
            var service = await _context.Services.FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            if (service == null)
            {
                return false;
            }
            service.IsActive = false;

             await _context.SaveChangesAsync();

            return true;
        }

    }
}
