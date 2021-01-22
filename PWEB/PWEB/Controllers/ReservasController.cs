using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PWEB.Models;

namespace PWEB.Controllers
{
    public class ReservasController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservas
        [Authorize(Roles = "Admin,User,Worker")]
        public ActionResult Index()
        {

            IEnumerable<Reserva> Reservas;
            if (User.IsInRole("User"))
            {
                var UserId = User.Identity.GetUserId();
                Reservas = db.Reservas.Where(c => c.Id_user == UserId).ToList();
            }
            else if (User.IsInRole("Worker"))
            {
                var workerId = User.Identity.GetUserId(); // id funcionario

                var id_emp = db.Users.Where(a => a.Id == workerId)
                    .Select(a => a.Id_empresa).FirstOrDefault(); // id_empresa

                var nome_emp = db.Empresas.Where(a => a.Id_empresa == id_emp)
                    .Select(a => a.Nome_Empresa).FirstOrDefault(); // nome_emp

                var dono = db.Users.Where(a => a.UserName == nome_emp)
                    .Select(a => a.Id).FirstOrDefault();

                var veic = db.Veiculos.Where(a => a.Id == dono)
                    .Select(a => a.Id_veiculo).FirstOrDefault();
                Reservas = db.Reservas.Where(a => a.Id_veiculo == veic).ToList();
            }
            else
            {
                Reservas = db.Reservas.ToList();
            }

            List<Reserva> reservaView = new List<Reserva>();
            foreach (var re in Reservas)
            {
                reservaView.Add(new Reserva
                {
                    Id_reserva = re.Id_reserva,
                    Datar_i = re.Datar_i,
                    Datar_f = re.Datar_f,
                    PrecoTotal = re.updatePrecoTotal(re.Id_veiculo),
                    Disponivel = re.Disponivel,
                    Id_veiculo = re.Id_veiculo,
                    Veiculos = re.Veiculos,
                    Id_user = db.Users.Find(re.Id_user).UserName
                });
            }

            return View(reservaView.ToList());
        }

        // GET: Reservas/Details/5
        [Authorize(Roles = "Admin,Worker,User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                View("~/Views/Shared/not_found.cshtml");
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                View("~/Views/Shared/not_found.cshtml");
            }
            return View(reserva);
        }

        // GET: Reservas/Create
        [Authorize(Roles = "Admin, User, Worker")]
        public ActionResult Create()
        {
            var listaAux = db.Veiculos;

            List<Veiculo> lista = new List<Veiculo>();
            if (User.IsInRole("User"))
            {
                foreach (var veiculo in listaAux)
                {
                    if (veiculo.isDisponivel())
                    {
                        lista.Add(veiculo);
                    }
                }
            }
            else if (User.IsInRole("Worker"))
            {
                var idWorker = User.Identity.GetUserId();
                var idEmpresa = db.Users.Where(a => a.Id == idWorker)
                    .Select(a => a.Id_empresa).FirstOrDefault();
                var nomeEmpresa = db.Empresas.Where(a => a.Id_empresa == idEmpresa)
                    .Select(a => a.Nome_Empresa).FirstOrDefault();
                var donoEmpresa = db.Users.Where(a => a.UserName == nomeEmpresa)
                    .Select(a => a.Id).FirstOrDefault();
                var veic = db.Veiculos.Where(a => a.Id == donoEmpresa)
                    .Select(a => a.Id_veiculo).FirstOrDefault();

                foreach (var veiculo in listaAux)
                {
                    if (veiculo.Id == donoEmpresa)
                    {
                        if (veiculo.isDisponivel())
                        {
                            lista.Add(veiculo);
                        }
                    }
                }
            }

            ViewBag.Id_veiculo = new SelectList(lista, "Id_veiculo", "Nome_veiculo");
            ViewBag.Id = new SelectList(db.Users, "Id", "Username");

            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_reserva,Datar_i,Datar_f,PrecoTotal,Disponivel,Id_veiculo,Id_user")] Reserva reserva)
        {
            Veiculo vei = new Veiculo {Id_veiculo = reserva.Id_veiculo};

            if (ModelState.IsValid)
            {
                var loggedInUser = User.Identity.GetUserId();
                reserva.Id_user = loggedInUser;
                reserva.Disponivel = false;
                reserva.Id_veiculo = reserva.Id_veiculo;

                db.Reservas.Add(reserva);
                db.SaveChanges();

                ViewBag.Id_veiculo = new SelectList(db.Veiculos, "Id_veiculo", "Nome_veiculo", reserva.Id_veiculo);

                var reservaidve = db.Reservas.Where(a => a.Id_veiculo == reserva.Id_veiculo)
                    .Select(a => a.Id_veiculo).FirstOrDefault();
                var valor = reserva.updatePrecoTotal(reservaidve);

                System.Diagnostics.Debug.WriteLine("  USER A" + reservaidve);
                System.Diagnostics.Debug.WriteLine("  USER B" + valor);

                reserva.PrecoTotal = valor;
                db.Entry(reserva).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        [Authorize(Roles = "Admin,Worker")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                View("~/Views/Shared/not_found.cshtml");
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                View("~/Views/Shared/not_found.cshtml");
            }

            ViewBag.Id_veiculo = new SelectList(db.Veiculos, "Id_veiculo", "Nome_veiculo", reserva.Id_veiculo);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_reserva,Datar_i,Datar_f,PrecoTotal,Disponivel,Id_veiculo,Id_user")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                if (reserva.Disponivel == true)
                {
                    Entrega entrega = new Entrega
                    {
                        Data_Entregai = reserva.Datar_i,
                        Data_Entregaf = reserva.Datar_f,
                        Id_reserva = reserva.Id_reserva,
                        IsEntregue = false,
                        IsRecebido = false,
                        NomeUser = db.Users.Find(reserva.Id_user).UserName
                    };
                    var aux = db.Entregas.FirstOrDefault(a => a.Id_reserva == entrega.Id_reserva);
                    if (aux == null)
                    {
                        db.Entregas.Add(entrega);
                        db.SaveChanges();
                    }
                }
                db.Entry(reserva).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_veiculo = new SelectList(db.Veiculos, "Id_veiculo", "Nome_veiculo", reserva.Id_veiculo);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                View("~/Views/Shared/not_found.cshtml");
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                View("~/Views/Shared/not_found.cshtml");
            }
            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reserva reserva = db.Reservas.Find(id);
            db.Reservas.Remove(reserva);
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
