using HotelInventory.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Core.Interfaces
{
    public interface IGoogleMapDetailsRepository
    {
        Task<IEnumerable<GoogleMapDetailsSnapshot>> GetAllGoogleMapDetailsAsync();
        Task<IEnumerable<GoogleMapDetailsSnapshot>> GetFilteredGoogleMapDetailsAsync(Expression<Func<GoogleMapDetailsSnapshot, bool>> expression);
        Task CreateGoogleMapDetails(GoogleMapDetailsSnapshot obj);
        Task UpdateGoogleMapDetails(GoogleMapDetailsSnapshot obj);
        Task DeleteGoogleMapDetails(GoogleMapDetailsSnapshot obj);
    }
}
