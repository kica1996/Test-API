using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Test_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestAPI : ControllerBase
    {


        private readonly ILogger<TestAPI> _logger;

        public TestAPI(ILogger<TestAPI> logger)
        {
            _logger = logger;

        }

        //Lista objekata sa tipom Korisnik velicine 100.
        public static List<Korisnik> lista = new List<Korisnik>(100);






        //Get metoda koja vraca broj objekata u listi.
        [HttpGet]
        public int Get()
        {
            return lista.Count;
        }

        [HttpGet, Route("[action]")] //Primer Get poziva: localhost:port/testapi/Get2?broj1=2&broj2=3

        //Get metoda koja mnozi 10 sa prvim i drugim parametrom
        public int Get2([FromQuery] int broj1, [FromQuery] int broj2)
        {
            int a = 10;
            int b = broj2;
            int c = a * b;
            int z = c * broj1;

            return z;
        }

        //Get metoda koja vraca objekat iz liste unet preko Post metode sa odgovarajucim Id-jem u json formatu.
        [HttpGet, Route("[action]")] //Primer Get3 poziva: localhost:port/testapi/Get3?id=1
        public ObjectResult Get3([FromQuery] int id)
        {
            if (lista.Any(x => x.Id == id) == true)
            {
                return Ok(lista.Find(item => item.Id == id));
            }

            else return NotFound(null);


        }

        //Post metoda koja kreira novi objekat klase Korisnik,ubacuje ga u listu i ispisuje u json formatu.
        [HttpPost, Route("[action]")]//Primer Post poziva(Postman): localhost:port/testapi/Post?id=1&ime=milos&prezime=kicovic

        public ObjectResult Post([FromBody] Korisnik korisnik)
        {
            Korisnik a = korisnik;
            lista.Insert(a.Id, a);//Nedostatak: id mora poceti od 0 i mora ici redom,ako unesemo id 1 bez objekta sa id-jem 0 pojavice se greska.

            if (lista.Any(x => x.Id == a.Id) == true)
            {
                return Ok(lista.Find(item => item.Id == a.Id));

            }
            else return NotFound(null);

        }


    }

    }

