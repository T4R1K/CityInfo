using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    // [Route("api/[controller]")] // with this approach, renaming/refactoring the controller would require refactoring the client code as well
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private ICityInfoRepository _cityInfoRepository;
        public CitiesController(ICityInfoRepository cityInforRepository)
        {
            // linjecting through constructor, if it's not feasible, it can also be injected as following:
            //HttpContext.RequestServices.GetService()...
            _cityInfoRepository = cityInforRepository;
        }



        [HttpGet()]
        public IActionResult GetCities() {
            // return Ok(CitiesDataStore.Current.Cities);
            var cityEntities = _cityInfoRepository.GetCities();

            var results = Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities);

            return Ok(results);
                        }

        [HttpGet ("api/cities/{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest= false) {

            var city = _cityInfoRepository.GetCity(id, includePointsOfInterest);
            
            if (city == null)
            {
                return NotFound();
            }
            if (includePointsOfInterest)
            {
                var cityResult = Mapper.Map<CityDto>(city);
                return Ok(cityResult);
                    }

            var cityWithoutPointsOfInterestResult = Mapper.Map<CityWithoutPointsOfInterestDto>(city);
            return Ok(cityWithoutPointsOfInterestResult);
              
        }

    }


    }

