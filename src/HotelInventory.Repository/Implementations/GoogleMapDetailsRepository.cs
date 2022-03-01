using HotelInventory.Core.Domains;
using HotelInventory.Core.Interfaces;
using HotelInventory.DAL;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Repository.Implementations
{
    public class GoogleMapDetailsRepository : GenericRepository<GoogleMapDetailsSnapshot>, IGoogleMapDetailsRepository
    {
        public GoogleMapDetailsRepository(DBContext _Context)
            : base(_Context)
        {
        }

        public async Task<IEnumerable<GoogleMapDetailsSnapshot>> GetAllGoogleMapDetailsAsync()
        {
            return await GetAll();
        }
        public async Task<IEnumerable<GoogleMapDetailsSnapshot>> GetFilteredGoogleMapDetailsAsync(Expression<Func<GoogleMapDetailsSnapshot, bool>> expression)
        {
            return await GetFiltered(expression);
        }
        public async Task CreateGoogleMapDetails(GoogleMapDetailsSnapshot obj)
        {
            await Create(obj);
        }
        public async Task UpdateGoogleMapDetails(GoogleMapDetailsSnapshot obj)
        {
            await Update(obj);
        }
        public async Task DeleteGoogleMapDetails(GoogleMapDetailsSnapshot obj)
        {
            await Delete(obj);
        }
    }
}
