using BackEnd.Model;
using System.ComponentModel.DataAnnotations;

namespace EasyBudget.Models
{
	public class TipoTransaccionViewModel
	{
		[Key]
		public int TipoTransaccionId { get; set; }

		[Required(ErrorMessage = "Este Campo es Obligatorio")]
		[Display(Name = "Descripción")]
		[StringLength(maximumLength: 150, ErrorMessage = "La descripción no debe ser mayor a 150 caracteres")]
		[DataType(DataType.Text)]
		public string TransaccionDescripcion { get; set; }

		//Metodos para CRUD
		public TIPOTRANSACCION Datos()
		{
			TIPOTRANSACCION TipoTransaccionDB = new TIPOTRANSACCION
			{
				TipoTransaccionId = this.TipoTransaccionId,
				TransaccionDescripcion = this.TransaccionDescripcion
			};

			return TipoTransaccionDB;
		}

		public TipoTransaccionViewModel TipoTransaccionDatos(TIPOTRANSACCION tipoTransaccion)
		{
			TipoTransaccionViewModel TipoTransaccionDB = new TipoTransaccionViewModel
			{
				TipoTransaccionId = tipoTransaccion.TipoTransaccionId,
				TransaccionDescripcion = tipoTransaccion.TransaccionDescripcion
			};

			return TipoTransaccionDB;
		}
	}
}