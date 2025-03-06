namespace WebTemplate.Models
{
    public class Korisnik
    {
        [Key]
        public int Id { get; set; }
        public  required string Ime { get; set; }
        public  required string Prezime { get; set; }
        public required string Email { get; set; }
        public required string Pin { get; set; }
        public int RacunId { get; set; }
        public Racun? Racun { get; set; }

        public List<Stednja>? Stednje { get; set; }
    }
}
