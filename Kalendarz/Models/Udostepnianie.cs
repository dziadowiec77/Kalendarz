
namespace Kalendarz.Models
{
    public class Udostepnianie
    {
        public int ID { get; set; }
        public bool Udostepnij { get; set; }
        public string? Email { get; set; }
        public int KalId { get; set; }

        public Kal Kal { get; set; }
    }
}
