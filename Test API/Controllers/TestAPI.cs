using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;




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
        
        [HttpGet]
        public int Get()
        {

            {
                using (var context = new Korisnici())
                {
                    if (context.Database.CanConnect() == true)
                    {
                        var p = context.svi.ToList();
                        return p.Count;
                    }
                    else return 0;
                }
            }
        }





        //Get metoda koja vraca objekat iz liste unet preko Post metode sa odgovarajucim Id-jem u json formatu.
        [HttpGet, Route("[action]")] //Primer Get3 poziva: localhost:port/testapi/Get3?id=1
        public ObjectResult Get3([FromQuery] int id)
        {

            using (var context = new Korisnici())
            {

                if (context.Database.CanConnect() == true)
                {
                    if (context.svi.Any(o => o.Id == id))
                    {
                        var p = context.svi.Single(a => a.Id == id);

                        return Ok(p);
                    }
                    else return NotFound(null);
                }
                else return NotFound(null);
            }
        }

    

        
        [HttpPost, Route("[action]")]

        public ObjectResult Post([FromBody] Korisnik korisnik)
        {
            Korisnik a = korisnik;


            using (var context = new Korisnici())
            {
                    context.Database.EnsureCreated();
                    context.Database.OpenConnection();
                
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.svi ON");
                    context.Add(a);
                    context.SaveChanges();


                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.svi OFF");
    
                    var p = context.svi.Single(c => c.Id == a.Id);

                    context.Database.CloseConnection();


                if (p.Id == a.Id)
                    {
                        return Ok(p);

                    }
                    else return NotFound(null);

                
            }

        }


    }

}

