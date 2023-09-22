using BackEnd.BLL.Objetivo;
using BackEnd.BLL.Presupuesto;
using BackEnd.BLL.Transaccion;
using BackEnd.Model;
using EasyBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EasyBudget.Controllers
{
	[Authorize]
    public class DashboardController : Controller
    {
        private IBLLPresupuesto presupuestoBLL;
        private IBLLObjetivo objetivosBLL;
        private IBLLTransacciones bllTransacciones;

        private IEnumerable<PRESUPUESTO> lista;
        private IEnumerable<OBJETIVO> Objetivos;
        private IEnumerable<TRANSACCIONE> Transacciones;

        public DashboardController()
        {
            presupuestoBLL = new BLLPresupuestoImpl();
            objetivosBLL = new BLLObjetivoImpl();
            bllTransacciones = new BLLTransaccionesImpl();
        }

        // GET: Dashboard
        public ActionResult Control()
        {
            int idUsuario = int.Parse(Session["userID"].ToString());

            Objetivos = objetivosBLL.GetObjectivesForSingleUser(idUsuario);
            Transacciones = bllTransacciones.GetAllTransactions(idUsuario);

            IBLLTransacciones transacciones = new BLLTransaccionesImpl();
            List<object> ObjetivoList = new List<object>();
            List<object> listaTransacciones = new List<object>();

			decimal presupuesto = presupuestoBLL.GetBudgetForUser(idUsuario).Salario;
            ViewBag.Presupuesto = presupuesto;

			foreach (var item in Objetivos)
            {
                if (item.ObjetivoCompleto == false)
                {
                    ObjetivosViewModel objetivosView = new ObjetivosViewModel();
                    objetivosView = objetivosView.ObjetivoData(item);
                    objetivosView.PorcentajeCompletado = objetivosBLL.GetCompletedPercentage(objetivosView.ObjetivoId, (int)presupuesto);
                    ObjetivoList.Add(objetivosView);
				}		
			}

            foreach (var item in Transacciones)
            {
                listaTransacciones.Add(item);
            }

            ViewBag.Objetivo = ObjetivoList;
            ViewBag.Transacciones = listaTransacciones;

            return View();
        }

        public ActionResult Resumen()
        {
			
			lista = presupuestoBLL.GetAll();

            int idUsuario = int.Parse(Session["userID"].ToString());

            var listaSalarios = lista.Distinct();

            var salario = presupuestoBLL.GetSalariesForChart(System.DateTime.Today.Year, idUsuario);
            List<int> anios = presupuestoBLL.GetYearsForChart(idUsuario);
            IEnumerable<string> meses = presupuestoBLL.GetMonthsForChart(System.DateTime.Today.Year, idUsuario);

			ViewBag.Meses = meses;
			ViewBag.Anios = anios;
			ViewBag.Salarios = salario;
			ViewBag.Anio = (System.DateTime.Today.Year);

			var P = presupuestoBLL.GetBudgetForUser(idUsuario).Salario;
			var I = bllTransacciones.GetIncomeForAllMonths(idUsuario);
			var G = bllTransacciones.GetExpensesForAllMonths(idUsuario);

			ViewBag.Presupuesto = P;
			ViewBag.Ingresos = I;
            ViewBag.Gastos = G;
            ViewBag.Movimientos = bllTransacciones.GetAllTransactions(idUsuario).Count;			

			return View();
        }

        [HttpPost]
        public JsonResult CambiarAnio(string anio)
        {
            int idUsuario = int.Parse(Session["userID"].ToString());

            //obtiene el numero de meses registrados con transacciones
            IEnumerable<string> meses = presupuestoBLL.GetMonthsForChart(int.Parse(anio), idUsuario);

            //obtiene el salario de cada mes
            var salario = presupuestoBLL.GetSalariesForChart(int.Parse(anio), idUsuario);

            //lista que se va a convertir en json para ser retornada. Tambien va a contener los datos
            List<object> listaRetornar = new List<object>();

            //se agregan ambas listas.
            listaRetornar.Add(meses);
            listaRetornar.Add(salario);

            return Json(listaRetornar, JsonRequestBehavior.AllowGet);
        }
    }
}