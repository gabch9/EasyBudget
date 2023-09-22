using BackEnd.BLL.Comercio;
using BackEnd.BLL.Comprobante;
using BackEnd.BLL.Persona;
using BackEnd.BLL.Presupuesto;
using BackEnd.BLL.TipoTransaccion;
using BackEnd.BLL.Transaccion;
using BackEnd.Model;
using EasyBudget.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EasyBudget.Controllers
{
	[Authorize]
	public class TransaccionController : Controller
    {
		private IBLLTransacciones transaccionBLL;
		private IBLLComercio comercioBLL;
		private IBLLComprobante comprobanteBLL;
		private IBLLTipoTransaccion tipotransaccionBLL;
		private IBLLPresupuesto presupuestoBLL;

		public TransaccionController()
		{
			transaccionBLL = new BLLTransaccionesImpl();
			comercioBLL = new BLLComercioImpl();
			comprobanteBLL = new BLLComprobanteImpl();
			tipotransaccionBLL = new BLLTipoTransaccionImpl();
			presupuestoBLL = new BLLPresupuestoImpl();
		}

		public List<ComerciosViewModel> ComerciosData()
		{
			List<ComerciosViewModel> ComerciosVM = new List<ComerciosViewModel>();
			List<COMERCIO> comercios = comercioBLL.GetAll();
			foreach (var dato in comercios)
			{
				ComerciosVM.Add(new ComerciosViewModel
				{
					ComercioId = dato.ComercioId,
					ComercioNombre = dato.ComercioNombre
				});
			};
			return ComerciosVM;
		}

		public List<TipoTransaccionViewModel> TipoTransaccionData()
		{
			List<TipoTransaccionViewModel> TipoTransaccionVM = new List<TipoTransaccionViewModel>();
			List<TIPOTRANSACCION> tipoTransaccion = tipotransaccionBLL.GetAll();
			foreach (var dato in tipoTransaccion)
			{
				TipoTransaccionVM.Add(new TipoTransaccionViewModel
				{
					TipoTransaccionId = dato.TipoTransaccionId,
					TransaccionDescripcion = dato.TransaccionDescripcion
				});
			};
			return TipoTransaccionVM;
		}

		public List<TransaccionViewModel> GetDatos()
		{
            int idUsuario = (int)Session["userID"];
            List<TransaccionViewModel> transaccionVM = new List<TransaccionViewModel>();
			List<TRANSACCIONE> transaccion = transaccionBLL.GetAllTransactions(idUsuario);
			foreach (var dato in transaccion)
			{
				transaccionVM.Add(new TransaccionViewModel
				{
					TransaccionId = dato.TransaccionId,
					TransaccionDescripcion = dato.TransaccionDescripcion,
					TransaccionFecha = dato.TransaccionFecha,
					TransaccionFechaModificacion = dato.TransaccionFechaModificacion,
					TransaccionValor = dato.TransaccionValor,
					FkIdTipoTransaccion = dato.FkIdTipoTransaccion,
					FkIdComprobante = dato.FkIdComprobante,
					FkIdComercio = dato.FkIdComercio,
					FkIdPersona = dato.FkIdPersona
				});
			};
			return transaccionVM;
		}

		// GET: Transaccion
		public ActionResult Index()
		{
			List<TransaccionViewModel> lista = GetDatos();
			return View(lista);
		}

		// GET: Transaccion/Details/5
		public ActionResult Details(int id, TransaccionViewModel transaccion)
		{
			return View(transaccion.TransaccionData(transaccionBLL.Get(id)));
		}

		// GET: Transaccion/Create
		public ActionResult Create()
		{
			TransaccionViewModel transaccion = new TransaccionViewModel();	
			transaccion.TipoTransaccion = TipoTransaccionData();
			transaccion.Comercios = ComerciosData();

			return View(transaccion);
		}

		// POST: Transaccion/Create
		[HttpPost]
		public ActionResult Create(TransaccionViewModel transaccion)
		{
			try
			{
				int idUsuario = (int)Session["userID"];

				ComprobanteViewModel comprobante = new ComprobanteViewModel();
				
				comprobante.FkPersonaId = idUsuario;
				comprobanteBLL.Add(comprobante.Datos());
				
				transaccion.FkIdPersona = idUsuario;
				transaccion.FkIdComprobante = comprobanteBLL.GetLastID();
				transaccionBLL.Add(transaccion.Datos());

                //Despues de que se inserta la transaccion se actualiza el presupuesto
				PresupuestoUpdate(transaccion.FkIdTipoTransaccion, transaccion.TransaccionValor);

				return RedirectToAction("index", "Transaccion");
			}
			catch
			{
				return View();
			}
		}

		/*Metodo Para Actualizar el Presupuesto segun los gastos e ingresos del Usuario*/
		public void PresupuestoUpdate(int tipotransaccion, decimal valor)
		{
			PresupuestoViewModel presupuesto = new PresupuestoViewModel();

			int idUsuario = (int)Session["userID"];
			var P = presupuestoBLL.GetBudgetForUser(idUsuario).Salario;

			presupuesto.PresupuestoId = presupuestoBLL.GetBudgetForUser(idUsuario).PresupuestoId;
            presupuesto.SalarioInicial = (decimal)presupuestoBLL.GetBudgetForUser(idUsuario).SalarioInicial;
            presupuesto.FkPersonaId = idUsuario;
			presupuesto.Dia = DateTime.Now.Day.ToString();
			presupuesto.Mes = DateTime.Now.ToString("MMMM");
			presupuesto.Año = DateTime.Now.Year;

			if (tipotransaccion == 3)
			{
				presupuesto.Salario = P + valor;
			}
			else if (tipotransaccion == 2)
			{
				presupuesto.Salario = P - valor;
			}
			
			presupuestoBLL.Update(presupuesto.Datos());
		}	

		// GET: Transaccion/Edit/5
		public ActionResult Edit(int id, TransaccionViewModel transaccion)
		{
			return View(transaccion.TransaccionData(transaccionBLL.Get(id)));
		}

		// POST: Transaccion/Edit/5
		[HttpPost]
		public ActionResult Edit(TransaccionViewModel transaccion)
		{
			try
			{
				transaccionBLL.Update(transaccion.Datos());
				return RedirectToAction("Details", new { id = transaccion.TransaccionId });
			}
			catch
			{
				return View();
			}
		}

		// GET: Transaccion/Delete/5
		public ActionResult Delete(int id)
		{
			TRANSACCIONE transaccion = transaccionBLL.Get(id);
			transaccionBLL.Remove(transaccion);
			return RedirectToAction("index");
		}
	}
}