using Core.DataAccess.Tools;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Database.Mssql.EntityFramework
{
    public class MssqlCorrectorContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DatabaseConnectionParameters.MssqlConectionString);
        }
        public DbSet<CorrectorMaster> CorrectorMasters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAccess> UserAccess { get; set; }
        public DbSet<UserAccessType> UserAccessTypes { get; set; }
        //public DbSet<HourlyArchiveParameter> HourlyArchiveParameters { get; set; }
        //public DbSet<DailyArchiveParameter> DailyArchiveParameters { get; set; }
        //public DbSet<EventArchiveParameter> EventArchiveParameters { get; set; }
        //public DbSet<MonthlyType1ArchiveParameter> MonthlyType1ArchiveParameters { get; set; }
        //public DbSet<MonthlyType2ArchiveParameter> MonthlyType2ArchiveParameters { get; set; }
    }
}
