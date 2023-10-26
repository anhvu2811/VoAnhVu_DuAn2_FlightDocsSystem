using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Entities;

namespace VoAnhVu_DuAn2.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> opt) : base(opt)
        {

        }
        #region
        public DbSet<RoleEntity> RoleEntities { get; set; }
        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<GroupPermissionEntity> GroupPermissionEntities { get; set; }
        public DbSet<AccessEntity> AccessEntities { get; set; }
        public DbSet<FlightEntity> FlightEntities { get; set; }
        public DbSet<DocumentTypeEntity> DocumentTypeEntities { get; set; }
        public DbSet<DocumentEntity> DocumentEntities { get; set; }
        #endregion
    }
}
