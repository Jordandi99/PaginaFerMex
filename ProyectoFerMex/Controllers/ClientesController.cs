using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ProyectoFerMex.Models;

namespace ProyectoFerMex.Controllers
{
    public class ClientesController : Controller
    {
        private contextFerMex db = new contextFerMex();

        // GET: Clientes
        public ActionResult Index()
        {
            return View(db.Cliente.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID_CLIENTE,NOMBRE,AP_PAT,AP_MAT,CORREO,CALLE_T,COLONIA_T,ESTADO_T,CODIGO_POSTAL_T,NUM_TARJETA")] Cliente cliente)
        public ActionResult Create(String NOMBRE, String AP_PAT, String AP_MAT, String CORREO, String CALLE_T, String COLONIA_T, String ESTADO_T, String CODIGO_POSTAL_T, String NUM_TARJETA, String TIPO_TARJ,String MES,String ANIO, String CVV)
        {
            Cliente cliente = new Cliente();
           
            int id = 0;
            if (!(db.Cliente.Max(c => (int?)c.ID_CLIENTE)==null))
            {
                id = db.Cliente.Max(c => c.ID_CLIENTE);
            }
            else
            {
                id = 0;
            }
            id++;
            if (Tarjeta(NUM_TARJETA,TIPO_TARJ,MES,ANIO,CVV))
            {
                if (validaPago(NOMBRE,CALLE_T,COLONIA_T,ESTADO_T,CODIGO_POSTAL_T,NUM_TARJETA,MES,ANIO,CVV))
                {
                    cliente.ID_CLIENTE = id;
                    cliente.NOMBRE = NOMBRE;
                    cliente.AP_MAT = AP_PAT;
                    cliente.AP_MAT = AP_MAT;
                    cliente.CORREO = Session["correo"].ToString();
                    cliente.CALLE_T = CALLE_T;
                    cliente.COLONIA_T = COLONIA_T;
                    cliente.ESTADO_T = ESTADO_T;
                    cliente.CODIGO_POSTAL_T = CODIGO_POSTAL_T;
                    cliente.NUM_TARJETA = NUM_TARJETA;

                    dirEntrega dir = new dirEntrega();

                    dir.CALLE = CALLE_T;
                    dir.COLONIA = COLONIA_T;
                    dir.ESTADO = ESTADO_T;
                    dir.CODIGO_POSTAL = CODIGO_POSTAL_T;
                    dir.ID_CLIENTE = id;

                    db.Cliente.Add(cliente);
                    db.dirEntrega.Add(dir);
                    db.SaveChanges();
                    String[] nombres = cliente.NOMBRE.ToString().Split(' ');
                    Session["name"] = nombres[0];
                    Session["usr"] = cliente.NOMBRE;

                    if (Session["CrearOrden"] != null)
                    {
                        if (Session["CrearOrden"].Equals("pend"))
                        {
                            RedirectToAction("CrearOrden","Pago");
                        }
                    }
                    else
                    {
                        RedirectToAction("Index","Home");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                db.Cliente.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_CLIENTE,NOMBRE,AP_PAT,AP_MAT,CORREO,CALLE_T,COLONIA_T,ESTADO_T,CODIGO_POSTAL_T,NUM_TARJETA")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            db.Cliente.Remove(cliente);
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

        private bool Tarjeta(String tarj, String tipo, String mes, String anio, String cvv)
        {
            bool retorna = validaTarj(tarj);
            if (retorna)
            {
                if ((tarj.StartsWith("4") && (tipo.Equals("Visa"))))
                {
                    retorna = true;
                }
                else
                {
                    if ((tarj.StartsWith("5") && (tipo.Equals("Masterd"))))
                    {
                        retorna = true;
                    }
                    else
                    {
                        if ((tarj.StartsWith("3") && (tipo.Equals("American"))))
                        {
                            retorna = true;
                        }
                        else
                        {
                            retorna = false;
                        }
                    }
                }
                DateTime hoy = new DateTime();
                if (Convert.ToInt32(anio) >= hoy.Year)
                {
                    if (Convert.ToInt32(mes)>hoy.Month)
                    {
                        retorna = true;
                    }
                    else
                    {
                        retorna = false;
                    }
                }
                else
                {
                    retorna = false;
                }
            }
            return retorna;
        }

        private bool validaTarj(String tarj)
        {
            bool retorna = true;
            StringBuilder digitsOnly = new StringBuilder();
            foreach(Char c in tarj)
            {
                if (Char.IsDigit(c)) digitsOnly.Append(c);

            };
            if (digitsOnly.Length > 18 || digitsOnly.Length < 15) return false;

                int sum = 0;
                int digit = 0;
                int addend = 0;
                bool timesTwo = false;

                for (int i=digitsOnly.Length-1;i>=0;i--)
                {
                    digit = Int32.Parse(digitsOnly.ToString(i, 1));
                    if (timesTwo)
                    {
                        addend = digit * 2;
                        if (addend>9)
                        {
                            addend -= 9;
                        }
                    }
                    else
                        addend = digit;
                        sum += addend;

                        timesTwo = !timesTwo;
                }
            retorna = ((sum % 10) == 0);
            return retorna;
        }
        private bool validaPago(String NOMBRE, String CALLE_T, String COLONIA_T, String ESTADO_T, String CODIGO_POSTAL_T, String NUM_TARJETA, String MES, String ANIO, String CVV)
        {
            return true;
        }
    }
}
