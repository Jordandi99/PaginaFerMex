using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoFerMex.Models;

namespace ProyectoFerMex.Controllers
{
    public class CatalogoController : Controller
    {
        private contextFerMex db = new contextFerMex();
        // GET: Catalogo
        public ActionResult Index()
        {
            prodCarro carro = new prodCarro();
            ViewBag.productos = carro.findAll();
            return View();
        }

        public ActionResult prodCategoria(int idCat)
        {
            List<Producto> mercancia = null;
            var query = from p in db.Producto
                        where p.ID_CATEGORIA == idCat
                        select p;
            if (idCat == 1)
            {
                mercancia = query.ToList();
                ViewBag.Catego = "Todos los Fertilizantes organicos";
            }
            if (idCat == 2)
            {
                mercancia = query.ToList();
                ViewBag.Catego = "Todos los Fertilizantes quimicos";
            }
            if (idCat == 3)
            {
                mercancia = query.ToList();
                ViewBag.Catego = "Todos los Biofertilizantes";
            }
            if (idCat == 4)
            {
                mercancia = query.ToList();
                ViewBag.Catego = "Todos los Bioestimulantes";
            }

            ViewBag.produ = mercancia;
            return View();
        }

        [HttpPost]
        public ActionResult BuscaProd(string nomBuscar)
        {
            ViewBag.SearchKey = nomBuscar;
            using (db)
            {
                var query = from st in db.Producto
                            where st.NOMBRE.Contains(nomBuscar)

                            select st;
                var listaProd = query.ToList();
                ViewBag.Listado = listaProd;
            }
            return View();

        }
    }

}