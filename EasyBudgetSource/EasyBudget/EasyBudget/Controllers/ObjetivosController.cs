using BackEnd.BLL.Objetivo;
using BackEnd.BLL.TipoObjetivo;
using BackEnd.Model;
using EasyBudget.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EasyBudget.Controllers
{
	[Authorize]
	public class ObjetivosController : Controller
    {
		private IBLLObjetivo objetivoBLL;
		private IBLLTipoObjetivo tipoobjetivoBLL;

		public ObjetivosController()
		{
			objetivoBLL = new BLLObjetivoImpl();
			tipoobjetivoBLL = new BLLTipoObjetivoImpl();
		}

		public List<TipoObjetivoViewModel> TipoObjetivoData()
		{
			List<TipoObjetivoViewModel> TipoObjetivoVM = new List<TipoObjetivoViewModel>();
			List<TIPOOBJETIVO> tipoTransaccion = tipoobjetivoBLL.GetAll();
			foreach (var dato in tipoTransaccion)
			{
				TipoObjetivoVM.Add(new TipoObjetivoViewModel
				{
					TipoObjetivoId = dato.TipoObjetivoId,
					TipoObjetivoDescripcion = dato.TipoObjetivoDescripcion
				});
			};
			return TipoObjetivoVM;
		}

		public List<ObjetivosViewModel> GetDatos()
		{
            int idUsuario = (int)Session["userID"];
            List<ObjetivosViewModel> objetivosVM = new List<ObjetivosViewModel>();
			List<OBJETIVO> objetivos = objetivoBLL.GetObjectivesForSingleUser(idUsuario);
			foreach (var dato in objetivos)
			{
				objetivosVM.Add(new ObjetivosViewModel
				{
					ObjetivoId = dato.ObjetivoId,
					ObjetivoCompleto = dato.ObjetivoCompleto,
					ObjetivoDescripcion = dato.ObjetivoDescripcion,
					ObjetivoFechaInicio = dato.ObjetivoFechaInicio,
					ObjetivoFechaFin = dato.ObjetivoFechaFin,
					ObjetivoFechaModificacion = dato.ObjetivoFechaModificacion,
					ObjetivoValor = dato.ObjetivoValor,
					FkPersonaId = dato.FkPersonaId,
					FkTipoObjetivoId = dato.FkTipoObjetivoId
				});
			};
			return objetivosVM;
		}

		// GET: Objetivos
		public ActionResult Index()
		{
			List<ObjetivosViewModel> lista = GetDatos();
			return View(lista);
		}

		// GET: Objetivos/Create
		public ActionResult Create()
		{
			ObjetivosViewModel objetivo = new ObjetivosViewModel();
			objetivo.TipoObjetivo = TipoObjetivoData();
			return View(objetivo);
		}

		// POST: Objetivos/Create
		[HttpPost]
		public ActionResult Create(ObjetivosViewModel objetivos)
		{
			try
			{
				int idUsuario = (int)Session["userID"];

				objetivos.FkPersonaId = idUsuario;
				objetivoBLL.Add(objetivos.Datos());
				return RedirectToAction("index", "Objetivos");
			}
			catch
			{
				return View();
			}
		}

		// GET: Objetivos/Edit/5
		public ActionResult Edit(int id, ObjetivosViewModel objetivos)
		{
            ObjetivosViewModel objetivoVM = objetivos.ObjetivoData(objetivoBLL.Get(id));
            objetivoVM.TipoObjetivo = TipoObjetivoData();
            return View(objetivoVM);
		}

		// POST: Objetivos/Edit/5
		[HttpPost]
		public ActionResult Edit(ObjetivosViewModel objetivos)
		{
			try
			{
				int idUsuario = (int)Session["userID"];

				objetivos.FkPersonaId = idUsuario;
				objetivoBLL.Update(objetivos.Datos());
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Objetivos/Delete/5
		public ActionResult Delete(int id)
		{
			OBJETIVO objetivo = objetivoBLL.Get(id);
			objetivoBLL.Remove(objetivo);
			return RedirectToAction("index");
		}
	}
}