namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class RacunController : ControllerBase
{
    public BankaContext Context { get; set; }

    public RacunController(BankaContext context)
    {
        Context = context;
    }

    [HttpPost]
    [Route("getStanje")]
    public async Task<ActionResult> getStanje([FromBody] PinRequest request)
    {
        try
        {
            var k = await Context.Korisnici.Include(k=>k.Racun).FirstOrDefaultAsync(r=>r.Pin == request.Pin);
            if(k == null)
                return BadRequest("Racun ne postoji.");
            
            return Ok(new { k.Racun?.Sredstva });

        }
        catch (Exception e)
        {
            return BadRequest("Greska " + e.Message);            

        }
    }

    [HttpPut]
    [Route("uplata")]
    public async Task<ActionResult> uplataNovca([FromBody] TransakcijaRequest request)
    {
        try
        {
            var user = await Context.Korisnici.Include(k=>k.Racun).FirstOrDefaultAsync(r=>r.Pin == request.Pin);
            if(user ==  null)
                return BadRequest("User ne postoji.");
            
            if(request.Iznos <= 0)
                return BadRequest("Iznos mora biti validan");

            if(user.Racun != null)
                user.Racun.Sredstva = (user.Racun.Sredstva) + request.Iznos;

            var transakcija = new Transakcija
            {
                Iznos = request.Iznos,
                Tip = "Uplata",
                Datum = DateTime.UtcNow,
                Racun = user.Racun
            };
            
            Context.Transakcije.Add(transakcija);
            await Context.SaveChangesAsync();
            return Ok(transakcija);
        }
        catch (Exception e)
        {
             var innerExceptionMessage = e.InnerException?.Message ?? "No inner exception.";
            return BadRequest($"Greska: {e.Message}, Inner Exception: {innerExceptionMessage}");
        }
    }

    
    
 [HttpPut]
[Route("transfer")]
public async Task<ActionResult> transferNovca([FromBody] TransferRequest request)
{
    try
    {
        var sender = await Context.Korisnici.Include(k => k.Racun).ThenInclude(r => r.Transakcije)
            .FirstOrDefaultAsync(r => r.Racun.BrojRacuna == request.SenderAccount);
        
        var receiver = await Context.Korisnici.Include(k => k.Racun).ThenInclude(r => r.Transakcije)
            .FirstOrDefaultAsync(r => r.Racun.BrojRacuna == request.ReceiverAccount);

        if (sender == null || receiver == null)
            return BadRequest("Greska, ne postoji.");

        if (sender.Racun?.Sredstva <= 0 || sender.Racun?.Sredstva < request.Iznos)
            return BadRequest("Nemate dovoljno sredstava za transfer");

        if (sender.Racun?.Transakcije == null)
            sender.Racun.Transakcije = new List<Transakcija>();

        if (receiver.Racun?.Transakcije == null)
            receiver.Racun.Transakcije = new List<Transakcija>();

        sender.Racun.Sredstva -= request.Iznos;
        receiver.Racun.Sredstva += request.Iznos;

        var transakcijaSender = new Transakcija
        {
            Iznos = request.Iznos,
            Tip = "Poslato",
            Datum = DateTime.Now,
            TekuciSender = sender.Racun?.BrojRacuna,
            TekuciReceiver = receiver.Racun?.BrojRacuna,
            Svrha = request.Svrha,
            Racun = sender.Racun 
        };

        sender.Racun.Transakcije.Add(transakcijaSender);

        var transakcijaReceiver = new Transakcija
        {
            Iznos = request.Iznos,
            Tip = "Primljeno",
            Datum = DateTime.Now,
            TekuciSender = sender.Racun?.BrojRacuna,
            TekuciReceiver = receiver.Racun?.BrojRacuna,
            Svrha = request.Svrha,
            Racun = receiver.Racun 
        };

        receiver.Racun.Transakcije.Add(transakcijaReceiver);

        Context.Transakcije.Add(transakcijaSender);
        Context.Transakcije.Add(transakcijaReceiver);

        Context.Korisnici.Update(sender);  
        Context.Korisnici.Update(receiver); 

        await Context.SaveChangesAsync();

        return Ok(new { transakcijaSender, transakcijaReceiver });
    }
    catch (Exception e)
    {
        return BadRequest("Greska: " + e.Message);
    }
}




    [HttpPost]
    [Route("getReceiver")]
    public async Task<ActionResult> getRecv([FromBody] AccountRequest request)
    {
        try
        {
            var receiver = await Context.Korisnici.Include(k=>k.Racun).FirstOrDefaultAsync(l=> l.Racun.BrojRacuna == request.tekuciReceiver);

            if(receiver == null)
                return BadRequest("Korisnik ne postoji.");
            
            return Ok(new { receiver });

        }
        catch (Exception e)
        {
            
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("getSender")]
    public async Task<ActionResult> getSender([FromBody] AccountRequest request)
    {
        try
        {
            var sender = await Context.Korisnici.Include(k=>k.Racun).FirstOrDefaultAsync(l=> l.Racun.BrojRacuna == request.tekuciReceiver);

            if(sender == null)
                return BadRequest("Korisnik ne postoji.");
            
            return Ok(new { sender });

        }
        catch (Exception e)
        {
            
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("getTransakcije")]
    public async Task<ActionResult> getTransakcije([FromBody] PinRequest request)
    {
        try
        {
            var user = await Context.Korisnici.Include(k=>k.Racun)
                                                    .ThenInclude(r=>r.Transakcije)
                                                    .FirstOrDefaultAsync(t=> t.Pin == request.Pin);
            if(user == null)
                return BadRequest("Racun ne postoji.");
            
            return Ok(new { message = "Transakacije od " + user.Ime,  user.Racun?.Transakcije  });
        }
        catch (Exception e)
        {
            return BadRequest("Greska " + e.Message);
        }
    }

    [HttpPut]
    [Route("changeValuta")]
    public async Task<ActionResult> promeniValutu([FromBody] string PinProvera, string valuta)
    {
        try
        {
            var user = await Context.Korisnici.Include(r=>r.Racun).FirstOrDefaultAsync(r=> r.Pin == PinProvera);
            if(user == null)
                return BadRequest("Racun ne postoji");

            if(user.Racun?.Valuta == valuta)
                return BadRequest("Racun je vec u toj valuti");
            decimal kurs;
            if(user.Racun?.Valuta == "RSD")
                kurs = 0.0085M;
            else
                kurs = 117.5M;
            
            if(user.Racun == null)
                return BadRequest("Racun ne postoji.");
            user.Racun.Valuta = valuta;
            user.Racun.Sredstva *= kurs;

            await Context.SaveChangesAsync();
            return Ok($"Nova valuta {user.Racun.Valuta} , stanje: {user.Racun.Sredstva}");

        }
        catch (Exception e)
        {
            return BadRequest("Greska " + e);
            
        }
    }
}

