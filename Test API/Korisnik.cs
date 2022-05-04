namespace Test_API
{
    public class Korisnik
    {
        private int id;
        private string ime;
        private string prezime;

        public int Id { get => id; set => id = value; }
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
