using System.Text.Json.Serialization;

namespace WebTemplate.Models
{
    public class Transakcija
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public decimal Iznos { get; set; }
        public string Tip { get; set; }  
        
        public int RacunId { get; set; }
        [JsonIgnore]
        public Racun Racun { get; set; }

        public string Svrha { get; set; }
        public string TekuciSender { get; set; }
        public string TekuciReceiver { get; set; }
    }
}
