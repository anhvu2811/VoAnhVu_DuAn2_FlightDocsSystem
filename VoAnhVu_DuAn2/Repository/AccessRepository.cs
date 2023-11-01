using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Data;
using VoAnhVu_DuAn2.Models;

namespace VoAnhVu_DuAn2.Repository
{
    public interface IAccessRepository
    {
        List<AccessModel> getAllAccess();
        void createAccess(AccessModel access);
        void updateAccess(AccessModel access);
        bool deleteAccess(string id);
    }
    public class AccessRepository : IAccessRepository
    {
        private readonly MyDbContext _context;
        public AccessRepository(MyDbContext context)
        {
            _context = context;
        }

        public void createAccess(AccessModel access)
        {
            try
            {
                _context.AccessModels.Add(access);
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
                var access = _context.AccessModels.FirstOrDefault(c => c.AccessId == id);
                if (access is null)
                {
                    return false;
                }
                _context.AccessModels.Remove(access);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AccessModel> getAllAccess()
        {
            return _context.AccessModels.ToList();
        }

        public void updateAccess(AccessModel access)
        {
            try
            {
                _context.AccessModels.Update(access);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
