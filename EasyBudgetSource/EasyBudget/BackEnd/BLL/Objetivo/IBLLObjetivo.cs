using BackEnd.Model;
using System.Collections.Generic;

namespace BackEnd.BLL.Objetivo
{
	public interface IBLLObjetivo : IBLLGenerico<OBJETIVO>
	{
		List<OBJETIVO> GetObjectivesForSingleUser(int id);
        int GetCompletedPercentage(int id, int presupuesto);
    }
}
