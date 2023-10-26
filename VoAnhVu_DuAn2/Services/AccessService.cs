using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Entities;
using VoAnhVu_DuAn2.Models;

namespace VoAnhVu_DuAn2.Services
{
    public interface IAccessService
    {
        List<AccessEntity> getAllAccess();
        void createAccess(AccessEntity access);
        void updateAccess(AccessEntity access);
        bool deleteAccess(string id);
    }
    public class AccessService : IAccessService
    {
        private readonly MyDbContext _context;
        public AccessService(MyDbContext context)
        {
            _context = context;
        }

        public void createAccess(AccessEntity access)
        {
            try
            {
                _context.AccessEntities.Add(access);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool deleteAccess(string id)
        {
            try
            {
                var access = _context.AccessEntities.FirstOrDefault(c => c.AccessId == id);
                if (access is null)
                {
                    return false;
                }
                _context.AccessEntities.Remove(access);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AccessEntity> getAllAccess()
        {
            return _context.AccessEntities.ToList();
        }

        public void updateAccess(AccessEntity access)
        {
            try
            {
                _context.AccessEntities.Update(access);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
