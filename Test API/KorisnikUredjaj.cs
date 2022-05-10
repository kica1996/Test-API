
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Test_API
{
    public class KorisnikUredjaj
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        private int? id;
        private string naziv;
        private string verzija;

        public int? Id { get => id; set => id = value; }
        public string Naziv { get => naziv; set => naziv = value; }
        public string Verzija { get => verzija; set => verzija = value; }

        public KorisnikUredjaj(int id, string naziv, string prezime)
        {
            Id = id;
            Naziv = naziv;
            Verzija = verzija;
        }

        public KorisnikUredjaj()
        {
            Id = 0;
            Naziv = "";
            Verzija = "";
        }
    }
}

