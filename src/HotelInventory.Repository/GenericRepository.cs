using HotelInventory.Core;
using HotelInventory.DAL;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelInventory.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DBContext _Context { get; set; }
        public GenericRepository(DBContext Context)
        {
            _Context = Context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _Context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetFiltered(Expression<Func<T, bool>> expression)
        {
            return await _Context.Set<T>().Where(expression).ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
            return await _Context.Set<T>().FindAsync(id);
        }
        public async Task Create(T entity)
        {
            await _Context.Set<T>().AddAsync(entity);
            await _Context.SaveChangesAsync();
        }
        public async Task CreateBulk(IEnumerable<T> entity)
        {
            await _Context.Set<T>().AddRangeAsync(entity);
            await _Context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _Context.Set<T>().Update(entity);
            await _Context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _Context.Set<T>().Remove(entity);
            await _Context.SaveChangesAsync();
        }
        public async Task SaveChanges()
        {
            await _Context.SaveChangesAsync();
        }
    }
}
