using BackEnd.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace EasyBudget.Models
{
	public class PersonasViewModel
	{
		[Key]
		public int PersonaId { get; set; }

		[Required(ErrorMessage = "Este Campo es Obligatorio")]
		[Display(Name = "Nombre")]
		[StringLength(maximumLength: 50, ErrorMessage = "El nombre no debe ser mayor a 50 caracteres")]
		[DataType(DataType.Text)]
		public string PersonaNombre { get; set; }

		[Required(ErrorMessage = "Este Campo es Obligatorio")]
		[Display(Name = "Apellido")]
		[StringLength(maximumLength: 50, ErrorMessage = "El apellido no debe ser mayor a 50 caracteres")]
		[DataType(DataType.Text)]
		public string PersonaApellido { get; set; }

		[Required(ErrorMessage = "Este Campo es Obligatorio")]
		[Display(Name = "Fecha de Nacimiento")]
		[DataType(DataType.Date)]
		public System.DateTime PersonaFechaNacimiento { get; set; }

		[Display(Name = "Ocupación")]
		[StringLength(maximumLength: 50, ErrorMessage = "El ocupación no debe ser mayor a 50 caracteres")]
		[DataType(DataType.Text)]
		public string PersonaOcupacion { get; set; }

		[Required(ErrorMessage = "Este Campo es Obligatorio")]
		[Display(Name = "País")]
		[StringLength(maximumLength: 50, ErrorMessage = "El país no debe ser mayor a 50 caracteres")]
		[DataType(DataType.Text)]
		public string PersonaPais { get; set; }

		[Display(Name = "Login ID")]
		public Nullable<int> FkLoginId { get; set; }


		//Metodos para CRUD
		public PERSONA Datos()
		{
			PERSONA PersonaDB = new PERSONA
			{
				PersonaId = this.PersonaId,
				PersonaNombre = this.PersonaNombre,
				PersonaApellido = this.PersonaApellido,
				PersonaOcupacion = this.PersonaOcupacion,
				PersonaPais = this.PersonaPais,
				PersonaFechaNacimiento = this.PersonaFechaNacimiento,
				FkLoginId = this.FkLoginId
			};

			return PersonaDB;
		}

		public PersonasViewModel PersonaData(PERSONA persona)
		{
			PersonasViewModel PersonaDB = new PersonasViewModel
			{
				PersonaId = persona.PersonaId,
				PersonaNombre = persona.PersonaNombre,
				PersonaApellido = persona.PersonaApellido,
				PersonaOcupacion = persona.PersonaOcupacion,
				PersonaPais = persona.PersonaPais,
				PersonaFechaNacimiento = persona.PersonaFechaNacimiento,
				FkLoginId = persona.FkLoginId
			};

			return PersonaDB;
		}
	}
}