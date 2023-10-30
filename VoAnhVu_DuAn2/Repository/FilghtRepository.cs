using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Entities;
using VoAnhVu_DuAn2.Models;

namespace VoAnhVu_DuAn2.Repository
{
    public interface IFlightRepository
    {
        List<FlightEntity> getAllFlight();
        FlightModel getFlightById(string id);
        void createFlight(FlightEntity flight);
        void updateFlight(FlightEntity flight);
        bool deleteFlight(string id);
    }
    public class FlightRepository : IFlightRepository
    {
        private readonly MyDbContext _context;
        public FlightRepository(MyDbContext context)
        {
            _context = context;
        }

        public void createFlight(FlightEntity flight)
        {
            try
            {
                _context.FlightEntities.Add(flight);
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
                var flight = _context.FlightEntities.FirstOrDefault(c => c.FlightId == id);
                if (flight is null)
                {
                    return false;
                }
                _context.FlightEntities.Remove(flight);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FlightEntity> getAllFlight()
        {
            return _context.FlightEntities.ToList();
        }

        public FlightModel getFlightById(string id)
        {
            var flight = _context.FlightEntities.FirstOrDefault(c => c.FlightId == id);
            if (flight != null)
            {
                var flightModel = new FlightModel
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

        public void updateFlight(FlightEntity flight)
        {
            try
            {
                _context.FlightEntities.Update(flight);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
