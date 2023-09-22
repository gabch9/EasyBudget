using BackEnd.BLL.Comercio;
using BackEnd.Model;
using EasyBudget.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EasyBudget.Controllers
{
	[Authorize]
	public class ComercioController : Controller
    {
		private IBLLComercio comercioBLL;

		public ComercioController()
		{
			comercioBLL = new BLLComercioImpl();
		}

		public List<ComerciosViewModel> GetDatos()
		{
			List<ComerciosViewModel> proveedoresVM = new List<ComerciosViewModel>();
			List<COMERCIO> proveedores = comercioBLL.GetAll();
			foreach (var dato in proveedores)
			{
				proveedoresVM.Add(new ComerciosViewModel
				{
					ComercioId = dato.ComercioId,
					ComercioNombre = dato.ComercioNombre,
					ComercioUbicacion = dato.ComercioUbicacion,
					FkPersonaId = dato.FkPersonaId
				});
			};
			return proveedoresVM;
		}

		// GET: Proveedores
		public ActionResult Index()
		{
			List<ComerciosViewModel> lista = GetDatos();
			return View(lista);
		}

		// GET: Create Proveedores View Model
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(ComerciosViewModel comercio)
		{
			int idUsuario = (int)Session["userID"];

			comercio.FkPersonaId = idUsuario;
			comercioBLL.Add(comercio.Datos());
			return RedirectToAction("index", "Comercio");
		}

		// GET: Edit Proveedores
		public ActionResult Edit(int id, ComerciosViewModel comercio)
		{
			return View(comercio.ComercioData(comercioBLL.Get(id)));
		}

		[HttpPost]
		public ActionResult Edit(ComerciosViewModel comercio)
		{
			int idUsuario = (int)Session["userID"];

			comercio.FkPersonaId = idUsuario;
			comercioBLL.Update(comercio.Datos());
			return RedirectToAction("index", "Comercio");
		}

		// GET: Delete Proveedores
		public ActionResult Delete(int id)
		{
			COMERCIO comercio = comercioBLL.Get(id);
			comercioBLL.Remove(comercio);
			return RedirectToAction("index");
		}
	}
}