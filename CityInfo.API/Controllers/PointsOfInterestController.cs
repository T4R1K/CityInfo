using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CityInfo.API.Controllers
{
    // [Route("api/[controller]")] // with this approach, renaming/refactoring the controller would require refactoring the client code as well
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private ILogger<PointsOfInterestController> _logger;
        private IMailService _mailService;
        private ICityInfoRepository _cityInfoRepository;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            LocalMailService mailService,
            ICityInfoRepository cityInfoRepository)
        {
            // linjecting through constructor, if it's not feasible, it can also be injected as following:
            //HttpContext.RequestServices.GetService()...
            _logger = logger;
            _mailService = mailService;
            _cityInfoRepository = cityInfoRepository;


        }


        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            try
            { 

                if (!_cityInfoRepository.CityExists(cityId))
                {
                    _logger.LogInformation($"City with id {cityId} wasnt found when accessing points of interest.");
                    return NotFound();
                }



                var pointsOfInterestForCity = _cityInfoRepository.GetPointsOfInterestForCity(cityId);
                var pointsOfInterestForCityResults =
                    Mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity);
                return Ok(pointsOfInterestForCityResults);
            }

           
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.", ex);
                return StatusCode(500, "A problem happend while handling your request.");
            }
        }

        [HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointsOfInterest(int cityId, int id)
        {

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            var pointOfInterestResult = Mapper.Map<PointOfInterestDto>(pointOfInterest);

            return Ok(pointOfInterestResult);

        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId,
         [FromBody] PointOfInterestDto pointOfInterest)
        {
            if (pointOfInterest == null) { return BadRequest(); }
            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be differetn from the name.");
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var finalPointOfInterest = Mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            _cityInfoRepository.AddPointOfInterestForCity(cityId, finalPointOfInterest);
            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happend while handling your request.");
            }

            var createdPointOfInterestToReturn = Mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new
            { cityId = cityId, id = createdPointOfInterestToReturn.Id }, createdPointOfInterestToReturn);
        }

        [HttpPut("{cityId}/pointofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,
         [FromBody] PointOfInterestDto pointOfInterest)

        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }
            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            Mapper.Map(pointOfInterest, pointOfInterestEntity);

            if (!_cityInfoRepository.Save())
            {

                return StatusCode(500, "A problem happend while handling your request.");
            }

                return NoContent();
            
        }


        [HttpPatch("{cityId}/pointofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)

            { return NotFound(); }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == id);
            if (pointOfInterestFromStore == null) { return NotFound(); }

            var pointOfInterestToPatch =
                new PointOfInterestForUpdateDto()
                {
                    Name = pointOfInterestFromStore.Name,
                    Description = pointOfInterestFromStore.Description
                };

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different frome the name.");
            }
            TryValidateModel(pointOfInterestToPatch);

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();

        }
        [HttpDelete("{cityId}/pointofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
           if (!_cityInfoRepository.CityExists(cityId, id))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);

           if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happend while handling your request.");
            }

            _mailService.Send("Point of interest deleted.",
                $"Point of interest {pointOfInterestEntity.Name} with id {pointOfInterestEntity.Id} was deleted.");



            return NoContent();
        }



    } }
 


            
            
        
    
    
    

