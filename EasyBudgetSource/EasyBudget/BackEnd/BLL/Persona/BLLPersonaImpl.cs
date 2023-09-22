using BackEnd.DAL;
using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BackEnd.BLL.Persona
{
	public class BLLPersonaImpl : BLLGenericoImpl<PERSONA>, IBLLPersona
	{
		private UnidadDeTrabajo<PERSONA> unidad;

		public List<PERSONA> ConsultarPorNombre(string nombre)
		{
			try
			{
				List<PERSONA> resultado;
				using (unidad = new UnidadDeTrabajo<PERSONA>(new EasyBudgetContext()))
				{
					Expression<Func<PERSONA, bool>> consulta = (d => d.PersonaNombre.Contains(nombre));
					resultado = unidad.genericDAL.Find(consulta).ToList();
				}
				return resultado;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

        public PERSONA GetDetailsWithLoginID(int id)
        {
            try
            {
                PERSONA resultado;
                using (unidad = new UnidadDeTrabajo<PERSONA>(new EasyBudgetContext()))
                {
                    Expression<Func<PERSONA, bool>> consulta = (x => x.FkLoginId == id);
                    resultado = unidad.genericDAL.Find(consulta).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int? GetLastID()
        {
            try
            {
                int? resultado;
                resultado = int.Parse(GetAll().Max(x => x.PersonaId).ToString());

                return resultado;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
