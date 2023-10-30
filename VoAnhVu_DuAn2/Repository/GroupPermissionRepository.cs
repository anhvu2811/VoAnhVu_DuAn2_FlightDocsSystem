using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Entities;
using VoAnhVu_DuAn2.Models;

namespace VoAnhVu_DuAn2.Repository
{
    public interface IGroupPermissionRepository
    {
        List<GroupPermissionModel> getAllGroupPermission();
        void createGroupPermission(GroupPermissionEntity gp);
        void updateGroupPermission(GroupPermissionEntity gp);
        bool deleteGroupPermission(string id);
    }
    public class GroupPermissionRepository : IGroupPermissionRepository
    {
        private readonly MyDbContext _context;
        public GroupPermissionRepository(MyDbContext context)
        {
            _context = context;
        }

        public void createGroupPermission(GroupPermissionEntity gp)
        {
            try
            {
                _context.GroupPermissionEntities.Add(gp);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool deleteGroupPermission(string id)
        {
            try
            {
                var gp = _context.GroupPermissionEntities.FirstOrDefault(c => c.GroupPermissionId == id);
                if (gp is null)
                {
                    return false;
                }
                _context.GroupPermissionEntities.Remove(gp);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GroupPermissionModel> getAllGroupPermission()
        {
            var gps = _context.GroupPermissionEntities
                .Include(gp => gp.Access)
                .Select(gp => new GroupPermissionModel
                {
                    GroupPermissionId = gp.GroupPermissionId,
                    GroupPermissionName = gp.GroupPermissionName,
                    CreateDate = gp.CreateDate,
                    Note = gp.Note,
                    Access = gp.Access
                }).ToList();
            return gps;
        }

        public void updateGroupPermission(GroupPermissionEntity gp)
        {
            try
            {
                _context.GroupPermissionEntities.Update(gp);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
