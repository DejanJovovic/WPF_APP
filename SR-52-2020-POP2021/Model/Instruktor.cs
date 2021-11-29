using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Model
{
    [Serializable]
    public class Instruktor
    {
        private Korisnik _korisnik;

        public Korisnik Korisnik
        {
            get { return _korisnik; }
            set { _korisnik = value; }
        }

        public override string ToString()
        {
            return "Ja sam instruktor i moje ime je:" + _korisnik.Ime + ", a moj email je :" + _korisnik.Email;
        }

        public string InstruktorZaUpisUFajl()
        {
            return Korisnik.Email;
        }
    }
}
