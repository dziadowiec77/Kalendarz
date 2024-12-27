using Kalendarz.Areas.Identity.Data;

namespace Kalendarz.Models
{
    public class TypWydarzenia
    {
        public int ID { get; set; }
        public required string Nazwa { get; set; }
        public string? Kolor { get; set; }
        public required ICollection<Kal> Kal { get; set; }
        public int UserId { get; set; }
        public KalendarzUser? User { get; set; }
    }
}
