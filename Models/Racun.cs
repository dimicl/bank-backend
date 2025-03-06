using System.Text.Json.Serialization;

namespace WebTemplate.Models
{
    public class Racun
    {
        public int Id { get; set; }
        public string BrojRacuna { get; set; }
        public decimal Sredstva { get; set; }
        public string Valuta { get; set; }
        public int KorisnikId { get; set; } 
        public List<Transakcija>? Transakcije { get; set; }
        
    }
}
