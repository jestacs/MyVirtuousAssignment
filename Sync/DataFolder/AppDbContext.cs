using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sync.DataFolder
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = ConfigurationSettings.AppSettings["DbConnectionString"];
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<AbbreviatedContact> Contacts { get; set; }
    }
}
