using HotelInventory.Core.Interfaces;
using HotelInventory.DAL;
using HotelInventory.Services.Implementation;
using HotelInventory.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using HotelInventory.Repository.Implementations;

namespace HotelInventory.WebApi.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["DBConn:connectionString"];
            services.AddDbContext<DBContext>(o => o.UseSqlServer(connectionString));
        }
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ILookupDetailsRepository, LookupDetailsRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IGoogleMapDetailsRepository, GoogleMapDetailsRepository>();
            services.AddScoped<IPropertyFacilityMappingRepository, PropertyFacilityMappingRepository>();
            services.AddScoped<IPropertyImageMappingRepository, PropertyImageMappingRepository>();
            services.AddScoped<IPropertyPolicyMappingRepository, PropertyPolicyMappingRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IRoomFacilityMappingRepository, RoomFacilityMappingRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
        }
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ILookupDetailsService, LookupDetailsService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IGoogleMapDetailsService, GoogleMapDetailsService>();
            services.AddScoped<IPropertyFacilityMappingService, PropertyFacilityMappingService>();
            services.AddScoped<IPropertyImageMappingService, PropertyImageMappingService>();
            services.AddScoped<IPropertyPolicyMappingService, PropertyPolicyMappingService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IRoomFacilityMappingService, RoomFacilityMappingService>();
            services.AddScoped<IRoomService, RoomService>();
        }
    }
}
