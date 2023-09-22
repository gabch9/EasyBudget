using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyBudget.Models
{
	public class TransaccionViewModel
	{
		[Key]
		public int TransaccionId { get; set; }

		[Required(ErrorMessage = "Este Campo es Obligatorio")]
		[Display(Name = "Descripción de la transacción")]
		[StringLength(maximumLength: 150, ErrorMessage = "La descripción no debe ser mayor a 150 caracteres")]
		[DataType(DataType.Text)]
		public string TransaccionDescripcion { get; set; }

		[Required(ErrorMessage = "Este Campo es Obligatorio")]
		[Display(Name = "Monto")]
		public decimal TransaccionValor { get; set; }

		[Display(Name = "Fecha de Transacción")]
		[DataType(DataType.Date)]
		public System.DateTime TransaccionFecha { get; set; }

		[Display(Name = "Fecha Modificación")]
		[DataType(DataType.Date)]
		public System.DateTime TransaccionFechaModificacion { get; set; }

		[Display(Name = "Comercio")]
		public Nullable<int> FkIdComercio { get; set; }
		public IEnumerable<ComerciosViewModel> Comercios { get; set; }

		[Required]
		[Display(Name = "Persona")]
		public int FkIdPersona { get; set; }

		[Required]
		[Display(Name = "Comprobante")]
		public int FkIdComprobante { get; set; }

		[Required]
		[Display(Name = "Tipo de Transacción")]
		public int FkIdTipoTransaccion { get; set; }
		public IEnumerable<TipoTransaccionViewModel> TipoTransaccion { get; set; }

		//Metodos para CRUD
		public TRANSACCIONE Datos()
		{
			TRANSACCIONE TransaccionDB = new TRANSACCIONE
			{
				TransaccionId = this.TransaccionId,
				TransaccionDescripcion = this.TransaccionDescripcion,
				TransaccionFecha = this.TransaccionFecha,
				TransaccionFechaModificacion = this.TransaccionFechaModificacion,
				TransaccionValor = this.TransaccionValor,
				FkIdPersona = this.FkIdPersona,
				FkIdComercio = this.FkIdComercio,
				FkIdComprobante = this.FkIdComprobante,
				FkIdTipoTransaccion = this.FkIdTipoTransaccion
			};

			return TransaccionDB;
		}

		public TransaccionViewModel TransaccionData(TRANSACCIONE transaccion)
		{
			TransaccionViewModel TransaccionDB = new TransaccionViewModel
			{
				TransaccionId = transaccion.TransaccionId,
				TransaccionDescripcion = transaccion.TransaccionDescripcion,
				TransaccionFecha = transaccion.TransaccionFecha,
				TransaccionFechaModificacion = transaccion.TransaccionFechaModificacion,
				TransaccionValor = transaccion.TransaccionValor,
				FkIdComercio = transaccion.FkIdComercio,
				FkIdComprobante = transaccion.FkIdComprobante,
				FkIdTipoTransaccion = transaccion.FkIdTipoTransaccion,
				FkIdPersona = transaccion.FkIdPersona
			};

			return TransaccionDB;
		}
	}
}