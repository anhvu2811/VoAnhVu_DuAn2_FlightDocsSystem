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
        #endregion
    }
}
