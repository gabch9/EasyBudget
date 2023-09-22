using BackEnd.BLL.Persona;
using BackEnd.BLL.Presupuesto;
using BackEnd.Model;
using EasyBudget.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EasyBudget.Controllers
{
	public class PersonaController : Controller
    {
		private IBLLPersona personasBLL;
		private IBLLPresupuesto presupuestoBLL;

		public PersonaController()
		{
			personasBLL = new BLLPersonaImpl();
			presupuestoBLL = new BLLPresupuestoImpl();
		}

		public List<PersonasViewModel> GetDatos()
		{
			List<PersonasViewModel> personasVM = new List<PersonasViewModel>();
			List<PERSONA> personas = personasBLL.GetAll();
			foreach (var dato in personas)
			{
				personasVM.Add(new PersonasViewModel
				{
					PersonaId = dato.PersonaId,
					PersonaNombre = dato.PersonaNombre,
					PersonaApellido = dato.PersonaApellido,
					PersonaOcupacion = dato.PersonaOcupacion,
					PersonaFechaNacimiento = dato.PersonaFechaNacimiento,
					PersonaPais = dato.PersonaPais,
					FkLoginId = dato.FkLoginId
				});
			};
			return personasVM;
		}

		[Authorize(Roles = "Administrador")]
		// GET: Persona
		public ActionResult Index()
		{
			List<PersonasViewModel> lista = GetDatos();
			return View(lista);
		}

		[Authorize]
		// GET: Persona/Edit/5
		public ActionResult Edit(int id, PersonasViewModel personas)
		{
			return View(personas.PersonaData(personasBLL.Get(id)));
		}

		// POST: Persona/Edit/5
		[HttpPost]
		public ActionResult Edit(PersonasViewModel personas)
		{
			try
			{
				int idUsuario = (int)Session["userID"];

				personas.FkLoginId = idUsuario;
				personasBLL.Update(personas.Datos());
				return RedirectToAction("Perfil");
			}
			catch
			{
				return View();
			}
		}

		[Authorize]
		// GET: Persona/Delete/5
		public ActionResult Delete(int id)
		{
			PERSONA persona = personasBLL.Get(id);
			personasBLL.Remove(persona);
			return RedirectToAction("index");
		}

		[Authorize]
		public ActionResult Perfil(PersonasViewModel personas)
		{
			int id = (int)Session["userID"];

            ViewBag.Presupuesto = presupuestoBLL.GetBudgetForUser((int)Session["userID"]);

            return View(personas.PersonaData(personasBLL.Get(id)));
		}
	}
}