using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PWEB.Models;

namespace PWEB.Controllers
{
    public class EntregasController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Entregas
        public ActionResult Index()
        {

            //Vamos obter os veiculos da empresa do worker logado
            var idWorker = User.Identity.GetUserId();

            var idEmpresa = db.Users.Where(a => a.Id == idWorker)
                .Select(a => a.Id_empresa).FirstOrDefault();
            var nomeEmpresa = db.Empresas.Where(a => a.Id_empresa == idEmpresa)
                .Select(a => a.Nome_Empresa).FirstOrDefault();
            var idEmpresaEncrip = db.Users.Where(a => a.UserName == nomeEmpresa)
                .Select(a => a.Id).FirstOrDefault();

            //Todos os veiculos da empresa
            var VeiculosDaEmpresa = db.Veiculos.Where(a => a.Id == idEmpresaEncrip).ToList();

            List<Entrega> listaEntregas = new List<Entrega>();
            if (idWorker == null || idEmpresa == null || nomeEmpresa == null || idEmpresaEncrip == null ||
                VeiculosDaEmpresa == null)
            {
                return View(listaEntregas);
            }
            //Vamos obter os veiculos da empresa nas reservas
            foreach (var veic in VeiculosDaEmpresa)
            {
                var reserva = db.Reservas.FirstOrDefault(a => a.Id_veiculo == veic.Id_veiculo);
                if (reserva != null && reserva.Disponivel == true)
                {
                    Entrega entrega = new Entrega
                    {
                        Id_Entrega = db.Entregas.FirstOrDefault(a => a.Id_reserva == reserva.Id_reserva).Id_Entrega,
                        Data_Entregai = reserva.Datar_i,
                        Data_Entregaf = reserva.Datar_f,
                        Id_reserva = reserva.Id_reserva,
                        IsEntregue = db.Entregas.FirstOrDefault(a => a.Id_reserva == reserva.Id_reserva).IsEntregue
                    };
                    ;
                    entrega.IsRecebido = db.Entregas.FirstOrDefault(a => a.Id_reserva == reserva.Id_reserva).IsRecebido; ;
                    entrega.NomeUser = db.Users.Find(reserva.Id_user).UserName;
                    listaEntregas.Add(entrega);
                }
            }

            return View(listaEntregas);
        }

        // GET: Entregas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                View("~/Views/Shared/not_found.cshtml");
            }
            Entrega entrega = db.Entregas.Find(id);
            if (entrega == null)
            {
                View("~/Views/Shared/not_found.cshtml");
            }
            ViewBag.Id_reserva = new SelectList(db.Reservas, "Id_reserva", "Id_user", entrega.Id_reserva);
            return View(entrega);
        }

        // POST: Entregas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Entrega,Data_Entregai,Data_Entregaf,Id_reserva,NomeUser,IsEntregue,IsRecebido")] Entrega entrega)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entrega).State = EntityState.Modified;
                db.SaveChanges();
                if (entrega.IsRecebido)
                {
                    return RedirectToAction("Index", "Defeitoes",new {idReserva = entrega.Id_reserva});
                }

                return RedirectToAction("Index");
            }
            ViewBag.Id_reserva = new SelectList(db.Reservas, "Id_reserva", "Id_user", entrega.Id_reserva);
            return View(entrega);
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
