using System.Web;
using System.Web.Mvc;

namespace MusicRadio.Permissions
{
    public class ValidateSesionAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["user"] == null)
            {

                filterContext.Result = new RedirectResult("~/Access/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}