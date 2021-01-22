using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PWEB.Models;

namespace PWEB.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Veiculos
        [Authorize(Roles = "Admin,User,Worker,Company")]
        public ActionResult Index()
        {
            IEnumerable<Veiculo> Veiculos;
            if (User.IsInRole("Company"))
            {
                var companyID = User.Identity.GetUserId();
                Veiculos = db.Veiculos.Where(c => c.Id == companyID).ToList();

                System.Diagnostics.Debug.WriteLine("AQui****** " + Veiculos);
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

                Veiculos = db.Veiculos.Where(a => a.Id_veiculo == veic).ToList();
            }
            else
            {
                Veiculos = db.Veiculos.ToList();
            }

            List<Veiculo> veiculosView = new List<Veiculo>();
            foreach (var vei in Veiculos)
            {
                veiculosView.Add(new Veiculo
                {
                    Id_veiculo = vei.Id_veiculo,
                    Nome_veiculo = vei.Nome_veiculo,
                    Marca = vei.Marca,
                    Cor = vei.Cor,
                    Kilometros = vei.Kilometros,
                    Defeitos = vei.Defeitos,
                    Id_categoria = vei.Id_categoria,
                    Imagem = vei.Imagem,
                    PrecoDia = vei.PrecoDia,
                    Id = db.Users.Find(vei.Id).UserName
                });
            }
            if (User.IsInRole(RoleName.Company))
            {
                return View("CompanyIndex", veiculosView);
            }
            return View(veiculosView);
        }

        [Authorize(Roles = "Admin,Company")]
        // GET: Veiculos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                View("~/Views/Shared/not_found.cshtml");
            }
            Veiculo veiculo = db.Veiculos.Find(id);
            if (veiculo == null)
            {
                View("~/Views/Shared/not_found.cshtml");
            }
            return View(veiculo);
        }

        // GET: Veiculos/Create

        [Authorize(Roles = "Admin,Company")]
        public ActionResult Create()
        {
            ViewBag.Id_categoria = new SelectList(db.Categorias, "Id_categoria", "NomeCategoria");
            ViewBag.Id = new SelectList(db.Users, "Id", "Username");

            var id_emp = User.Identity.GetUserName();

            ViewBag.Id_empresa = new SelectList(id_emp);

            System.Diagnostics.Debug.WriteLine("AQui_____");

            return View();
        }

        // POST: Veiculos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_veiculo,Nome_veiculo,Marca,Cor,Kilometros,Defeitos,Id_categoria,PrecoDia,Id")] Veiculo veiculo)
        {
            var id_emp = User.Identity.GetUserName();

            if (ModelState.IsValid)
            {
                var loggedInUser = User.Identity.GetUserId();
                veiculo.Id = loggedInUser;

                db.Veiculos.Add(veiculo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_categoria = new SelectList(db.Categorias, "Id_categoria", "NomeCategoria", veiculo.Id_categoria);
            ViewBag.Id_empresa = new SelectList(id_emp, veiculo.Id);
            return View(veiculo);
        }

        // GET: Veiculos/Edit/5
        [Authorize(Roles = "Admin,Company")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                View("~/Views/Shared/not_found.cshtml");
            }
            Veiculo veiculo = db.Veiculos.Find(id);
            if (veiculo == null)
            {
                View("~/Views/Shared/not_found.cshtml");
            }
            ViewBag.Id_categoria = new SelectList(db.Categorias, "Id_categoria", "NomeCategoria", veiculo.Id_categoria);
            return View(veiculo);
        }

        // POST: Veiculos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_veiculo,Nome_veiculo,Marca,Cor,Kilometros,Defeitos,Id_categoria,PrecoDia,Id")] Veiculo veiculo, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = User.Identity.GetUserId();
                veiculo.Id = loggedInUser;

                string nomeImagem = "Veiculo_" + veiculo.Id_veiculo.ToString();

                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        nomeImagem += Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath("~/uploads"), nomeImagem));
                        veiculo.Imagem = nomeImagem;
                    }
                }

                db.Entry(veiculo).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Id_categoria = new SelectList(db.Categorias, "Id_categoria", "NomeCategoria", veiculo.Id_categoria);
            return View(veiculo);
        }

        // GET: Veiculos/Delete/5
        [Authorize(Roles = "Admin,Company")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                View("~/Views/Shared/not_found.cshtml");
            }
            Veiculo veiculo = db.Veiculos.Find(id);
            if (veiculo == null)
            {
                View("~/Views/Shared/not_found.cshtml");
            }
            return View(veiculo);
        }

        // POST: Veiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Veiculo veiculo = db.Veiculos.Find(id);
            db.Veiculos.Remove(veiculo);
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
