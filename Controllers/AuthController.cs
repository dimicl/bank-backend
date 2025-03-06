namespace WebTemplate.Controllers;
using System.Linq;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    public BankaContext Context { get; set; }
    public AuthService authService { get; set; }
    public AuthController(BankaContext context, AuthService service)
    {
        Context = context;
        authService = service;
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> register([FromBody] RegisterRequest request)
    {
        try
        {
            if(Context.Korisnici.Any(k=> k.Email == request.Email))
                return BadRequest("Email vec postoji.");
            
            string pinKod = authService.GeneratePin();
            string brojR = authService.GenerateBrojRacuna();
            var racun = new Racun
            {
                BrojRacuna = brojR,
                Sredstva = 0,
                Valuta = "RSD"
            };

            var korisnik = new Korisnik
            {
                Ime = request.Ime,
                Prezime = request.Prezime,
                Email = request.Email,
                Pin = pinKod,
                Racun = racun
            };

            Context.Korisnici.Add(korisnik);
            await Context.SaveChangesAsync();
            return Ok(new {Message = "Registracija uspesna.", PinKod=pinKod, racun = new { racun.BrojRacuna, racun.Sredstva, racun.Valuta }});
            
        }
        catch (Exception e)
        {
            return BadRequest("Greska" + e.Message);
        }
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> login([FromBody] LoginRequest request)
    {
        try
        {
            var korisnik = await Context.Korisnici.Include(k=>k.Racun).FirstOrDefaultAsync(k=> k.Pin == request.Pin);
            if(korisnik == null)
                return Unauthorized(new { message = "Pogresan pin" });
            
            return Ok(new { message  = "Korisnik " + korisnik.Ime + " sa pinom " + korisnik.Pin + " se uspesno ulogovao", korisnik = new {korisnik.Ime, korisnik.Prezime, korisnik.Pin, korisnik.Email} , racun = new { korisnik.Racun?.BrojRacuna, korisnik.Racun?.Sredstva, korisnik.Racun?.Valuta} });
        }
        catch (Exception e)
        {
            
            return BadRequest("Greska " + e.Message);
        }
    }

    
}
