using MusicRadio.Permissions;
using System.Web.Mvc;

namespace MusicRadio.Controllers
{
    [ValidateSesionAttribute]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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


        public ActionResult SignOut()
        {
            Session["User"] = null;
            return RedirectToAction("Login", "Access");
        }


    }
}