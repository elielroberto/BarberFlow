using BarberFlow.Application.DTOs.Appointment;
using BarberFlow.Application.Services;
using BarberFlow.Domain.Entities;
using BarberFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Xunit;

namespace BarberFlow.Tests;

public class AppointmentServiceTests
{
    [Fact]
    public async Task CreateAsync_Should_ReturnFalse_When_TimeIsBlocked()
    {
        // ARRANGE (preparar cenário)

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        var context = new AppDbContext(options);

        var userId = Guid.NewGuid();

        var professional = new Professional
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = "Barber"
        };

        var client = new Client
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = "Client"
        };

        var service = new Service
        {
            Id = Guid.NewGuid(),
            Name = "Corte",
            SlotCount = 1,
            Price = 30,
            IsActive = true
        };

        context.Professionals.Add(professional);
        context.Clients.Add(client);
        context.Services.Add(service);

        //  bloqueio
        context.BlockedTimes.Add(new BlockedTime
        {
            Id = Guid.NewGuid(),
            ProfessionalId = professional.Id,
            Start = new DateTime(2026, 4, 20, 14, 0, 0),
            End = new DateTime(2026, 4, 20, 15, 0, 0),
            CreatedAt = DateTime.UtcNow
        });

        await context.SaveChangesAsync();

        var appointmentService = new AppointmentService(context);

        var dto = new CreateAppointmentDto
        {
            ProfessionalId = professional.Id,
            ServiceId = service.Id,
            StartTime = new DateTime(2026, 4, 20, 14, 0, 0)
        };

        //  ACT (executar)
        var result = await appointmentService.CreateAsync(userId, dto);

        //  ASSERT (validar)
        Assert.False(result);
    }

    [Fact]
    public async Task CreateAsync_Should_ReturnTrue_When_TimeIsAvailable()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb2")
            .Options;

        var context = new AppDbContext(options);

        var userId = Guid.NewGuid();

        var professional = new Professional
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = "Barber"
        };

        var client = new Client
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = "Client"
        };

        var service = new Service
        {
            Id = Guid.NewGuid(),
            Name = "Corte",
            SlotCount = 1,
            Price = 30,
            IsActive = true
        };

        context.Professionals.Add(professional);
        context.Clients.Add(client);
        context.Services.Add(service);

        await context.SaveChangesAsync();

        var appointmentService = new AppointmentService(context);

        var dto = new CreateAppointmentDto
        {
            ProfessionalId = professional.Id,
            ServiceId = service.Id,
            StartTime = new DateTime(2026, 4, 20, 15, 0, 0)
        };

        var result = await appointmentService.CreateAsync(userId, dto);

        Assert.True(result);
    }

    [Fact]
    public async Task CreateAsync_Should_ReturnFalse_When_ClientNotFound()
    {
        var context = TestHelper.CreateContext();

        var service = TestHelper.CreateService(context);

        var appointmentService = new AppointmentService(context);

        var dto = new CreateAppointmentDto
        {
            ProfessionalId = Guid.NewGuid(),
            ServiceId = service.Id,
            StartTime = DateTime.Now.AddHours(2)
        };

        var result = await appointmentService.CreateAsync(Guid.NewGuid(), dto);

        Assert.False(result);
    }

    [Fact]
    public async Task CreateAsync_Should_ReturnFalse_When_ServiceNotFound()
    {
        var context = TestHelper.CreateContext();

        var client = TestHelper.CreateClient(context);

        var appointmentService = new AppointmentService(context);

        var dto = new CreateAppointmentDto
        {
            ProfessionalId = Guid.NewGuid(),
            ServiceId = Guid.NewGuid(),
            StartTime = DateTime.Now.AddHours(2)
        };

        var result = await appointmentService.CreateAsync(client.UserId, dto);

        Assert.False(result);
    }

    [Fact]
    public async Task CreateAsync_Should_ReturnFalse_When_StartTimeIsPast()
    {
        var context = TestHelper.CreateContext();

        var client = TestHelper.CreateClient(context);
        var service = TestHelper.CreateService(context);

        var appointmentService = new AppointmentService(context);

        var dto = new CreateAppointmentDto
        {
            ProfessionalId = Guid.NewGuid(),
            ServiceId = service.Id,
            StartTime = DateTime.Now.AddHours(-1)
        };

        var result = await appointmentService.CreateAsync(client.UserId, dto);

        Assert.False(result);
    }

    [Fact]
    public async Task CreateAsync_Should_ReturnFalse_When_MinuteIsInvalid()
    {
        var context = TestHelper.CreateContext();

        var client = TestHelper.CreateClient(context);
        var service = TestHelper.CreateService(context);

        var appointmentService = new AppointmentService(context);

        var dto = new CreateAppointmentDto
        {
            ProfessionalId = Guid.NewGuid(),
            ServiceId = service.Id,
            StartTime = DateTime.Now.Date.AddHours(14).AddMinutes(17) 
        };

        var result = await appointmentService.CreateAsync(client.UserId, dto);

        Assert.False(result);
    }

    [Fact]
    public async Task CreateAsync_Should_ReturnTrue_When_Valid()
    {
        var context = TestHelper.CreateContext();

        var client = TestHelper.CreateClient(context);
        var service = TestHelper.CreateService(context);
        var professional = TestHelper.CreateProfessional(context);

        var appointmentService = new AppointmentService(context);

        var dto = new CreateAppointmentDto
        {
            ProfessionalId = professional.Id,
            ServiceId = service.Id,
            StartTime = DateTime.Now.Date.AddDays(1).AddHours(15) 
        };

        var result = await appointmentService.CreateAsync(client.UserId, dto);

        Assert.True(result);
    }
}
