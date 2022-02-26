using HotelInventory.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Core.Interfaces
{
    public interface ILookupDetailsRepository
    {
        Task<IEnumerable<LookupDetailsSnapshot>> GetAllLookupDetailssAsync();
        Task<IEnumerable<LookupDetailsSnapshot>> GetFilteredLookupDetailsAsync(Expression<Func<LookupDetailsSnapshot, bool>> expression);
    }
}
