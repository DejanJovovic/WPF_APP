using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Model
{
    public class FitnesCentar
    {
        int id;
        string naziv;
        Adresa adresa;
        public bool obrisano = false;//logicko brisanje

        public FitnesCentar(int id, string naziv, Adresa adresa)
        {
            this.Id = id;
            this.Naziv = naziv;
            this.Adresa = adresa;
        }

        public int Id { get => id; set => id = value; }
        public string Naziv { get => naziv; set => naziv = value; }
        public Adresa Adresa { get => adresa; set => adresa = value; }
    }
}
