using Microsoft.EntityFrameworkCore;
using SpeedzCarWashAPI.Models;

namespace SpeedzCarWashAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 

        }

        public DbSet<Washer> Washers { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<Booking> Bookings { get; set; }
    }
}
