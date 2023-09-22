using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace BackEnd.DAL
{
	public class DALGenericoImpl<TEntity> : IDALGenerico<TEntity> where TEntity : class
	{
		protected readonly EasyBudgetContext Context;

		public DALGenericoImpl(EasyBudgetContext context)
		{
			Context = context;
		}

		public void Add(TEntity entity)
		{
			try
			{
				Context.Set<TEntity>().Add(entity);
			}
			catch (Exception err)
			{

				throw err;
			}
		}

		public void AddRange(IEnumerable<TEntity> entities)
		{
			try
			{
				Context.Set<TEntity>().AddRange(entities);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
		{
			try
			{
				return Context.Set<TEntity>().Where(predicate);
			}
			catch (Exception)
			{

				return null;
			}
		}

		public TEntity Get(int id)
		{
			try
			{
				return Context.Set<TEntity>().Find(id);
			}
			catch (Exception)
			{

				return null;
			}
		}

		public IEnumerable<TEntity> GetAll()
		{
			try
			{
				return Context.Set<TEntity>().ToList();
			}
			catch (Exception err)
			{

				throw err;
			}
		}

		public void Remove(TEntity entity)
		{
			try
			{
				Context.Set<TEntity>().Attach(entity);
				Context.Set<TEntity>().Remove(entity);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public void RemoveRange(IEnumerable<TEntity> entities)
		{
			try
			{
				Context.Set<TEntity>().RemoveRange(entities);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
		{
			try
			{
				return Context.Set<TEntity>().SingleOrDefault(predicate);
			}
			catch (Exception)
			{

				return null;
			}
		}

		public void Update(TEntity entity)
		{
			try
			{
				Context.Entry(entity).State = EntityState.Modified;
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
