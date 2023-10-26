using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Entities;
using VoAnhVu_DuAn2.Models;

namespace VoAnhVu_DuAn2.Services
{
    public interface IRoleService
    {
        List<RoleEntity> getAllRole();
        void createRole(RoleEntity role);
        void updateRole(RoleEntity role);
        bool deleteRole(string id);
    }
    public class RoleService : IRoleService
    {
        private readonly MyDbContext _context;
        public RoleService(MyDbContext context)
        {
            _context = context;
        }

        public void createRole(RoleEntity role)
        {
            try
            {
                _context.RoleEntities.Add(role);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool deleteRole(string id)
        {
            try
            {
                var role = _context.RoleEntities.FirstOrDefault(c => c.RoleId == id);
                if(role is null)
                {
                    return false;
                }
                _context.RoleEntities.Remove(role);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<RoleEntity> getAllRole()
        {
            return _context.RoleEntities.ToList();
        }

        public void updateRole(RoleEntity role)
        {
            try
            {
                _context.RoleEntities.Update(role);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
