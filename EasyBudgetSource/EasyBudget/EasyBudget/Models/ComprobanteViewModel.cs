using BackEnd.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace EasyBudget.Models
{
	public class ComprobanteViewModel
	{
		[Key]
		public int ComprobanteId { get; set; }

		[Required(ErrorMessage = "Este Campo es Obligatorio")]
		[Display(Name = "URL del Comprobante")]
		[DataType(DataType.Text)]
		public string ComprobanteUrl { get; set; }

		[Required]
		[Display(Name = "Persona")]
		public Nullable<int> FkPersonaId { get; set; }

		//Metodos para CRUD
		public COMPROBANTE Datos()
		{
			COMPROBANTE ComprobanteDB = new COMPROBANTE
			{
				ComprobanteId = this.ComprobanteId,
				ComprobanteUrl = this.ComprobanteUrl,
				FkPersonaId = this.FkPersonaId
			};

			return ComprobanteDB;
		}

		public ComprobanteViewModel ComprobanteData(COMPROBANTE comprobante)
		{
			ComprobanteViewModel ComprobanteDB = new ComprobanteViewModel
			{
				ComprobanteId = comprobante.ComprobanteId,
				ComprobanteUrl = comprobante.ComprobanteUrl,
				FkPersonaId = comprobante.FkPersonaId
			};

			return ComprobanteDB;
		}
	}
}