using BackEnd.Model;
using System.Collections.Generic;

namespace BackEnd.BLL.Presupuesto
{
	public interface IBLLPresupuesto : IBLLGenerico<PRESUPUESTO>
	{
        PRESUPUESTO GetBudgetForUser(int id);

		IEnumerable<string> GetMonthsForChart(int year, int idUsuario);

		List<decimal> GetSalariesForChart(int year, int idUsuario);

		List<int> GetYearsForChart(int idUsuario);

		int? GetLastID();
	}
}
