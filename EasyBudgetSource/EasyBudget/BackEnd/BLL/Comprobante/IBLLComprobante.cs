using BackEnd.Model;

namespace BackEnd.BLL.Comprobante
{
	public interface IBLLComprobante : IBLLGenerico<COMPROBANTE>
	{
		COMPROBANTE GetDetailsWithLoginID(int id);
		int GetLastID();
	}
}
