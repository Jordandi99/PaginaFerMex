//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoFerMex.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ordenProducto
    {
        public int ID_ORDEN_CLIENTE { get; set; }
        public int ID_PRODUCTO { get; set; }
        public Nullable<int> CANTIDAD { get; set; }
    
        public virtual ordenCliente ordenCliente { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
