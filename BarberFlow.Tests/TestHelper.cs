using BarberFlow.Application.DTOs.Appointment;
using BarberFlow.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberFlow.Tests
{
    using BarberFlow.Domain.Entities;
    using BarberFlow.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;

    public static class TestHelper
    {
        public static AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        public static Client CreateClient(AppDbContext context)
        {
            var client = new Client
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Client"
            };

            context.Clients.Add(client);
            context.SaveChanges();

            return client;
        }

        public static Service CreateService(AppDbContext context)
        {
            var service = new Service
            {
                Id = Guid.NewGuid(),
                Name = "Corte",
                SlotCount = 1,
                Price = 30,
                IsActive = true
            };

            context.Services.Add(service);
            context.SaveChanges();

            return service;
        }

        public static Professional CreateProfessional(AppDbContext context)
        {
            var professional = new Professional
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Barber"
            };

            context.Professionals.Add(professional);
            context.SaveChanges();

            return professional;
        }
    }
}
