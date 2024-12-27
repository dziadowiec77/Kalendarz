using System.Diagnostics;
using System.Security.Claims;
using Kalendarz.Areas.Identity.Data;
using Kalendarz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kalendarz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly KalendarzDBContext _context;

        public HomeController(ILogger<HomeController> logger, KalendarzDBContext context)
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
                .Where(k => k.Udostepnij == true);
       
            var fullCalendarEvents = new List<object>();

            foreach (var ev in events)
            {
                // Jeœli wydarzenie jest powtarzalne
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
                            title = ev.Nazwa + " - " + ev.KalendarzUser?.FirstName,
                            description = ev.Opis,
                            start = currentStart,
                            end = currentEnd,
                            type = ev.TypWydarzeniaId,
                            color = ev.TypWydarzenia?.Kolor
                        });

                        // Dodaj interwa³ powtarzania
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
                    // Jeœli wydarzenie nie jest powtarzalne
                    fullCalendarEvents.Add(new
                    {
                        id = ev.ID,
                        title = ev.Nazwa + " - " + ev.KalendarzUser?.FirstName,
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
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}