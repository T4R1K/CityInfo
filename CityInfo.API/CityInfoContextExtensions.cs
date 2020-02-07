using CityInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public static class CityInfoContextExtensions
    {
        public static void EnsureSeedDataForContext(this CityInfoContext context)
        {
            // skip seeding if db is not empty
            if (context.Cities.Any())
            {
                return;
            }
            var cities = new List<City>() {
            new City()
            {
                Id =1, Name = "New York City", Description = "The one with that big park.", PointsOfInterest = new List<PointOfInterest>(){
                    new PointOfInterest()
                    {
                        Id=1,
                        Name="Central Park",
                        Description="The most visited urban park in the United States." },
                    new PointOfInterest()
                    {
                        Id=2,
                        Name="Empire State Building",
                        Description="A 1o2 Story skyscraper located in Midtown Mannhattan." } } },

            new City() { Id =2, Name = "Antwerp", Description = "The oe with the cathedral that was never really finisher", PointsOfInterest = new List<PointOfInterest>(){
           new PointOfInterest()
                    {
                        Id=3,
                        Name="Cathedral of our Lady",
                        Description="A Gothic style cathedral, conceived by archizects Jan and Pieter Appelmans." },
                    new PointOfInterest()
                    {
                        Id=4,
                        Name="Empire State Building",
                        Description="A 1o2 Story skyscraper located in Midtown Mannhattan." } } },


        };
            context.Cities.AddRange(cities);
            context.SaveChanges();



        }
    }
}

