using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _dbcontext;

		public GenericRepository(StoreContext dbcontext)
        {
			_dbcontext = dbcontext;
		}
        public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			//if (typeof(T)==typeof(Product))
			//	return (IEnumerable<T>) await _dbcontext.Products.Include(p=>p.ProductBrand).Include(p=>p.ProductType).ToListAsync();
			//else
				return await _dbcontext.Set<T>().ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			//return await _dbcontext.Set<T>().Where(X => X.Id == id).FirstOrDefaultAsync();
			return await _dbcontext.Set<T>().FindAsync(id);
		}
		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySpecificatoin(spec).ToListAsync();
		}

		public async Task<int> GetCountWithSpecAsync(ISpecifications<T> Spec)
		{
			return await ApplySpecificatoin(Spec ).CountAsync();
		}

		public async Task<T> GetByIdWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySpecificatoin(spec).FirstOrDefaultAsync();
		}

		private IQueryable<T> ApplySpecificatoin(ISpecifications<T> spec)
		{

			return SpecificationsEvalutor<T>.GetQurey(_dbcontext.Set<T>(),spec);
		}

	}
}
