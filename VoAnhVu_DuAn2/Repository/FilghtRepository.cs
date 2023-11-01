using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Data;
using VoAnhVu_DuAn2.DTO;
using VoAnhVu_DuAn2.Models;

namespace VoAnhVu_DuAn2.Repository
{
    public interface IFlightRepository
    {
        List<FlightModel> getAllFlight();
        FlightDTO getFlightById(string id);
        void createFlight(FlightModel flight);
        void updateFlight(FlightModel flight);
        bool deleteFlight(string id);
    }
    public class FlightRepository : IFlightRepository
    {
        private readonly MyDbContext _context;
        public FlightRepository(MyDbContext context)
        {
            _context = context;
        }

        public void createFlight(FlightModel flight)
        {
            try
            {
                _context.FlightModels.Add(flight);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool deleteFlight(string id)
        {
            try
            {
                var flight = _context.FlightModels.FirstOrDefault(c => c.FlightId == id);
                if (flight is null)
                {
                    return false;
                }
                _context.FlightModels.Remove(flight);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FlightModel> getAllFlight()
        {
            return _context.FlightModels.ToList();
        }

        public FlightDTO getFlightById(string id)
        {
            var flight = _context.FlightModels.FirstOrDefault(c => c.FlightId == id);
            if (flight != null)
            {
                var flightModel = new FlightDTO
                {
                    FlightId = flight.FlightId,
                    Date = flight.Date,
                    PointOfLoading = flight.PointOfLoading,
                    PointOfUnloading = flight.PointOfUnloading
                };
                return flightModel;
            }
            return null;
        }

        public void updateFlight(FlightModel flight)
        {
            try
            {
                _context.FlightModels.Update(flight);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
