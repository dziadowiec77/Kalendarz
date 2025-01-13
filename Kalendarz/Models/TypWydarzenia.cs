using System.ComponentModel.DataAnnotations;
using Kalendarz.Areas.Identity.Data;

namespace Kalendarz.Models
{
    public class TypWydarzenia
    {
        public int ID { get; set; }
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "To pole może zawierać tylko litery.")]
        public required string Nazwa { get; set; }
        public required string Kolor { get; set; }
        public required ICollection<Kal> Kal { get; set; }
        public int UserId { get; set; }
        public KalendarzUser? User { get; set; }
    }
}
