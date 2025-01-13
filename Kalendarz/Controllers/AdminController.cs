using System.Diagnostics;
using System.Security.Claims;
using Kalendarz.Areas.Identity.Data;
using Kalendarz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Kalendarz.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly KalendarzDBContext _context;

        private const string AdminEmail = "admin@gmail.com";

        public AdminController(ILogger<AdminController> logger, KalendarzDBContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public JsonResult GetEvents()
        {
            var events = _context.Kal
                .Include(k => k.TypWydarzenia)
                .Include(k => k.KalendarzUser)
                .Include(k => k.Powtarzalnosc)
                .Where(k => k.Udostepnianie.Udostepnij == true);

            var fullCalendarEvents = new List<object>();

            foreach (var ev in events)
            {
                if (ev.Powtarzalnosc?.Powtorz == true && !string.IsNullOrEmpty(ev.Powtarzalnosc.CoIle))
                {
                    DateTime currentStart = ev.StartDate;
                    DateTime currentEnd = ev.EndDate;
                    int count = ev.Powtarzalnosc.PrzezIle;
                    for (int i = 0; i < count; i++)
                    {
                        fullCalendarEvents.Add(new
                        {
                            id = ev.ID,
                            title = ev.Nazwa + " - " + ev.KalendarzUser?.FirstName,
                            description = ev.Opis,
                            start = currentStart,
                            end = currentEnd,
                            type = ev.TypWydarzeniaId,
                            color = ev.TypWydarzenia?.Kolor
                        });

                        switch (ev.Powtarzalnosc.CoIle)
                        {
                            case "Daily":
                                currentStart = currentStart.AddDays(1);
                                currentEnd = currentEnd.AddDays(1);
                                break;
                            case "Weekly":
                                currentStart = currentStart.AddDays(7);
                                currentEnd = currentEnd.AddDays(7);
                                break;
                            case "Monthly":
                                currentStart = currentStart.AddMonths(1);
                                currentEnd = currentEnd.AddMonths(1);
                                break;
                            case "Yearly":
                                currentStart = currentStart.AddYears(1);
                                currentEnd = currentEnd.AddYears(1);
                                break;
                        }
                    }
                }
                else
                {
                    fullCalendarEvents.Add(new
                    {
                        id = ev.ID,
                        title = ev.Nazwa + " - " + ev.KalendarzUser?.FirstName,
                        description = ev.Opis,
                        start = ev.StartDate,
                        end = ev.EndDate,
                        type = ev.TypWydarzeniaId,
                        color = ev.TypWydarzenia?.Kolor,
                    });
                }

            }

            return Json(fullCalendarEvents);
        }
        public ActionResult Details(int id)
        {
            var kalendarz = _context.Kal
            .Include(k => k.TypWydarzenia)
            .Include(k => k.Powtarzalnosc)
            .FirstOrDefault(k => k.ID == id);

            if (kalendarz == null)
            {
                return NotFound();
            }
            return View(kalendarz);
        }
        public IActionResult Index()
        {
            var currentUserEmail = User.Identity.Name;

            if (currentUserEmail != AdminEmail)
            {
                return Unauthorized();
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var kalendarz = _context.Kal
                .Include(k => k.Udostepnianie)
                .FirstOrDefault(k => k.ID == id);

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
                var kalendarz = _context.Kal
                    .Include(k => k.Udostepnianie)
                    .FirstOrDefault(k => k.ID == id);
                kalendarz.Udostepnianie.Udostepnij = kal.Udostepnianie.Udostepnij;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

