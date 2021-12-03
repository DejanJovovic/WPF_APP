using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Model
{
    public class Instruktor
    {
        Korisnik korisnik;
        int idFitnesCentra;
        public ObservableCollection<Trening> lstTreninzi = new ObservableCollection<Trening>();

        public bool obrisano = false;//logicko brisanje

        public Instruktor(Korisnik korisnik, int idFitnesCentra)
        {
            this.Korisnik = korisnik;
            this.IdFitnesCentra = idFitnesCentra;
        }

        public Korisnik Korisnik { get => korisnik; set => korisnik = value; }
        public int IdFitnesCentra { get => idFitnesCentra; set => idFitnesCentra = value; }

        public override string ToString()
        {
            return korisnik.ToString() + ", idFitnes centra: " + IdFitnesCentra + ", Obrisan Instr: " + obrisano;
        }
    }
}
