using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Entities
{
    public class CityInfoContext : DbContext
    {    
        public CityInfoContext (DbContextOptions<CityInfoContext> options)
            :base(options)
        {
            //Database.EnsureCreated();
            Database.Migrate(); // will execute migrations


            // Few gotchas:
            // - If more then one DbContext exist, DbContext name as argument needs to be specified: -Context=PikerContext (PowerShell commands); --context=PikerContext (dotnet commands)

            // Steps for initial setup:
            // - make sure database doesn't exist
            // - uncomment Database.EnsureCreated(); (for initial table creation)
            // - run the app..
            // - make sure migrations folder doesn't contain existing migrations for the context
            // - in Package Manager Console execute: Add-Migration CityInfoContextInitial. For VSCode: dotnet ef migrations add CityInfoContextInitial
            // - delete database
            // - comment out Database.EnsureCreated(); and uncomment Database.Migrate(); line
            // - run the app..
            // : from this point on, DB will be created (if not exists), and will be migrated (if not up to date) each time the app is run.
            //** The other way would be to keep only Database.EnsureCreated(); line, and to manually migrate the database each time new migration is needed (by executing: Add-Migration MigName /  dotnet ef migrations add MigName), 
            //   and then to update the database (by executing: Update-Database / dotnet ef database update)
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.HasSequence<long>("Cities");
        //     modelBuilder.HasSequence<long>("PointsOfInterests");

        //     // modelBuilder.Entity<City>().Property(p => p.Id).ValueGeneratedOnAdd();
        //     // modelBuilder.Entity<PointOfInterest>().Property(p => p.Id).ValueGeneratedOnAdd();
        // }
    }
}
