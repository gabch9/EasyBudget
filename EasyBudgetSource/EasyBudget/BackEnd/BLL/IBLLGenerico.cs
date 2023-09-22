using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BackEnd.BLL
{
	public interface IBLLGenerico<T> where T : class
	{
		T Get(int id);
		List<T> GetAll();
		List<T> Find(Expression<Func<T, bool>> predicate);

		T SingleOrDefault(Expression<Func<T, bool>> predicate);

		string Add(T entity);
		string AddRange(IEnumerable<T> entities);

		string Update(T entity);
		string Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);
	}
}
