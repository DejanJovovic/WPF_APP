using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Model
{
    public class Polaznik: INotifyPropertyChanged
    {
        Korisnik korisnik;
        public ObservableCollection<Trening> lstTreninzi = new ObservableCollection<Trening>();

        public bool obrisano = false;//logicko brisanje

        string jmbg;
        string imePrezime;

        public Polaznik(Korisnik korisnik)
        {
            this.Korisnik = korisnik;

            this.jmbg = korisnik.Jmbg;
            this.ImePrezime = korisnik.Ime + " " + korisnik.Prezime;
        }
        public Polaznik()
        {
            this.Korisnik = new Korisnik();
            this.Korisnik.Adresa = new Adresa();
        }
        public Polaznik(Polaznik polaznik)
        {
            this.Korisnik = new Korisnik();

            this.Korisnik.Ime = polaznik.Korisnik.Ime;
            this.Korisnik.Prezime = polaznik.Korisnik.Prezime;
            this.Korisnik.Jmbg = polaznik.Korisnik.Jmbg;
            this.Korisnik.Pol = polaznik.Korisnik.Pol;

            this.Korisnik.Adresa.Id = polaznik.Korisnik.Adresa.Id;
            this.Korisnik.Adresa.Ulica = polaznik.Korisnik.Adresa.Ulica;
            this.Korisnik.Adresa.Broj = polaznik.Korisnik.Adresa.Broj;
            this.Korisnik.Adresa.Grad = polaznik.Korisnik.Adresa.Grad;
            this.Korisnik.Adresa.Drzava = polaznik.Korisnik.Adresa.Drzava;

            this.Korisnik.Email = polaznik.Korisnik.Email;
            this.Korisnik.Lozinka = polaznik.Korisnik.Lozinka;
            this.Korisnik.TipKorisnika = polaznik.Korisnik.TipKorisnika;

            this.obrisano = polaznik.obrisano;

            this.jmbg = polaznik.Korisnik.Jmbg;
            this.ImePrezime = polaznik.Korisnik.Ime + " " + polaznik.Korisnik.Prezime;

        }
        //public Korisnik(string ime, string prezime, string jmbg, EPol pol, Adresa adresa, string email, string lozinka, ETipKorisnika tipKorisnika)
        // public Adresa(int id, string ulica, string broj, string grad, string drzava)


        //public Korisnik Korisnik { get => korisnik; set => korisnik = value; }
        public Korisnik Korisnik
        {
            get
            {
                return korisnik;
            }
            set
            {
                korisnik = value;
                OnPropertyChanged("Korisnik");
            }
        }

        public string Jmbg { get => jmbg; set => jmbg = value; }
        public string ImePrezime { get => imePrezime; set => imePrezime = value; }
    

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
            return korisnik.ToString() + ", Obrisan POlaznik: " + obrisano;
        }
    }
}
