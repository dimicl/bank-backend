using System.Text.Json.Serialization;

namespace WebTemplate.Models
{
   public class Racun
    {
        [Key]
        public int Id { get; set; }

        public required string BrojRacuna { get; set; }
        public decimal Sredstva { get; set; }
        public string? Valuta { get; set; }

        public int KorisnikId { get; set; }

        [JsonIgnore]
        public Korisnik? Korisnik { get; set; } = null!;

        public List<Transakcija>? Transakcije { get; set; }
    }
}
