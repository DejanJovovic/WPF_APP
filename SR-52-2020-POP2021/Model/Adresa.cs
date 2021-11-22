using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Model
{
    [Serializable]

    public class Adresa
    {
        private string _sifra;

        public string Sifra
        {
            get { return _sifra; }
            set { _sifra = value; }
        }

        private string _ulica;

        public string Ulica
        {
            get { return _ulica; }
            set { _ulica = value; }
        }

        private string _broj;

        public string Broj
        {
            get { return _broj; }
            set { _broj = value; }
        }

        private string _grad;

        public string Grad
        {
            get { return _grad; }
            set { _grad = value; }
        }

        private string _drzava;

        public string Drzava
        {
            get { return _drzava; }
            set { _drzava = value; }
        }

        public Adresa()
        {

        }
        public override string ToString()
        {
            return "Adresa: " + Ulica + " " + Broj;
        }

    }
}
