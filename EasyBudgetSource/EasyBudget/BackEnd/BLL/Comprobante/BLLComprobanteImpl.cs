using BackEnd.DAL;
using BackEnd.Model;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BackEnd.BLL.Comprobante
{
	public class BLLComprobanteImpl : BLLGenericoImpl<COMPROBANTE>, IBLLComprobante
	{
		private UnidadDeTrabajo<COMPROBANTE> unidad;

		public COMPROBANTE GetDetailsWithLoginID(int id)
		{
			try
			{
				COMPROBANTE resultado;
				using (unidad = new UnidadDeTrabajo<COMPROBANTE>(new EasyBudgetContext()))
				{
					Expression<Func<COMPROBANTE, bool>> consulta = (x => x.FkPersonaId == id);
					resultado = unidad.genericDAL.Find(consulta).FirstOrDefault();
				}
				return resultado;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public int GetLastID()
		{
			try
			{
				int resultado;
				resultado = int.Parse(GetAll().Max(x => x.ComprobanteId).ToString());

				return resultado;
			}
			catch (Exception err)
			{
				throw err;
			}
		}
	}
}
