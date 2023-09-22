using BackEnd.BLL.Comprobante;
using BackEnd.Model;
using EasyBudget.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EasyBudget.Controllers
{
	[Authorize]
	public class ComprobanteController : Controller
    {
		private IBLLComprobante comprobanteBLL;

		public ComprobanteController()
		{
			comprobanteBLL = new BLLComprobanteImpl();
		}

		public List<ComprobanteViewModel> GetDatos()
		{
			List<ComprobanteViewModel> comprobanteVM = new List<ComprobanteViewModel>();
			List<COMPROBANTE> comprobante = comprobanteBLL.GetAll();
			foreach (var dato in comprobante)
			{
				comprobanteVM.Add(new ComprobanteViewModel
				{
					ComprobanteId = dato.ComprobanteId,
					ComprobanteUrl = dato.ComprobanteUrl,
					FkPersonaId = dato.FkPersonaId
				});
			};
			return comprobanteVM;
		}

		// GET: Comprobante
		public ActionResult Index()
		{
			List<ComprobanteViewModel> lista = GetDatos();
			return View(lista);
		}

		// GET: Comprobante/Details/5
		public ActionResult Details(int id, ComprobanteViewModel comprobante)
		{
			return View(comprobante.ComprobanteData(comprobanteBLL.Get(id)));
		}

		// GET: Comprobante/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Comprobante/Create
		[HttpPost]
		public ActionResult Create(ComprobanteViewModel comprobante)
		{
			try
			{
				comprobanteBLL.Add(comprobante.Datos());
				return RedirectToAction("index", "Comprobante");
			}
			catch
			{
				return View();
			}
		}

		// GET: Comprobante/Edit/5
		public ActionResult Edit(int id, ComprobanteViewModel comprobante)
		{
			return View(comprobante.ComprobanteData(comprobanteBLL.Get(id)));
		}

		// POST: Comprobante/Edit/5
		[HttpPost]
		public ActionResult Edit(ComprobanteViewModel comprobante)
		{
			try
			{
				comprobanteBLL.Update(comprobante.Datos());
				return RedirectToAction("Details", new { id = comprobante.ComprobanteId });
			}
			catch
			{
				return View();
			}
		}

		// GET: Comprobante/Delete/5
		public ActionResult Delete(int id)
		{
			COMPROBANTE comprobante = comprobanteBLL.Get(id);
			comprobanteBLL.Remove(comprobante);
			return RedirectToAction("index");
		}
	}
}