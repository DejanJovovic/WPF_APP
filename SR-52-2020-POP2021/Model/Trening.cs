using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Model
{
    public class Trening
    {
        int id;
        DateTime datumTreninga;
        string vremePocetka;
        int trajanjeTreninga;
        bool slobodan=true;
        Instruktor instruktor;
        Polaznik polaznik;

        public bool obrisano = false;//logicko brisanje

        public Trening(int id, DateTime datumTreninga, string vremePocetka, int trajanjeTreninga, bool slobodan, Instruktor instruktor, Polaznik polaznik)
        {
            this.Id = id;
            this.DatumTreninga = datumTreninga;
            this.VremePocetka = vremePocetka;
            this.TrajanjeTreninga = trajanjeTreninga;
            this.Slobodan = slobodan;
            this.Instruktor = instruktor;
            this.Polaznik = polaznik;
        }

        public Trening(Trening t)
        {
            this.Id = t.Id;
            this.DatumTreninga = t.DatumTreninga;
            this.VremePocetka = t.VremePocetka;
            this.TrajanjeTreninga = t.TrajanjeTreninga;
            this.Slobodan = t.Slobodan;
            this.Instruktor = new Instruktor(t.Instruktor);
            this.Polaznik = new Polaznik(t.Polaznik);

            this.obrisano = t.obrisano;

        }

        public int Id { get => id; set => id = value; }
        public DateTime DatumTreninga { get => datumTreninga; set => datumTreninga = value; }
        public string VremePocetka { get => vremePocetka; set => vremePocetka = value; }
        public int TrajanjeTreninga { get => trajanjeTreninga; set => trajanjeTreninga = value; }
        public bool Slobodan { get => slobodan; set => slobodan = value; }
        public Instruktor Instruktor { get => instruktor; set => instruktor = value; }
        public Polaznik Polaznik { get => polaznik; set => polaznik = value; }

       
    }
}
