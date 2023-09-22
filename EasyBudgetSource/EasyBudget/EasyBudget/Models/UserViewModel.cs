using BackEnd.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyBudget.Models
{
	public class UserViewModel
	{
		public int UserViewModelId { get; set; }
		[Required(ErrorMessage = "Debe ingresar un Nombre de Usuario.")]
		[Display(Name = "Usuario")]
		public string UserName { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Debe ingresar una Contraseña.")]
		[Display(Name = "Contraseña")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Este Campo es Obligatorio")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Las Contraseñas no son iguales.")]
		[Display(Name = "Confirmar Contraseña")]
		public string ConfirmLoginPassword { get; set; }

		public string LoginErrorMessage { get; set; }
		public virtual ICollection<ROLE> Roles { get; set; }

		//Metodos para CRUD
		public LOGIN Datos()
		{
			LOGIN Login = new LOGIN
			{
				LoginPassword = this.Password,
				LoginUsername = this.UserName
			};

			return Login;
		}

		public UserViewModel Datos(LOGIN login)
		{
			UserViewModel LoginDB = new UserViewModel
			{
				Password = login.LoginPassword,
				UserName = login.LoginUsername
			};

			return LoginDB;
		}

	}
}