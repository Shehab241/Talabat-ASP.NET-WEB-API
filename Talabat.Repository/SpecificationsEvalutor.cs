using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
	public class SpecificationsEvalutor<TEntity> where TEntity : BaseEntity
	{
		public static IQueryable<TEntity> GetQurey(IQueryable<TEntity> inputQurey,ISpecifications<TEntity> spec)
		{
			var qurey = inputQurey;//dbcontext
			if(spec.Criteria is not null)
				qurey=qurey.Where(spec.Criteria);


			if(spec.OrderBy is not null)
				qurey=	qurey.OrderBy(spec.OrderBy);

			
			if(spec.OrderByDescending is not null)
				qurey=	qurey.OrderBy(spec.OrderByDescending);

			
			if(spec.IsPagintionEnbled)
				qurey=qurey.Skip(spec.Skip).Take(spec.Take);

			qurey = spec.Includes.Aggregate(qurey, (currentQurey, includeExpression) => currentQurey.Include(includeExpression));

			return qurey;
		}
	}
}
