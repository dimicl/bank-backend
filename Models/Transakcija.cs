using System.Text.Json.Serialization;

namespace WebTemplate.Models
{
    public class Transakcija
    {
        [Key]
        public int Id { get; set; }

        public required decimal Iznos { get; set; }

        public required DateTime Datum { get; set; }

        public required string Tip   { get; set; } 

        public string? TekuciSender { get; set; }
        public string? TekuciReceiver { get; set;}
        public string? Svrha { get; set; }

        [ForeignKey("Racun")]
        public int RacunId { get; set; } 
      
        [JsonIgnore]
        public Racun? Racun { get; set; }
    }
}