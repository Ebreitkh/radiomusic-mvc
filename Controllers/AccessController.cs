using MusicRadio.Data;
using MusicRadio.Interfaces;
using MusicRadio.Models;
using System;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Security;

namespace MusicRadio.Controllers
{
    public class AccessController : Controller
    {

        private readonly IAccessRepository _repository;

        public AccessController()
        {
            _repository = new AccessRepository();
        }

        // GET: Access
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var isValidUser = _repository.ValidateUser(model.Mail, model.Password);

                if (isValidUser)
                {
                    FormsAuthentication.SetAuthCookie(model.Mail, false);
                    Session["User"] = model;
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Correo o contraseña incorrectos");
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error durante el login.");
                return View(model);
            }
        }




        public ActionResult Register()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(user);
                }

                _repository.Add(user);
                return RedirectToAction("Index", "Home");
            }
            catch (SqlException ex) when (ex.Message.Contains("CHK_Mail"))
            {
                ModelState.AddModelError("Mail", "El formato del correo electrónico no es válido");
                return View(user);
            }
            catch (SqlException)
            {
                ModelState.AddModelError("", "Error al registrar el usuario. Por favor intente nuevamente.");
                return View(user);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error inesperado. Por favor intente nuevamente.");
                return View(user);
            }
        }
    }
}