using BackEnd.Model;
using System.Collections.Generic;

namespace BackEnd.BLL.Persona
{
	public interface IBLLPersona : IBLLGenerico<PERSONA>
	{
		List<PERSONA> ConsultarPorNombre(string nombre);
        PERSONA GetDetailsWithLoginID(int id);
        int? GetLastID();

    }
}
