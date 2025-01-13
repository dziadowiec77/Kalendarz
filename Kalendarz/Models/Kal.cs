using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Kalendarz.Areas.Identity.Data;

namespace Kalendarz.Models
{
    public class Kal
    {
        public int ID { get; set; }
        public required string Nazwa { get; set; }
        public string? Opis { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }

        public int KalendarzUserId { get; set; }
        public KalendarzUser? KalendarzUser { get; set; }

        public int? TypWydarzeniaId { get; set; }
        public TypWydarzenia? TypWydarzenia { get; set; }

        public Powtarzalnosc? Powtarzalnosc { get; set; }

        public Udostepnianie? Udostepnianie { get; set; }
    }
}
