using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PWEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            var userRole = User.Identity.GetUserName();

            string roles = "";
            if (userRole != "")
            {
                var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                if (UserManager == null)
                {
                    return View();
                }

                roles = UserManager.GetRoles(User.Identity.GetUserId()).FirstOrDefault();
            }

            if (roles == "") 
                roles = "Guest";
            //Role pertencente
            ViewBag.Role = roles;
            
            //Companhia pertencente

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}