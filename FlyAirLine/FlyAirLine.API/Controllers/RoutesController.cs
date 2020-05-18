using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlyAirLine.Aplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FlyAirLine.Aplication.Interfaces;
using FlyAirLine.Domain.Entities;
using FlyAirLine.API.DTO;

namespace FlyAirLine.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoutesController : ControllerBase
    {
        private readonly IRoutesAppService _routesAppService;

        public RoutesController(
            IRoutesAppService routesAppService
        )
        {
            _routesAppService = routesAppService;
        }

        [HttpGet]
        [Route("SearchBestRoute")]
        public JsonResult GetSearchBestRoute([FromBody]TripDTO trip)
        {
            string[] routeFound = _routesAppService.FindBestRoute(trip.Origin, trip.Destination);

            return new JsonResult( new { route = routeFound[0], value = routeFound[1] } ) ;
        }

        [HttpPost]
        [Route("AddNewRoutes")]
        public IActionResult PostAddNewRoutes([FromBody]IEnumerable<Route> Rotes)
        {
            bool success = _routesAppService.AddRoutes(Rotes);

            if (success)
                return Ok();
            else
                return Problem("failed to add route",null,500);
        }
    }
}