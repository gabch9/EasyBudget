using BackEnd.Model;
using System.ComponentModel.DataAnnotations;

namespace EasyBudget.Models
{
	public class TipoObjetivoViewModel
	{
		[Key]
		public int TipoObjetivoId { get; set; }

		[Required(ErrorMessage = "Este Campo es Obligatorio")]
		[Display(Name = "Descripción")]
		[StringLength(maximumLength: 150, ErrorMessage = "La descripción no debe ser mayor a 150 caracteres")]
		[DataType(DataType.Text)]
		public string TipoObjetivoDescripcion { get; set; }

		//Metodos para CRUD
		public TIPOOBJETIVO Datos()
		{
			TIPOOBJETIVO TipoObjetivoDB = new TIPOOBJETIVO
			{
				TipoObjetivoId = this.TipoObjetivoId,
				TipoObjetivoDescripcion = this.TipoObjetivoDescripcion
			};

			return TipoObjetivoDB;
		}

		public TipoObjetivoViewModel Datos(TIPOOBJETIVO tipoObjetivos)
		{
			TipoObjetivoViewModel TipoObjetivosDB = new TipoObjetivoViewModel
			{
				TipoObjetivoId = tipoObjetivos.TipoObjetivoId,
				TipoObjetivoDescripcion = tipoObjetivos.TipoObjetivoDescripcion
			};

			return TipoObjetivosDB;
		}
	}
}