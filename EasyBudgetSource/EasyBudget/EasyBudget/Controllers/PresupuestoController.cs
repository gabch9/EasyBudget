using BackEnd.BLL.Presupuesto;
using BackEnd.Model;
using EasyBudget.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EasyBudget.Controllers
{
	[Authorize]
	public class PresupuestoController : Controller
    {
		private IBLLPresupuesto presupuestoBLL;

		public PresupuestoController()
		{
			presupuestoBLL = new BLLPresupuestoImpl();
		}

		public List<PresupuestoViewModel> GetDatos()
		{
			List<PresupuestoViewModel> PresupuestoVM = new List<PresupuestoViewModel>();
			List<PRESUPUESTO> Presupuesto = presupuestoBLL.GetAll();
			foreach (var dato in Presupuesto)
			{
				PresupuestoVM.Add(new PresupuestoViewModel
				{
					PresupuestoId = dato.PresupuestoId,
					Salario = dato.Salario,
					Dia = dato.Dia,
					Mes = dato.Mes,
					Año = dato.Año,
					FkPersonaId = dato.FkPersonaId
				});
			};
			return PresupuestoVM;
		}

		// GET: Presupuesto/Edit/5
		public ActionResult Edit(int id, PresupuestoViewModel presupuesto)
		{
			return View(presupuesto.PresupuestoData(presupuestoBLL.Get(id)));
		}

		// POST: Presupuesto/Edit/5
		[HttpPost]
		public ActionResult Edit(PresupuestoViewModel presupuesto)
		{
			try
			{
				int idUsuario = (int)Session["userID"];
				presupuesto.FkPersonaId = idUsuario;

				presupuesto.Dia = DateTime.Now.Day.ToString();
				presupuesto.Mes = DateTime.Now.ToString("MMMM");
				presupuesto.Año = DateTime.Now.Year;

				presupuestoBLL.Update(presupuesto.Datos());
				return RedirectToAction("Perfil","Persona");
			}
			catch
			{
				return View();
			}
		}

		// GET: Presupuesto/Delete/5
		public ActionResult Delete(int id)
		{
			PRESUPUESTO presupuesto = presupuestoBLL.Get(id);
			presupuestoBLL.Remove(presupuesto);
			return RedirectToAction("index");
		}
	}
}