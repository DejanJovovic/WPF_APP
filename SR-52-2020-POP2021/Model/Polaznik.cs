using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Model
{
    public class Polaznik
    {
        Korisnik korisnik;
        public ObservableCollection<Trening> lstTreninzi = new ObservableCollection<Trening>();

        public bool obrisano = false;//logicko brisanje

        public Polaznik(Korisnik korisnik)
        {
            this.Korisnik = korisnik;
        }
        public Korisnik Korisnik { get => korisnik; set => korisnik = value; }
    }
}
