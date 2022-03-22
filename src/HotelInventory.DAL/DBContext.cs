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
        public DbSet<PropertyPolicyMappingSnapshot> PropertyPolicyMappings { get; set; }
        public DbSet<PropertyImageMappingSnapshot> PropertyImageMappings { get; set; }
        public DbSet<RoomFacilityMappingSnapshot> RoomFacilityMappings { get; set; }
        public DbSet<PropertyFacilityMappingSnapshot> PropertyFacilityMappings { get; set; }
        public DbSet<GoogleMapDetailsSnapshot> GoogleMapDetails { get; set; }
        public DbSet<AddressSnapshot> Addresses { get; set; }
        public DbSet<RoomSnapshot> Rooms { get; set; }
        public DbSet<PropertySnapshot> Properties { get; set; }
        public DbSet<UserSnapshot> Users { get; set; }
        public DbSet<Country_State_City_AreaSnapshot> Areas { get; set; }

    }
}
