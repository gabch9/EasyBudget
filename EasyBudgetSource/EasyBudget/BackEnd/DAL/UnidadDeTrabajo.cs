using BackEnd.Model;
using System;

namespace BackEnd.DAL
{
	public class UnidadDeTrabajo<TEntity> : IDisposable where TEntity : class
	{
		private readonly EasyBudgetContext context;
		public IDALGenerico<TEntity> genericDAL;

		public UnidadDeTrabajo(EasyBudgetContext _context)
		{
			context = _context;
			genericDAL = new DALGenericoImpl<TEntity>(context);
		}

		public void Complete()
		{
			context.SaveChanges();
		}

		public void Dispose()
		{
			context.Dispose();
		}
	}
}
