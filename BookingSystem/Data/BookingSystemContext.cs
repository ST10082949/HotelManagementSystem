using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookingSystem.Models;

namespace BookingSystem.Data
{
    public class BookingSystemContext : DbContext
    {
        public BookingSystemContext(DbContextOptions<BookingSystemContext> options)
            : base(options)
        {
        }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Guest> Guest { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Room> Room { get; set; }


    }
}
