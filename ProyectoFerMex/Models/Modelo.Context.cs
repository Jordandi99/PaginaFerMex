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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class contextFerMex : DbContext
    {
        public contextFerMex()
            : base("name=contextFerMex")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<dirEntrega> dirEntrega { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<ordenCliente> ordenCliente { get; set; }
        public virtual DbSet<ordenProducto> ordenProducto { get; set; }
        public virtual DbSet<Paqueteria> Paqueteria { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
    }
}
