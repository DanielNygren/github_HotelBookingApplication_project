using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Inlämning3ApplikationHotelBooking.Models;
using Microsoft.AspNetCore.Identity;

namespace Inlämning3ApplikationHotelBooking.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>().OwnsOne(x => x.Coordinate, c =>
            {
                c.Property(l => l.Latitude)
                .IsRequired(true);

                c.Property(l => l.Longitude)
                .IsRequired(true);

                c.Property(l => l.Latitude)
                .HasColumnName("Latitude");

                c.Property(l => l.Longitude)
                .HasColumnName("Longitude");

                c.Ignore(a => a.Altitude);
            });

            modelBuilder.Entity<Restaurant>().OwnsOne(x => x.Coordinate, c =>
            {
                c.Property(l => l.Latitude)
                .IsRequired(true);

                c.Property(l => l.Longitude)
                .IsRequired(true);

                c.Property(l => l.Latitude)
                .HasColumnName("Latitude");

                c.Property(l => l.Longitude)
                .HasColumnName("Longitude");

                c.Ignore(a => a.Altitude);
            });


            base.OnModelCreating(modelBuilder);
            // any unique string id
            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            const string ROLE_ID = "ad376a8f-9eab-4bb9-9fca-30b01540f445";

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ROLE_ID,
                Name = "admin",
                NormalizedName = "admin"
            });

            var hasher = new PasswordHasher<IdentityUser>();
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = ADMIN_ID,
                UserName = "admin@gmail.com",
                NormalizedUserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin123#"),
                SecurityStamp = string.Empty
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Account> Account { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }


    }
}
