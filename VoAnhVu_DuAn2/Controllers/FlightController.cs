using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Entities;
using VoAnhVu_DuAn2.Models;
using VoAnhVu_DuAn2.Repository;

namespace VoAnhVu_DuAn2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightRepository _flightRepository;
        public FlightController(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }
        [HttpGet]
        [Route("/api/[Controller]/get-all-flights")]
        public IActionResult getAllFlight()
        {
            try
            {
                var flight = _flightRepository.getAllFlight();
                if (!flight.Any())
                {
                    return BadRequest("Không có chuyến bay nào.");
                }
                return Ok(flight);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("/api/[Controller]/create-flight")]
        public IActionResult createFlight(FlightModel flight)
        {
            try
            {
                var kt = _flightRepository.getAllFlight().Where(c => c.FlightId == flight.FlightId);
                if (kt.Any())
                {
                    return BadRequest("Id đã tồn tại ! Hãy nhập mã khác");
                }
                FlightEntity flightEntity = new FlightEntity
                {
                    FlightId = flight.FlightId,
                    Date = flight.Date,
                    PointOfLoading = flight.PointOfLoading,
                    PointOfUnloading = flight.PointOfUnloading
                };
                _flightRepository.createFlight(flightEntity);
                return Ok(flightEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("/api/[Controller]/update-flight")]
        public IActionResult updateFlight(FlightModel flight)
        {
            try
            {
                FlightEntity flightEntity = new FlightEntity
                {
                    FlightId = flight.FlightId,
                    Date = flight.Date,
                    PointOfLoading = flight.PointOfLoading,
                    PointOfUnloading = flight.PointOfUnloading
                };
                _flightRepository.updateFlight(flightEntity);
                return Ok(flightEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("/api/[Controller]/delete-flight")]
        public IActionResult deleteFlight(string id)
        {
            try
            {
                bool flight = _flightRepository.deleteFlight(id);
                if (!flight)
                {
                    return BadRequest("Không tìm thấy chuyến bay để xóa!");
                }
                return Ok("Xóa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
