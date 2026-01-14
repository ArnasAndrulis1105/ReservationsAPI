using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReservationsAPI.Models;

namespace ReservationsAPI.Data
{
    public class ReservationsAPIContext : DbContext
    {
        public ReservationsAPIContext (DbContextOptions<ReservationsAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Hall> Halls { get; set; }
        public DbSet<HallGroup> HallGroups { get; set; }
        public DbSet<HallSeat> HallSeats { get; set; }
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Reservation> Reservations => Set<Reservation>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Hall -> HallGroups (one-to-many)
            modelBuilder.Entity<Hall>()
                .HasMany(h => h.HallGroups)
                .WithOne(g => g.Hall)
                .HasForeignKey(g => g.HallId)
                .OnDelete(DeleteBehavior.Cascade);

            // HallGroup -> HallSeats (one-to-many)
            modelBuilder.Entity<HallGroup>()
                .HasMany(g => g.HallSeats)
                .WithOne(s => s.HallGroup)
                .HasForeignKey(s => s.HallGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reservation>()
               .HasOne(r => r.HallSeat)
               .WithMany() // arba .WithMany(s => s.Reservations) jei turi kolekciją
               .HasForeignKey(r => r.HallSeatId)
               .OnDelete(DeleteBehavior.NoAction); // arba Restrict

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Reservations)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.NoAction); // rekomenduojama ir či
        }
    }
}
