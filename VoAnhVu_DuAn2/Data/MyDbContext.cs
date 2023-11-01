using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Models;

namespace VoAnhVu_DuAn2.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> opt) : base(opt)
        {

        }
        #region
        public DbSet<RoleModel> RoleModels { get; set; }
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<GroupPermissionModel> GroupPermissionModels { get; set; }
        public DbSet<AccessModel> AccessModels { get; set; }
        public DbSet<FlightModel> FlightModels { get; set; }
        public DbSet<DocumentTypeModel> DocumentTypeModels { get; set; }
        public DbSet<DocumentModel> DocumentModels { get; set; }
        #endregion
    }
}
