using MusicRadio.Data;
using MusicRadio.Interfaces;
using MusicRadio.Models;
using MusicRadio.Permissions;
using System;
using System.Web.Mvc;

namespace MusicRadio.Controllers
{
    [ValidateSesionAttribute]
    public class AlbumSetController : Controller
    {
        private readonly IAlbumSetRepository _repository;

        public AlbumSetController()
        {
            _repository = new AlbumSetRepository();
        }

        public ActionResult Index()
        {
            try
            {
                return View(_repository.GetAll());
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(AlbumSet albumSet)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Add(albumSet);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(albumSet);
        }

        [HttpGet]
        public ActionResult Edit(int idAlbumSet)
        {
            try
            {
                var albumSet = _repository.GetById(idAlbumSet);
                if (albumSet == null) return HttpNotFound();
                return View(albumSet);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(AlbumSet albumSet)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update(albumSet);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(albumSet);
        }

        [HttpGet]
        public ActionResult Delete(int idAlbumSet)
        {
            try
            {
                var albumSet = _repository.GetById(idAlbumSet);
                if (albumSet == null) return HttpNotFound();
                return View(albumSet);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int idAlbumSet)
        {
            try
            {
                _repository.Delete(idAlbumSet);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }
    }

}
