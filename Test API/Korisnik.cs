using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Test_API
{
    public class Korisnik
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        private int? id;
        private string ime;
        private string prezime;

        public int? Id { get => id; set => id = value; }
        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        

        public Korisnik(int id, string ime, string prezime)
        {
            Id = id;
            Ime = ime;
            Prezime = prezime;
        }

        public Korisnik()
        {
            Id = 0;
            Ime = "";
            Prezime = "";
        }
    }

   
}
