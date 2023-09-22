using BackEnd.BLL.Login;
using BackEnd.BLL.Persona;
using BackEnd.BLL.Presupuesto;
using EasyBudget.Models;
using System;
using System.Web.Mvc;

namespace EasyBudget.Controllers
{
	public class RegistroController : Controller
    {
		IBLLLogin loginBLL;
		IBLLPersona personaBLL;
		IBLLPresupuesto presupuestoBLL;

		public RegistroController()
		{
			loginBLL = new BLLLoginImpl();
			personaBLL = new BLLPersonaImpl();
			presupuestoBLL = new BLLPresupuestoImpl();
		}

		// GET: Cuenta
		public ActionResult Registro()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Registro(RegistroViewModel cuenta)
		{
			PersonasViewModel persona = cuenta.persona;

			UserViewModel usuario = cuenta.users;

			PresupuestoViewModel presupuesto = cuenta.presupuesto;
			presupuesto.PresupuestoId = (int)presupuestoBLL.GetLastID() + 1;
            presupuesto.SalarioInicial = presupuesto.Salario;

			DateTime fecha = DateTime.Now;
			int week =  System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(fecha, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

			presupuesto.Dia = DateTime.Now.Day.ToString();
			presupuesto.Mes = DateTime.Now.ToString("MMMM");
			presupuesto.Año = DateTime.Now.Year;

			loginBLL.CreateUser(usuario.Datos());

			persona.FkLoginId = loginBLL.GetLastID();
			
            personaBLL.Add(persona.Datos());

            presupuesto.FkPersonaId = (int)personaBLL.GetLastID();
            presupuestoBLL.Add(presupuesto.Datos());

			return RedirectToAction("Index", "Login");
		}
	}
}