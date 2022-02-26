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
    public class LookupDetailsRepository : GenericRepository<LookupDetailsSnapshot>, ILookupDetailsRepository
    {
        public LookupDetailsRepository(DBContext _Context)
            : base(_Context)
        {
        }
        public async Task<IEnumerable<LookupDetailsSnapshot>> GetAllLookupDetailssAsync()
        {
            return await GetAll();
        }
        public async Task<IEnumerable<LookupDetailsSnapshot>> GetFilteredLookupDetailsAsync(Expression<Func<LookupDetailsSnapshot, bool>> expression)
        {
            return await GetFiltered(expression);
        }
    }
}
