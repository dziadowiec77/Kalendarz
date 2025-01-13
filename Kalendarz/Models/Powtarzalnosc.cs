using System.ComponentModel.DataAnnotations;

namespace Kalendarz.Models
{
    public class Powtarzalnosc
    {
        public int ID { get; set; }
        public bool Powtorz { get; set; }
        public string? CoIle { get; set; }
        public int PrzezIle { get; set; }

        public int KalId { get; set; }
        public Kal Kal { get; set; }
    }
}
