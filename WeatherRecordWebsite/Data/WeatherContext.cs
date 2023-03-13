using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherRecordWebsite.Models;

namespace WeatherRecordWebsite.Data
{
    public class WeatherContext : DbContext
    {
        public WeatherContext (DbContextOptions<WeatherContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Temperature> Temperature { get; set;}
        public DbSet<Weather> Weather { get;set;}
        public DbSet<WindSpeed> WindSpeed { get; set;}
        public DbSet<Record> Records { get; set; }
        public DbSet<User> Users { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().ToTable("City");
            modelBuilder.Entity<Temperature>().ToTable("Temperature");
            modelBuilder.Entity<Weather>().ToTable("Weather");
            modelBuilder.Entity<WindSpeed>().ToTable("WindSpeed");
            modelBuilder.Entity<Record>().ToTable("Record");
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
