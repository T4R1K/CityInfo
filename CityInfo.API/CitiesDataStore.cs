using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public class CitiesDataStore

    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore(); // temp data store, will be replaced with db

        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            // returns an instance of SourcesDataStore (to make sure we work with the same data - as long as we don't restart app)
            // (new c# 6 syntax - auto* property initializer syntax - it allows assignment of properties directly within the decoration. For readonly properties, like this one, it makes sure the proeprty is immutable)
            Cities = new List<CityDto>() {
            new CityDto()
            {
                Id =1, Name = "New York City", Description = "The one with that big park.", PointsOfInterest = new List<PointOfInterestDto>(){
                    new PointOfInterestDto()
                    {
                        Id=1,
                        Name="Central Park",
                        Description="The most visited urban park in the United States." },
                    new PointOfInterestDto()
                    {
                        Id=2,
                        Name="Empire State Building",
                        Description="A 1o2 Story skyscraper located in Midtown Mannhattan." } } },

            new CityDto() { Id =2, Name = "Antwerp", Description = "The oe with the cathedral that was never really finisher", PointsOfInterest = new List<PointOfInterestDto>(){
           new PointOfInterestDto()
                    {
                        Id=3,
                        Name="Cathedral of our Lady",
                        Description="A Gothic style cathedral, conceived by archizects Jan and Pieter Appelmans." },
                    new PointOfInterestDto()
                    {
                        Id=4,
                        Name="Empire State Building",
                        Description="A 1o2 Story skyscraper located in Midtown Mannhattan." } } },


           

            new CityDto() { Id =3, Name = "Paris", Description = "The one with that big tower", PointsOfInterest = new List<PointOfInterestDto>(){
                  new PointOfInterestDto()
                    {
                        Id=5,
                        Name="Eiffel Tower",
                        Description="A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel." },
                    new PointOfInterestDto()
                    {
                        Id=6,
                        Name="The Louvre",
                        Description="The world largest museum." } } },



           
        };
        }
    } 
}
