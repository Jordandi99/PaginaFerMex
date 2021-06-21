using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoFerMex.Models;

namespace ProyectoFerMex.Controllers
{
    [Authorize]
    public class PagoController : Controller
    {
        private contextFerMex db = new contextFerMex();
        private string NumConfirPago;
        // GET: Pago
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult CrearOrden()
        {
            if (!User.Identity.IsAuthenticated)
            {
                Session["CrearOrden"] = "pend";
                return RedirectToAction("Login", "Account");
            }
            var orden = new ordenCliente();
            return View();
        }
    }
}