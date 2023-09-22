using BackEnd.Model;
using System.ComponentModel.DataAnnotations;

namespace EasyBudget.Models
{
	public class ComerciosViewModel
	{
		[Key]
		public int ComercioId { get; set; }

		[Required(ErrorMessage = "Este Campo es Obligatorio")]
		[Display(Name = "Nombre del Comercio")]
		[StringLength(maximumLength: 50, ErrorMessage = "El nombre no debe ser mayor a 50 caracteres")]
		[DataType(DataType.Text)]
		public string ComercioNombre { get; set; }

		[Required(ErrorMessage = "Este Campo es Obligatorio")]
		[Display(Name = "Ubicación del Comercio")]
		[StringLength(maximumLength: 50, ErrorMessage = "La ubicación no debe ser mayor a 50 caracteres")]
		[DataType(DataType.Text)]
		public string ComercioUbicacion { get; set; }

		[Required]
		[Display(Name = "Persona")]
		public int FkPersonaId { get; set; }

		//Metodos para CRUD
		public COMERCIO Datos()
		{
			COMERCIO ComercioDB = new COMERCIO
			{
				ComercioId = this.ComercioId,
				ComercioNombre = this.ComercioNombre,
				ComercioUbicacion = this.ComercioUbicacion,
				FkPersonaId = this.FkPersonaId
			};

			return ComercioDB;
		}

		public ComerciosViewModel ComercioData(COMERCIO comercio)
		{
			ComerciosViewModel ComercioDB = new ComerciosViewModel
			{
				ComercioId = comercio.ComercioId,
				ComercioNombre = comercio.ComercioNombre,
				ComercioUbicacion = comercio.ComercioUbicacion,
				FkPersonaId = comercio.FkPersonaId
			};

			return ComercioDB;
		}
	}
}