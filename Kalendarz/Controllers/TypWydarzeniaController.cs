using System.Security.Claims;
using Kalendarz.Areas.Identity.Data;
using Kalendarz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kalendarz.Controllers
{
    public class TypWydarzeniaController : Controller
    {
        private readonly KalendarzDBContext _context;

        public TypWydarzeniaController(KalendarzDBContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: TypWydarzeniaController
        public ActionResult Index()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var typyWydarzen = _context.TypWydarzenia
                .Where(t => t.UserId == userId)
                .Include(t => t.Kal).ToList();
            return View(typyWydarzen);
        }

        // GET: TypWydarzeniaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TypWydarzeniaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypWydarzeniaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TypWydarzenia typWydarzenia)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                typWydarzenia.UserId = userId;
                _context.TypWydarzenia.Add(typWydarzenia);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TypWydarzeniaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TypWydarzeniaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TypWydarzenia typWydarzenia)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                typWydarzenia.UserId = userId;
                _context.TypWydarzenia.Update(typWydarzenia);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TypWydarzeniaController/Delete/5
        //public ActionResult Delete(int id)
        //{

        //    return View();
        //}

        public async Task<IActionResult> Delete(int? id)
        {
            var typWydarzenia = await _context.TypWydarzenia.FindAsync(id);
            if (typWydarzenia == null)
            {
                return NotFound();
            }
            var wydarzeniaDoAktualizacji = _context.Kal.Where(e => e.TypWydarzeniaId == id);
            foreach (var wydarzenie in wydarzeniaDoAktualizacji)
            {
                wydarzenie.TypWydarzeniaId = null;
            }

            _context.TypWydarzenia.Remove(typWydarzenia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: TypWydarzeniaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, TypWydarzenia typWydarzenia)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
