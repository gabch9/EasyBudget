using BackEnd.BLL.Login;
using BackEnd.BLL.Persona;
using BackEnd.Model;
using EasyBudget.Models;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EasyBudget.Controllers
{
	public class LoginController : Controller
    {
		private IBLLLogin loginBLL;

		//
		// GET: /Login/
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Autherize(UserViewModel userModel)
		{
            IBLLPersona personaBLL = new BLLPersonaImpl();

			loginBLL = new BLLLoginImpl();
			var userDetails = loginBLL.getUser(userModel.UserName, userModel.Password);
			if (userDetails == null)
			{
				userModel.LoginErrorMessage = "Nombre de Usuario o Password Incorrectos";
				return View("Index", userModel);
			}
			else
			{
                PERSONA persona = personaBLL.GetDetailsWithLoginID(userDetails.LoginId);
                Session["userID"] = persona.PersonaId;
                Session["userName"] = persona.PersonaNombre;
                var authTicket = new FormsAuthenticationTicket(userDetails.LoginUsername, true, 100000);
				string encTicket = FormsAuthentication.Encrypt(authTicket);
				var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
				Response.Cookies.Add(cookie);

				return RedirectToAction("Resumen", "Dashboard");
			}
		}

		public ActionResult LogOut()
		{
			int userId = (int)Session["userID"];
			Session.Abandon();
			FormsAuthentication.SignOut();
			
			return RedirectToAction("Index", "Home");
		}
	}
}