using BackEnd.Model;
using System.ComponentModel.DataAnnotations;

namespace EasyBudget.Models
{
	public class PresupuestoViewModel
	{
		[Key]
		public int PresupuestoId { get; set; }

		[Display(Name = "Persona")]
		public int FkPersonaId { get; set; }

		[Required(ErrorMessage = "Este Campo es Obligatorio")]
		[Display(Name = "Presupuesto")]
		public decimal Salario { get; set; }

        public decimal SalarioInicial { get; set; }

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
		[DataType(DataType.Text)]
		public string Dia { get; set; }

		[Required(ErrorMessage = "Este Campo es Obligatorio")]
		[DataType(DataType.Text)]
		public string Mes { get; set; }

		[Required(ErrorMessage = "Este Campo es Obligatorio")]
		public int Año { get; set; }

		//Metodos para CRUD
		public PRESUPUESTO Datos()
		{
			PRESUPUESTO PresupuestoDB = new PRESUPUESTO
			{
				FkPersonaId = this.FkPersonaId,
				PresupuestoId = this.PresupuestoId,
				Salario = this.Salario,
                SalarioInicial = this.SalarioInicial,
				Dia = this.Dia,
				Mes = this.Mes,
				Año = this.Año
			};

			return PresupuestoDB;
		}

		public PresupuestoViewModel PresupuestoData(PRESUPUESTO presupuesto)
		{
			PresupuestoViewModel presupuestoDB = new PresupuestoViewModel
			{
				FkPersonaId = presupuesto.FkPersonaId,
				PresupuestoId = presupuesto.PresupuestoId,
				Salario = presupuesto.Salario,
                SalarioInicial = (decimal)presupuesto.SalarioInicial,
                Dia = presupuesto.Dia,
				Mes = presupuesto.Mes,
				Año = presupuesto.Año
			};

			return presupuestoDB;
		}
	}
}