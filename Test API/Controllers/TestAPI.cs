using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.Json;

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
                        var p = context.korisnik.ToList();
                        return p.Count;
                    }
                    else return 0;
                }
            }
        }

        //Get metoda koja vraca objekat iz liste unet preko Post metode sa odgovarajucim Id-jem u json formatu.
        [HttpGet, Route("[action]")] //Primer Get3 poziva: localhost:port/testapi/Get3?id=1
        public ActionResult Get2([FromQuery] int id)
        {

            using (var context = new Korisnici())
            {


                if (context.Database.CanConnect() == true)
                {
                    if (context.korisnik.Any(o => o.Id == id))
                    {
                        var p = context.korisnik.Single(a => a.Id == id);

                        return new JsonResult(p);
                    }
                    else return NotFound(null);
                }
                else return NotFound(null);
            }
        }



        
        [HttpGet, Route("[action]")] 
        public ActionResult Get3([FromQuery] int id)// primer metode koja vraca podatke iz dve tabele po id-u
        {

            var context = new Korisnici();
               

                var query = from Korisnik in context.korisnik
                            join KorisnikUredjaj in context.korisnikUredj
                                on Korisnik.Id equals KorisnikUredjaj.Id
                            where Korisnik.Id == KorisnikUredjaj.Id && Korisnik.Id == id
                            select new { Korisnik, KorisnikUredjaj };

            if (query.Any(o => o.Korisnik.Id == id))
            {





                return new JsonResult(query);
            }
            else return NotFound(null);
                       
            
        }

        [HttpGet, Route("[action]")] 
        public ActionResult Get4([FromQuery] int id)
        {

            using (var context = new Korisnici())
            {


                if (context.Database.CanConnect() == true)
                {
                    if (context.korisnikUredj.Any(o => o.Id == id))
                    {
                        var p = context.korisnikUredj.Single(a => a.Id == id);

                        return new JsonResult(p);
                    }
                    else return NotFound(null);
                }
                else return NotFound(null);
            }
        }



        [HttpPost, Route("[action]")]

        public ActionResult Post([FromBody] Korisnik korisnik)
        {
            Korisnik a = korisnik;


            using (var context = new Korisnici())
            {
                    context.Database.EnsureCreated();
                    context.Database.OpenConnection();
                
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.korisnik ON");
                    context.korisnik.Add(a);
                    


                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.korisnik OFF");
                    context.SaveChanges();

                var p = context.korisnik.Single(c => c.Id == a.Id);

                    context.Database.CloseConnection();


                if (p.Id == a.Id)
                    {
                        return new JsonResult(p);

                    }
                    else return NotFound(null);

                
            }

        }

        [HttpPost, Route("[action]")]

        public ActionResult Post2([FromBody] KorisnikUredjaj korisnikU)
        {
            KorisnikUredjaj a = korisnikU;


            using (var context = new Korisnici())
            {
                context.Database.EnsureCreated();
                context.Database.OpenConnection();

                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.korisnikUredj ON");
                context.korisnikUredj.Add(a);
                


                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.korisnikUredj OFF");
                context.SaveChanges();

                var p = context.korisnikUredj.Single(c => c.Id == a.Id);

                context.Database.CloseConnection();


                if (p.Id == a.Id)
                {
                    return new JsonResult(p);

                }
                else return NotFound(null);


            }

        }

        [HttpPost, Route("[action]")]

        public ActionResult Post3([FromBody]  KorisnikJSON korJson) // Primer poziva { "kor": { "id":7,"ime":"milos","prezime":"kicovic"} , "korU": {"id": 7, "naziv": "Android", "verzija": "12"} }
        {

            Korisnik ka = korJson.kor;
            KorisnikUredjaj kb = korJson.korU;



            var context = new Korisnici();
            
                context.Database.EnsureCreated();
                context.Database.OpenConnection();

                
                
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.korisnik ON");
                context.korisnik.Add(ka);
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.korisnik OFF");
                context.SaveChanges();

                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.korisnikUredj ON");
                context.korisnikUredj.Add(kb);
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.korisnik OFF");
                context.SaveChanges();

                var query = from Korisnik in context.korisnik
                            join KorisnikUredjaj in context.korisnikUredj
                                on Korisnik.Id equals KorisnikUredjaj.Id
                            where Korisnik.Id == KorisnikUredjaj.Id && Korisnik.Id == ka.Id
                            select new { Korisnik, KorisnikUredjaj };

            context.Database.CloseConnection();

            if (kb.Id == ka.Id)
                {
                    return new JsonResult(query);

                }
                else return NotFound(null);

                


                


            }

        }

    }



