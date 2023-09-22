using System.Collections.Generic;
using System.Linq;
using BackEnd.DAL;
using BackEnd.Model;

namespace BackEnd.BLL.Objetivo
{
	public class BLLObjetivoImpl : BLLGenericoImpl<OBJETIVO>, IBLLObjetivo
	{
		private UnidadDeTrabajo<OBJETIVO> unidad;

		public BLLObjetivoImpl()
		{
			EasyBudgetContext contexto = new EasyBudgetContext();
			unidad = new UnidadDeTrabajo<OBJETIVO>(contexto);
		}

		public List<OBJETIVO> GetObjectivesForSingleUser(int id)
		{
			List<OBJETIVO> lista = unidad.genericDAL.GetAll().Where(x => x.FkPersonaId == id).ToList();

			return lista;
		}

        public int GetCompletedPercentage(int id, int presupuesto)
        {
            OBJETIVO objetivo = unidad.genericDAL.Get(id);
            decimal? porcentaje;
            if (presupuesto <= objetivo.ObjetivoValor)
            {
                porcentaje = 100 * (presupuesto / objetivo.ObjetivoValor);
            }
            else if(presupuesto < 0)
            {
                porcentaje = 0;
            }
            else
            {
                porcentaje = 100;
            }
            
            return (int)porcentaje;
        }
	}
}
