using BackEnd.DAL;
using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd.BLL.Transaccion
{
	public class BLLTransaccionesImpl : BLLGenericoImpl<TRANSACCIONE>, IBLLTransacciones
	{
		private UnidadDeTrabajo<TRANSACCIONE> unidad;

		public BLLTransaccionesImpl()
		{
			EasyBudgetContext contexto = new EasyBudgetContext();
			unidad = new UnidadDeTrabajo<TRANSACCIONE>(contexto);
		}

		public List<TRANSACCIONE> GetAllTransactions(int id)
		{
			try
			{
				return unidad.genericDAL.GetAll().Where(x => x.FkIdPersona == id).ToList();
			}
			catch(Exception e)
			{
				throw e;
			}
		}

		public decimal GetExpensesForAllMonths(int id)
		{
			List<TRANSACCIONE> listaTransacciones = GetAllTransactions(id);

			decimal total = 0;
			//ver bien cual va a ser el id para gastos u obtener el id de gasto

			foreach (var item in listaTransacciones)
			{
				//Definir bien cual va a ser el id para gasto
				if (item.FkIdTipoTransaccion == 2)
				{
					total = total + item.TransaccionValor;
				}
			}

			return total;
		}

		public decimal GetIncomeForAllMonths(int id)
		{
			decimal total = 0;
			//ver bien cual va a ser el id para gastos u obtener el id de gasto

			List<TRANSACCIONE> listaTransacciones = GetAllTransactions(id);

			foreach (var item in listaTransacciones)
			{
				//Definir bien cual va a ser el id para ingreso
				if (item.FkIdTipoTransaccion == 3)
				{
					total = total + item.TransaccionValor;
				}
			}

			return total;
		}
	}
}
