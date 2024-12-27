using System.Security.Claims;
using Kalendarz.Areas.Identity.Data;
using Kalendarz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

            // Pobieramy wydarzenia dla zalogowanego użytkownika
            var events = _context.Kal
                .Where(k => k.KalendarzUserId == userId)
                .Include(k => k.TypWydarzenia)
                .ToList();

            var fullCalendarEvents = new List<object>();

            foreach (var ev in events)
            {
                // Jeśli wydarzenie jest powtarzalne
                if (ev.Powtarzalnosc == true && !string.IsNullOrEmpty(ev.CoIle))
                {
                    DateTime currentStart = ev.StartDate;
                    DateTime currentEnd = ev.EndDate;
                    int count = 1;
                    for (int i = 0; i < count; i++)
                    {
                        fullCalendarEvents.Add(new
                        {
                            id = ev.ID,
                            title = ev.Nazwa,
                            description = ev.Opis,
                            start = currentStart,
                            end = currentEnd,
                            type = ev.TypWydarzeniaId,
                            color = ev.TypWydarzenia?.Kolor
                        });

                        // Dodaj interwał powtarzania
                        switch (ev.CoIle)
                        {
                            case "Daily":
                                currentStart = currentStart.AddDays(1);
                                currentEnd = currentEnd.AddDays(1);
                                count = 7;
                                break;
                            case "Weekly":
                                currentStart = currentStart.AddDays(7);
                                currentEnd = currentEnd.AddDays(7);
                                count = 51;
                                break;
                            case "Monthly":
                                currentStart = currentStart.AddMonths(1);
                                currentEnd = currentEnd.AddMonths(1);
                                count = 12;
                                break;
                            case "Yearly":
                                currentStart = currentStart.AddYears(1);
                                currentEnd = currentEnd.AddYears(1);
                                count = 5;
                                break;
                        }
                    }
                }
                else
                {
                    // Jeśli wydarzenie nie jest powtarzalne
                    fullCalendarEvents.Add(new
                    {
                        id = ev.ID,
                        title = ev.Nazwa,
                        description = ev.Opis,
                        start = ev.StartDate,
                        end = ev.EndDate,
                        type = ev.TypWydarzeniaId,
                        color = ev.TypWydarzenia?.Kolor
                    });
                }

            }

            return Json(fullCalendarEvents);
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
            var kalendarz = _context.Kal
                .Include(k => k.TypWydarzenia)
                .FirstOrDefault(k => k.ID == id);


            if (kalendarz == null)
            {
                return NotFound();
            }

            return View(kalendarz);
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
