using HotelInventory.Core.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelInventory.DAL
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<RoleSnapshot> Roles { get; set; }
        public DbSet<LookupDetailsSnapshot> LookupDetails { get; set; }
        
    }
}
