using Microsoft.EntityFrameworkCore;
using ShackProfiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShackProfiles.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<ShackProfile> ShackProfiles { get; set; }
    }
}
