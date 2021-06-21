using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFerMex.Models
{
    public class prodCarro
    {
        private contextFerMex db = new contextFerMex();
        private List<Producto> productos;

        public prodCarro()
        {
            productos = db.Producto.ToList();
        }

        public List<Producto> findAll()
        {
            return this.productos;
        }

        public Producto find(int id)
        {
            Producto pp = this.productos.Single(p => p.ID_PRODUCTO.Equals(id));
            return pp;
        }

    }
}