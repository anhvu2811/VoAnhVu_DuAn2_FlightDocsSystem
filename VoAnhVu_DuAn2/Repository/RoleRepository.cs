using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Data;
using VoAnhVu_DuAn2.Models;

namespace VoAnhVu_DuAn2.Repository
{
    public interface IRoleRepository
    {
        List<RoleModel> getAllRole();
        void createRole(RoleModel role);
        void updateRole(RoleModel role);
        bool deleteRole(string id);
    }
    public class RoleRepository : IRoleRepository
    {
        private readonly MyDbContext _context;
        public RoleRepository(MyDbContext context)
        {
            _context = context;
        }

        public void createRole(RoleModel role)
        {
            try
            {
                _context.RoleModels.Add(role);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool deleteRole(string id)
        {
            try
            {
                var role = _context.RoleModels.FirstOrDefault(c => c.RoleId == id);
                if (role is null)
                {
                    return false;
                }
                _context.RoleModels.Remove(role);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RoleModel> getAllRole()
        {
            return _context.RoleModels.ToList();
        }

        public void updateRole(RoleModel role)
        {
            try
            {
                _context.RoleModels.Update(role);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
