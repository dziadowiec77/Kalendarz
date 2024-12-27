using System.Security.Claims;
using Kalendarz.Areas.Identity.Data;
using Kalendarz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Kalendarz.Controllers
{
    public class KalController : Controller
    {
        private readonly KalendarzDBContext _context;
        public KalController(KalendarzDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public JsonResult GetEvents()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var events = _context.Kal
                .Where(k => k.KalendarzUserId == userId)
                .Select(k => new
                {
                    id = k.ID,
                    title = k.Nazwa,
                    description = k.Opis,
                    start = k.StartDate,
                    end = k.EndDate,
                    type = k.TypWydarzeniaId
                })
                .ToList();

            return Json(events);
        }
        // GET: KalController
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        // GET: KalController/Details/5
        public ActionResult Details(int id)
        {
            var kalendarz = _context.Kal
            .Include(k => k.TypWydarzenia)
            .FirstOrDefault(k => k.ID == id);
            


            if (kalendarz == null)
            {
                return NotFound();
            }
            return View(kalendarz);
        }

        // GET: KalController/Create
        public ActionResult Create()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ViewBag.TypWydarzeniaId = new SelectList(_context.TypWydarzenia.Where(k => k.UserId == userId), "ID", "Nazwa");
            return View();
        }

        // POST: KalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kal kal)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                kal.KalendarzUserId = userId;
                _context.Kal.Add(kal);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: KalController/Edit/5
        public ActionResult Edit(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ViewBag.TypWydarzeniaId = new SelectList(_context.TypWydarzenia.Where(k => k.UserId == userId), "ID", "Nazwa");
            var kalendarz = _context.Kal.FirstOrDefault(k => k.ID == id);

            if (kalendarz == null)
            {
                return NotFound();
            }

            return View(kalendarz);
        }

        // POST: KalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Kal kal)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                kal.KalendarzUserId = userId;
                _context.Update(kal);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: KalController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: KalController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Kal kal)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                kal.KalendarzUserId = userId;
                _context.Kal.Remove(kal);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
