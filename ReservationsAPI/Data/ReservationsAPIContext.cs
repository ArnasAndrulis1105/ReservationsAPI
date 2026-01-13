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
        }
    }
}
