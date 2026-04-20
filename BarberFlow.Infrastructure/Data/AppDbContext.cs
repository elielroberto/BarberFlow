using BarberFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberFlow.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<Service> Services => Set<Service>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Professional> Professionals => Set<Professional>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<BlockedTime> BlockedTimes => Set<BlockedTime>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.Price)
                    .HasColumnType("decimal(18,2)");

                entity.Property(x => x.SlotCount)
                    .IsRequired().HasDefaultValue(1);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasIndex(x => x.Email)
                    .IsUnique();

                entity.Property(x => x.PasswordHash)
                    .IsRequired();
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                modelBuilder.Entity<Appointment>(entity =>
                {
                    entity.HasKey(x => x.Id);

                    entity.HasOne(x => x.Client)
                        .WithMany()
                        .HasForeignKey(x => x.ClientId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasOne(x => x.Professional)
                        .WithMany()
                        .HasForeignKey(x => x.ProfessionalId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasOne(x => x.Service)
                        .WithMany()
                        .HasForeignKey(x => x.ServiceId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                // Índice para performance (muito importante)
                entity.HasIndex(x => new { x.ProfessionalId, x.StartTime, x.EndTime });
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(x => x.Phone)
                    .HasMaxLength(20);

                entity.Property(x => x.UserId)
                    .IsRequired();

                entity.HasIndex(x => x.UserId)
                    .IsUnique(); 

                entity.HasOne<User>()
                    .WithOne()
                    .HasForeignKey<Client>(x => x.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Professional>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(x => x.UserId)
                    .IsRequired();

                entity.HasIndex(x => x.UserId)
                    .IsUnique(); 

                entity.HasOne<User>()
                    .WithOne()
                    .HasForeignKey<Professional>(x => x.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BlockedTime>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Start).IsRequired();
                entity.Property(x => x.End).IsRequired();

                entity.HasOne(x => x.Professional)
                    .WithMany()
                    .HasForeignKey(x => x.ProfessionalId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
