//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BackEnd.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRESUPUESTO
    {
        public int PresupuestoId { get; set; }
        public int FkPersonaId { get; set; }
        public decimal Salario { get; set; }
        public string Mes { get; set; }
        public int Año { get; set; }
        public string Dia { get; set; }
        public Nullable<decimal> SalarioInicial { get; set; }
    
        public virtual PERSONA PERSONA { get; set; }
    }
}
