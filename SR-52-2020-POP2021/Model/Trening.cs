using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Model
{
    public class Trening : INotifyPropertyChanged
    {
        int id;
        DateTime datumTreninga;
        string vremePocetka;
        int trajanjeTreninga;
        bool slobodan=true;
        Instruktor instruktor;
        Polaznik polaznik;

        string imePrezimePolaznika;
        string imePrezimeInstruktora;

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

            if (this.Polaznik != null)
                this.ImePrezimePolaznika = this.Polaznik.Korisnik.Ime + " " + this.Polaznik.Korisnik.Prezime;
            if(this.Instruktor!=null)
                this.ImePrezimeInstruktora = this.Instruktor.Korisnik.Ime + " " + this.Instruktor.Korisnik.Prezime;

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

            if(t.Polaznik!=null)
                this.ImePrezimePolaznika = t.ImePrezimePolaznika;
            if (t.Instruktor != null)
                this.ImePrezimeInstruktora = t.ImePrezimeInstruktora;

            this.obrisano = t.obrisano;

        }

        //public int Id { get => id; set => id = value; }
        //public DateTime DatumTreninga { get => datumTreninga; set => datumTreninga = value; }
        //public string VremePocetka { get => vremePocetka; set => vremePocetka = value; }
        //public int TrajanjeTreninga { get => trajanjeTreninga; set => trajanjeTreninga = value; }
        //public bool Slobodan { get => slobodan; set => slobodan = value; }
        //public Instruktor Instruktor { get => instruktor; set => instruktor = value; }
        //public Polaznik Polaznik { get => polaznik; set => polaznik = value; }


        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public DateTime DatumTreninga
        {
            get
            {
                return datumTreninga;
            }
            set
            {
                datumTreninga = value;
                OnPropertyChanged("DatumTreninga");
            }
        }
        public string VremePocetka
        {
            get
            {
                return vremePocetka;
            }
            set
            {
                vremePocetka = value;
                OnPropertyChanged("VremePocetka");
            }
        }
        public int TrajanjeTreninga
        {
            get
            {
                return trajanjeTreninga;
            }
            set
            {
                trajanjeTreninga = value;
                OnPropertyChanged("TrajanjeTreninga");
            }
        }
        public bool Slobodan
        {
            get
            {
                return slobodan;
            }
            set
            {
                slobodan = value;
                OnPropertyChanged("Slobodan");
            }
        }

        public Instruktor Instruktor
        {
            get
            {
                return instruktor;
            }
            set
            {
                instruktor = value;
                OnPropertyChanged("Instruktor");
            }
        }
        public Polaznik Polaznik
        {
            get
            {
                return polaznik;
            }
            set
            {
                polaznik = value;
                OnPropertyChanged("Polaznik");
            }
        }

        public string ImePrezimePolaznika { get => imePrezimePolaznika; set => imePrezimePolaznika = value; }
        public string ImePrezimeInstruktora { get => imePrezimeInstruktora; set => imePrezimeInstruktora = value; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public override string ToString()
        {
            return Id + " " + DatumTreninga + " " + VremePocetka + " " + TrajanjeTreninga + " " + Slobodan + ", instruktor:" + Instruktor + ", polaznik:" + Polaznik;
        }

        // public Trening(int id, DateTime datumTreninga, string vremePocetka, int trajanjeTreninga, bool slobodan, Instruktor instruktor, Polaznik polaznik)


    }
}
