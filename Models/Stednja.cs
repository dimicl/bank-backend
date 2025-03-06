namespace WebTemplate.Models
{
    public class Stednja
    {
        [Key]
        public int Id { get; set; }
        public  string Naziv { get; set; }

        public decimal Vrednost { get; set; }
        public decimal Cilj { get; set; }
        public Korisnik? Korisnik { get; set; }

    }
}
