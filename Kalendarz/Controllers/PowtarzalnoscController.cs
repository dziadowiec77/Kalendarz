using Kalendarz.Areas.Identity.Data;
using Kalendarz.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Kalendarz.Controllers
{
    public class PowtarzalnoscController : Controller
    {
        private readonly KalendarzDBContext _context;

        public PowtarzalnoscController(KalendarzDBContext context)
        {
            _context = context;
        }
        // GET: PowtarzalnoscController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PowtarzalnoscController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PowtarzalnoscController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PowtarzalnoscController/Create
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

        // GET: PowtarzalnoscController/Edit/5
        public ActionResult Edit(int id)
        {
            var kalendarz = _context.Powtarzalnosc.FirstOrDefault(k => k.KalId == id);
            if (kalendarz == null)
            {
                return NotFound();
            }

            return View(kalendarz);
        }

        // POST: PowtarzalnoscController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Powtarzalnosc powtarzalnosc)
        {
            try
            {
                var powtorz = _context.Powtarzalnosc.FirstOrDefault(p => p.KalId == id);
                if (powtorz != null)
                {
                    powtorz.Powtorz = powtarzalnosc.Powtorz;
                    powtorz.CoIle = powtarzalnosc.CoIle;
                    powtorz.PrzezIle = powtarzalnosc.PrzezIle;

                    _context.SaveChanges();
                }
                return RedirectToAction("Index", "Kal");
            }
            catch
            {
                return View();
            }
        }

        // GET: PowtarzalnoscController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PowtarzalnoscController/Delete/5
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
