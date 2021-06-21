using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFerMex.Models;

namespace ProyectoFerMex.Controllers
{
    public class envioController : Controller
    {
        private contextFerMex db = new contextFerMex();

        // GET: envio
        public ActionResult Index()
        {
            var ordenCliente = db.ordenCliente.Include(o => o.Cliente).Include(o => o.Paqueteria);
            return View(ordenCliente.ToList());
        }

        // GET: envio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ordenCliente ordenCliente = db.ordenCliente.Find(id);
            if (ordenCliente == null)
            {
                return HttpNotFound();
            }
            return View(ordenCliente);
        }

        // GET: envio/Create
        public ActionResult Create()
        {
            ViewBag.ID_CLIENTE = new SelectList(db.Cliente, "ID_CLIENTE", "NOMBRE");
            ViewBag.ID_PAQUETERIA = new SelectList(db.Paqueteria, "ID_PAQUETERIA", "NOMBRE");
            return View();
        }

        // POST: envio/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_ORDEN_CLIENTE,FECHA_CREACION,NUM_CONFIRMACION,TOTAL,ID_CLIENTE,ID_DIRENTREGA,ID_PAQUETERIA,NUM_GUIA,FECHA_ENVIO,FECHA_ENTREGA")] ordenCliente ordenCliente)
        {
            if (ModelState.IsValid)
            {
                db.ordenCliente.Add(ordenCliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CLIENTE = new SelectList(db.Cliente, "ID_CLIENTE", "NOMBRE", ordenCliente.ID_CLIENTE);
            ViewBag.ID_PAQUETERIA = new SelectList(db.Paqueteria, "ID_PAQUETERIA", "NOMBRE", ordenCliente.ID_PAQUETERIA);
            return View(ordenCliente);
        }

        // GET: envio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ordenCliente ordenCliente = db.ordenCliente.Find(id);
            if (ordenCliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CLIENTE = new SelectList(db.Cliente, "ID_CLIENTE", "NOMBRE", ordenCliente.ID_CLIENTE);
            ViewBag.ID_PAQUETERIA = new SelectList(db.Paqueteria, "ID_PAQUETERIA", "NOMBRE", ordenCliente.ID_PAQUETERIA);
            return View(ordenCliente);
        }

        // POST: envio/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ORDEN_CLIENTE,FECHA_CREACION,NUM_CONFIRMACION,TOTAL,ID_CLIENTE,ID_DIRENTREGA,ID_PAQUETERIA,NUM_GUIA,FECHA_ENVIO,FECHA_ENTREGA")] ordenCliente ordenCliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordenCliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CLIENTE = new SelectList(db.Cliente, "ID_CLIENTE", "NOMBRE", ordenCliente.ID_CLIENTE);
            ViewBag.ID_PAQUETERIA = new SelectList(db.Paqueteria, "ID_PAQUETERIA", "NOMBRE", ordenCliente.ID_PAQUETERIA);
            return View(ordenCliente);
        }

        // GET: envio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ordenCliente ordenCliente = db.ordenCliente.Find(id);
            if (ordenCliente == null)
            {
                return HttpNotFound();
            }
            return View(ordenCliente);
        }

        // POST: envio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ordenCliente ordenCliente = db.ordenCliente.Find(id);
            db.ordenCliente.Remove(ordenCliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
