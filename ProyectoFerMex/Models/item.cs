using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFerMex.Models
{
    public class item
    {
        public Producto Producto
        {
            get;
            set;
        }
        public int Cantidad
        {
            get;
            set;
        }
    }
}