using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PWEB.Models;

namespace PWEB.Controllers
{
    public class FuncionariosController : Controller
    {
        // GET: Funcionarios
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public FuncionariosController()
        {
        }

        public FuncionariosController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        //


        public ActionResult Index()
        {
            string user = User.Identity.GetUserName();
            var emp = db.Empresas.First(a => a.Nome_Empresa == user);
            var user2 = db.Users.Where(a => a.Id_empresa == emp.Id_empresa);


            return View(user2.ToList());
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult RegisterFuncionario()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterFuncionario(RegisterFuncionarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var nomeEmpresa = User.Identity.GetUserName();
                
                //adicionar à inicialização os novos parâmetros que se adicionem à ApplicationUser (IdentityModels)
                var emp = db.Empresas.First(a => a.Nome_Empresa == nomeEmpresa);
                    

                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, Id_empresa = emp.Id_empresa};

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    await this.UserManager.AddToRoleAsync(user.Id, model.RoleName);

                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Index", "Funcionarios");
        }
    }
}