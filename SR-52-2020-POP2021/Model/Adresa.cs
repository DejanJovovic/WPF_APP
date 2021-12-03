using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Model
{

    public class Adresa
    {

        int id;
        string ulica;
        string broj;
        string grad;
        string drzava;
        public bool obrisano = false;//logicko brisanje

        public Adresa(int id, string ulica, string broj, string grad, string drzava)
        {
            this.Id = id;
            this.Ulica = ulica;
            this.Broj = broj;
            this.Grad = grad;
            this.Drzava = drzava;
        }

        public int Id { get => id; set => id = value; }
        public string Ulica { get => ulica; set => ulica = value; }
        public string Broj { get => broj; set => broj = value; }
        public string Grad { get => grad; set => grad = value; }
        public string Drzava { get => drzava; set => drzava = value; }

        public override string ToString()
        {
            return "Ulica: " + Ulica + " Broj: " + Broj + " Grad: " + Grad + " Drzava: " + Drzava;
        }
    }
}
