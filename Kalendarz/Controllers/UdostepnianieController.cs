using Kalendarz.Areas.Identity.Data;
using Kalendarz.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kalendarz.Controllers
{
    public class UdostepnianieController : Controller
    {
        private readonly KalendarzDBContext _context;

        public UdostepnianieController(KalendarzDBContext context)
        {
            _context = context;
        }
        // GET: UdostepnianieController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UdostepnianieController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UdostepnianieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UdostepnianieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: UdostepnianieController/Edit/5
        public ActionResult Edit(int id)
        {
            var kalendarz = _context.Udostepnianie.FirstOrDefault(k => k.KalId == id);
            if (kalendarz == null)
            {
                return NotFound();
            }

            return View(kalendarz);
        }

        // POST: UdostepnianieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Udostepnianie udostepnianie)
        {
            try
            {
                var udos = _context.Udostepnianie.FirstOrDefault(p => p.KalId == id);
                if (udos != null)
                {
                    udos.Udostepnij = udostepnianie.Udostepnij;
                    udos.Email = udostepnianie.Email;

                    _context.SaveChanges();
                }
                return RedirectToAction("Index", "Kal");
            }
            catch
            {
                return View();
            }
        }

        // GET: UdostepnianieController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UdostepnianieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
