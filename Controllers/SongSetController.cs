using MusicRadio.Data;
using MusicRadio.Interfaces;
using MusicRadio.Models;
using MusicRadio.Permissions;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MusicRadio.Controllers
{
    [ValidateSesionAttribute]
    public class SongSetController : Controller
    {
        private readonly ISongSetRepository _repository;

        public SongSetController()
        {
            _repository = new SongSetRepository();
        }

        public ActionResult Index()
        {
            try
            {
                return View(_repository.GetAll());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al cargar las canciones: " + ex.Message;
                return View(new List<SongSet>());
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            try
            {
                var albumRepo = new AlbumSetRepository();
                ViewBag.Albums = new SelectList(albumRepo.GetAll(), "Id", "Name");
                return View(new SongSet());
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar los álbumes: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Register(SongSet songSet)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Add(songSet);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al registrar la canción: " + ex.Message);
                }
            }

            var albumRepo = new AlbumSetRepository();
            ViewBag.Albums = new SelectList(albumRepo.GetAll(), "Id", "Name");
            return View(songSet);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var songSet = _repository.GetById(id);
                if (songSet == null) return HttpNotFound();

                var albumRepo = new AlbumSetRepository();
                ViewBag.Albums = new SelectList(albumRepo.GetAll(), "Id", "Name", songSet.Album_id);
                return View(songSet);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al editar la canción: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(SongSet songSet)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update(songSet);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al actualizar la canción: " + ex.Message);
                }
            }

            var albumRepo = new AlbumSetRepository();
            ViewBag.Albums = new SelectList(albumRepo.GetAll(), "Id", "Name", songSet.Album_id);
            return View(songSet);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var songSet = _repository.GetById(id);
                if (songSet == null) return HttpNotFound();
                return View(songSet);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar la canción para eliminar: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _repository.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al eliminar la canción: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }

}