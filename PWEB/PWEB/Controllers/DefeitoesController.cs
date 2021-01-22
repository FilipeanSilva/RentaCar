using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PWEB.Models;

namespace PWEB.Controllers
{
    public class DefeitoesController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Defeitoes
        public ActionResult Index(int idReserva)
        {

            var defeito = db.Defeitos.FirstOrDefault(a => a.Id_reserva == idReserva);
            if (defeito == null)
            {
                defeito = new Defeito { Id_reserva = idReserva };

                ViewBag.nomeCarro = db.Veiculos.FirstOrDefault(a =>
                        a.Id_veiculo == (db.Reservas.FirstOrDefault(m => m.Id_reserva == idReserva).Id_veiculo))
                    ?.Nome_veiculo;

                ViewBag.nomeCliente = db.Users.Find((db.Reservas.FirstOrDefault(a => a.Id_reserva == idReserva)?.Id_user)).UserName;

                db.Defeitos.Add(defeito);
                db.SaveChanges();
                ViewBag.txtKilometros = "Introduza os kilometros atuais do carro";
                return View(defeito);
            }
            //TODO
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Id,Id_reserva,DanosExteriores,DanosInteriores")] Defeito defeito, IEnumerable<HttpPostedFileBase> files, string kmFinais)
        {
            if (ModelState.IsValid)
            {
                var carro = db.Veiculos.FirstOrDefault(a =>
                        a.Id_veiculo == (db.Reservas.FirstOrDefault(m => m.Id_reserva == defeito.Id_reserva).Id_veiculo));

                var kmEntregues = 0;
                try
                {
                    kmEntregues = Int16.Parse(kmFinais);
                }
                catch (Exception e)
                {
                    ViewBag.txtKilometros = "Kilometros invalidos";
                    return View(defeito);
                }

                if (carro.Kilometros > kmEntregues)
                {
                    ViewBag.txtKilometros = "Kilometros invalidos";
                    return View(defeito);
                }
                else
                {
                    carro.Kilometros = kmEntregues - carro.Kilometros;

                    db.Entry(carro).State = EntityState.Modified;
                    db.SaveChanges();
                }

                var nomeCarro = carro.Nome_veiculo;

                string nomeImagem = nomeCarro + "_Veiculo_Defeito_";
                var httpPostedFileBases = files.ToList();
                foreach (var file in httpPostedFileBases)
                {
                    string auxNomeImagem = nomeImagem;
                    if (file != null && file.ContentLength > 0)
                    {
                        if (httpPostedFileBases[0] == file)
                        {
                            auxNomeImagem += "Exterior_";
                            auxNomeImagem += Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath("~/uploads"), auxNomeImagem));
                            defeito.DanosInteriores = auxNomeImagem;
                        }
                        else if (httpPostedFileBases[1] == file)
                        {
                            auxNomeImagem += "Interior_";
                            auxNomeImagem += Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath("~/uploads"), auxNomeImagem));
                            defeito.DanosExteriores = auxNomeImagem;
                        }
                    }
                }
                db.Entry(defeito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(defeito);
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
