using BackEnd.Model;
using System.Collections.Generic;

namespace BackEnd.BLL.Transaccion
{
	public interface IBLLTransacciones : IBLLGenerico<TRANSACCIONE>
	{
		decimal GetExpensesForAllMonths(int id);
		decimal GetIncomeForAllMonths(int id);
		List<TRANSACCIONE> GetAllTransactions(int id);
	}
}
