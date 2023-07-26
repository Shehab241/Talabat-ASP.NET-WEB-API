using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
	public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>> OrderBy { get; set; }
		public Expression<Func<T, object>> OrderByDescending { get ; set ; }
		public int Skip { get ; set ; }
		public int Take { get ; set ; }
		public bool IsPagintionEnbled { get; set; }

		public BaseSpecifications()
        {
            
        }

        public BaseSpecifications(Expression<Func<T, bool>> Criteriaexpression)
        {
            Criteria = Criteriaexpression;

		}

        public void AddOrderBy(Expression<Func<T, object>> OrderByexpression)
        {
            OrderBy = OrderByexpression;
        }
        public void AddOrderByDescending(Expression<Func<T, object>> OrderByDescendingexpression)
        {
			OrderByDescending = OrderByDescendingexpression;
        }

        public void ApplyPagintion(int skip,int take)
        {
            IsPagintionEnbled = true;
            Skip = skip;
            Take = take;

        }

	}
}
