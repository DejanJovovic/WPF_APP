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
        public string _id;

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public string _ulica;

        public string Ulica
        {
            get { return _ulica; }
            set { _ulica = value; }
        }

        public string _broj;

        public string Broj
        {
            get { return _broj; }
            set { _broj = value; }
        }

        public string _grad;

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

        public override string ToString()
        {
            return "Ulica " + Ulica + " broj " + Broj + " grad" + Grad + " drzava " + Drzava;
        }
    }
}
