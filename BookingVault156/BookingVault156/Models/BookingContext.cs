using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookingVault156.Models
{
    internal class BookingContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:vault156booking.database.windows.net,1433;Initial Catalog=vault156booking;Persist Security Info=False;User ID=douglasadmin;Password=PrestonGarvey123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public DbSet<BookingHistory> Bookings { get; set; }
        public DbSet<Dweller> Dwellers { get; set; }
        public DbSet<Mutation> Mutations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        
        

        
    }
}
