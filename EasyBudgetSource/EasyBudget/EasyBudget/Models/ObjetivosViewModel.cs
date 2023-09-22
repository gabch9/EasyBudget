using BackEnd.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyBudget.Models
{
	public class ObjetivosViewModel
	{
		[Key]
		public int ObjetivoId { get; set; }

		[Column(TypeName = "date")]
		[Display(Name = "Fecha de Inicio")]
		[DataType(DataType.Date)]
		public System.DateTime ObjetivoFechaInicio { get; set; }

		[Column(TypeName = "date")]
		[Display(Name = "Fecha de Fin")]
		[DataType(DataType.Date)]
		public System.DateTime ObjetivoFechaFin { get; set; }

		[Display(Name = "Descripcion del objetivo")]
		[StringLength(maximumLength: 200, ErrorMessage = "La descripcion del objetivo no debe ser mayor a 200 caracteres")]
		[DataType(DataType.Text)]
		public string ObjetivoDescripcion { get; set; }

		[Display(Name = "Objetivo Completado")]
		public bool ObjetivoCompleto { get; set; }

		[Display(Name = "Monto del Objetivo")]
        [DisplayFormat(DataFormatString = "{0:n0}", ApplyFormatInEditMode = true)]
        public decimal ObjetivoValor { get; set; }

		[Column(TypeName = "date")]
		[Display(Name = "Fecha Modificacion")]
		[DataType(DataType.Date)]
		public System.DateTime ObjetivoFechaModificacion { get; set; }

		[Required]
		[Display(Name = "Persona")]
		public int FkPersonaId { get; set; }

		[Required]
		[Display(Name = "Tipo de Objetivo")]
		public int FkTipoObjetivoId { get; set; }
		public IEnumerable<TipoObjetivoViewModel> TipoObjetivo { get; set; }

        public int? PorcentajeCompletado { get; set; }

        //Metodos para CRUD
        public OBJETIVO Datos()
		{
			OBJETIVO ObjetivoDB = new OBJETIVO
			{
				ObjetivoId = this.ObjetivoId,
				ObjetivoCompleto = this.ObjetivoCompleto,
				ObjetivoDescripcion = this.ObjetivoDescripcion,
				ObjetivoFechaInicio = this.ObjetivoFechaInicio,
				ObjetivoFechaFin = this.ObjetivoFechaFin,
				ObjetivoFechaModificacion = this.ObjetivoFechaModificacion,
				ObjetivoValor = this.ObjetivoValor,
				FkPersonaId = this.FkPersonaId,
				FkTipoObjetivoId = this.FkTipoObjetivoId
			};

			return ObjetivoDB;
		}

		public ObjetivosViewModel ObjetivoData(OBJETIVO objetivos)
		{
			ObjetivosViewModel ObjetivosDB = new ObjetivosViewModel
			{
				ObjetivoId = objetivos.ObjetivoId,
				ObjetivoDescripcion = objetivos.ObjetivoDescripcion,
				ObjetivoCompleto = objetivos.ObjetivoCompleto,
				ObjetivoFechaInicio = objetivos.ObjetivoFechaInicio,
				ObjetivoFechaFin = objetivos.ObjetivoFechaFin,
				ObjetivoFechaModificacion = objetivos.ObjetivoFechaModificacion,
				ObjetivoValor = objetivos.ObjetivoValor,
				FkPersonaId = objetivos.FkPersonaId,
				FkTipoObjetivoId = objetivos.FkTipoObjetivoId
			};

			return ObjetivosDB;
		}
	}
}