using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Data;
using VoAnhVu_DuAn2.DTO;
using VoAnhVu_DuAn2.Models;

namespace VoAnhVu_DuAn2.Repository
{
    public interface IGroupPermissionRepository
    {
        List<GroupPermissionDTO> getAllGroupPermission();
        void createGroupPermission(GroupPermissionModel gp);
        void updateGroupPermission(GroupPermissionModel gp);
        bool deleteGroupPermission(string id);
    }
    public class GroupPermissionRepository : IGroupPermissionRepository
    {
        private readonly MyDbContext _context;
        public GroupPermissionRepository(MyDbContext context)
        {
            _context = context;
        }

        public void createGroupPermission(GroupPermissionModel gp)
        {
            try
            {
                _context.GroupPermissionModels.Add(gp);
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
                var gp = _context.GroupPermissionModels.FirstOrDefault(c => c.GroupPermissionId == id);
                if (gp is null)
                {
                    return false;
                }
                _context.GroupPermissionModels.Remove(gp);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GroupPermissionDTO> getAllGroupPermission()
        {
            var gps = _context.GroupPermissionModels
                .Include(gp => gp.Access)
                .Select(gp => new GroupPermissionDTO
                {
                    GroupPermissionId = gp.GroupPermissionId,
                    GroupPermissionName = gp.GroupPermissionName,
                    CreateDate = gp.CreateDate,
                    Note = gp.Note,
                    Access = gp.Access
                }).ToList();
            return gps;
        }

        public void updateGroupPermission(GroupPermissionModel gp)
        {
            try
            {
                _context.GroupPermissionModels.Update(gp);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
