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
    using System.ComponentModel.DataAnnotations;

    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            this.ordenProducto = new HashSet<ordenProducto>();
        }
    
        public int ID_PRODUCTO { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string PRECIO { get; set; }
        [Display(Name = "ULTIMA ACTUALIZACION")]
        public Nullable<System.DateTime> ULT_ACTUALIZACION { get; set; }
        public string IMAGEN { get; set; }
        public Nullable<int> STOCK { get; set; }
        [Display(Name = "ID CATEGORIA")]       
        public Nullable<int> ID_CATEGORIA { get; set; }
    
        public virtual Categoria Categoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ordenProducto> ordenProducto { get; set; }
    }
}