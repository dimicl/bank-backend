namespace WebTemplate.Models
{
    public class Korisnik
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        public required string Ime { get; set; }
        public required string Prezime { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [MaxLength(4)]
        public required string Pin { get; set; } 

        public List<Stednja>? Stednje { get; set; }
        [ForeignKey("RacunId")]
        public int? RacunId { get; set; }
        public Racun? Racun { get; set; }
    }
}
