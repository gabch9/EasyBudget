using BackEnd.BLL.Transaccion;
using BackEnd.DAL;
using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace BackEnd.BLL.Presupuesto
{
    public class BLLPresupuestoImpl : BLLGenericoImpl<PRESUPUESTO>, IBLLPresupuesto
    {
        private UnidadDeTrabajo<PRESUPUESTO> unidad;
        private IBLLTransacciones BLLTransacciones;
        public BLLPresupuestoImpl()
        {
            EasyBudgetContext contexto = new EasyBudgetContext();
            unidad = new UnidadDeTrabajo<PRESUPUESTO>(contexto);
            BLLTransacciones = new BLLTransaccionesImpl();
        }

        public int? GetLastID()
        {
            try
            {
                int? resultado;

                resultado = int.Parse(GetAll().Max(x => x.PresupuestoId).ToString());

                return resultado;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public List<PRESUPUESTO> ConsultarPorId(string id)
        {
            try
            {
                List<PRESUPUESTO> resultado;
                using (unidad = new UnidadDeTrabajo<PRESUPUESTO>(new EasyBudgetContext()))
                {
                    Expression<Func<PRESUPUESTO, bool>> consulta = (d => d.PresupuestoId.ToString().Contains(id));
                    resultado = unidad.genericDAL.Find(consulta).ToList();
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<string> GetMonthsForChart(int year, int idUsuario)
        {

            IEnumerable<int> listaTransacciones = BLLTransacciones.GetAll().Where(x => x.FkIdPersona == idUsuario && x.TransaccionFecha.Year == year).
                Distinct().OrderBy(x => x.TransaccionFecha).Select(x => x.TransaccionFecha.Month);
            List<string> listaMeses = new List<string>();

            foreach (var item in listaTransacciones)
            {
                listaMeses.Add(MonthName(item));
            }

            return listaMeses;
        }

        public List<decimal> GetSalariesForChart(int year, int idUsuario)
        {
            //Lista que va a guardar todas las transacciones del usuario
            BLLTransacciones = new BLLTransaccionesImpl();

            //busca en la lista el id del usuario y lo ordena por fecha
            List<TRANSACCIONE> listaTransacciones = BLLTransacciones.GetAll().Where(x => x.FkIdPersona == idUsuario).
                OrderBy(x => x.TransaccionFecha).ToList();

            //Variable que va a tener el numero del mes inicial
            int mes = 0;

            //Variable que va a tener el numero del anio inicial
            int anioInicial = 0;

            if (listaTransacciones.Count != 0)
            {
                //busca en la la lista el numero del primer mes, como se ordeno por fecha va y busca el primero
                mes = listaTransacciones.ElementAt(0).TransaccionFecha.Month;

                //busca en la la lista el numero del primer anio, como se ordeno por fecha va y busca el primero
                anioInicial = listaTransacciones.ElementAt(0).TransaccionFecha.Year;
            }
            
            //lista que va a guardar los presupuestos, cada indice representa un mes, ej 0 == enero
            List<decimal> listaPresupuestos = new List<decimal>();

            //variables para guardar el total y calcularlo
            decimal? ingresos = 0;
            decimal? gastos = 0;
            decimal? total;

            
            using (unidad = new UnidadDeTrabajo<PRESUPUESTO>(new EasyBudgetContext()))
            {
                //Busqueda del presupuesto del usuario
                Expression<Func<PRESUPUESTO, bool>> consulta = (l => l.FkPersonaId == idUsuario);
                total = unidad.genericDAL.Find(consulta).FirstOrDefault().SalarioInicial;
            }        

            //For each para ir recorriendo las fechas y las transacciones en dichas fechas
            foreach (var item in listaTransacciones)
            {
                //ya que se organizo por fecha la lista, si hay años anteriores hay que hacer calculos apartes para mantener continuidad
                //Aqui se pregunta si el item tiene el mismo año con el que se inicializo el resumes, ej: si el usuario abre el grafico el
                //2018, aqui year va a ser 2018, si no lo es solo pasa a sumar los ingresos o gastos
                if (item.TransaccionFecha.Year == year)
                {
                    if (anioInicial != year)
                    {
                        anioInicial = year;
                        mes = item.TransaccionFecha.Month;
                    }
                    //Se verifica que sea del mismo mes la transaccion para sumarlo despues a los gastos o ingresos de ese mismo mes
                    if (item.TransaccionFecha.Month == mes)
                    {
                        if (item.FkIdTipoTransaccion == 3)
                        {
                            ingresos = ingresos + item.TransaccionValor;
                        }
                        else if (item.FkIdTipoTransaccion == 2)
                        {
                            gastos = gastos + item.TransaccionValor;
                        }

                    }
                    //se llega aqui si el mes no era el mismo
                    else
                    {
                        //se suma todo antes de meterlo a la lista y despues se resetea
                        total = total + (ingresos - gastos);
                        ingresos = 0;
                        gastos = 0;

                        //Esta parte es para identificar una vez mas que transaccion es porque en este punto el objeto que llego aqui es de otro
                        //mes. Si no se hiciera esto los valores de la primera transaccion de ese mes no se contaria
                        if (item.FkIdTipoTransaccion == 3)
                        {
                            ingresos = ingresos + item.TransaccionValor;
                        }
                        else if (item.FkIdTipoTransaccion == 2)
                        {
                            gastos = gastos + item.TransaccionValor;
                        }

                        //Se agrega el total
                        listaPresupuestos.Add((decimal)total);

                        //se asigna el nuevo valor al mes
                        mes = item.TransaccionFecha.Month;
                    }

                }
                else
                {
                    //parar de contar si ya el año es mayor al que se ingreso con el grafico
                    if (item.TransaccionFecha.Year > year)
                    {
                        break;
                    }
                    //se ve el tipo de transaccion
                    if (item.FkIdTipoTransaccion == 3)
                    {
                        ingresos = ingresos + item.TransaccionValor;
                    }
                    else if (item.FkIdTipoTransaccion == 2)
                    {
                        gastos = gastos + item.TransaccionValor;
                    }
                }
            }

            if (ingresos != 0 || gastos != 0)
            {
                total = total + (ingresos - gastos);
                listaPresupuestos.Add((decimal)total);
            }

            return listaPresupuestos;
        }

        private string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(month);
        }

        public PRESUPUESTO GetBudgetForUser(int id)
        {
            PRESUPUESTO budget;

            try
            {
                using (unidad = new UnidadDeTrabajo<PRESUPUESTO>(new EasyBudgetContext()))
                {
                    Expression<Func<PRESUPUESTO, bool>> consulta = (d => d.FkPersonaId == id);
                    budget = unidad.genericDAL.Find(consulta).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return budget;
        }

        public List<int> GetYearsForChart(int idUsuario)
        {
            return BLLTransacciones.GetAll().Where(x => x.FkIdPersona == idUsuario).
            Select(x => x.TransaccionFecha.Year).Distinct().ToList();
        }
    }
}
