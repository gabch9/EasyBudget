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
    
    public partial class COMERCIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public COMERCIO()
        {
            this.TRANSACCIONES = new HashSet<TRANSACCIONE>();
        }
    
        public int ComercioId { get; set; }
        public int FkPersonaId { get; set; }
        public string ComercioNombre { get; set; }
        public string ComercioUbicacion { get; set; }
    
        public virtual PERSONA PERSONA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRANSACCIONE> TRANSACCIONES { get; set; }
    }
}