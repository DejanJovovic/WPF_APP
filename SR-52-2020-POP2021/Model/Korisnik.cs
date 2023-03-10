using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Model
{

    public class Korisnik: INotifyPropertyChanged   //za data binding
    {

        string ime;
        string prezime;
        string jmbg;//identifikacija
        EPol pol;
        Adresa adresa;
        string email;
        string lozinka;
        ETipKorisnika tipKorisnika;

        public bool obrisano = false;//logicko brisanje

        string imePrezime;

        public Korisnik(string ime, string prezime, string jmbg, EPol pol, Adresa adresa, string email, string lozinka, ETipKorisnika tipKorisnika)
        {
            this.Ime = ime;
            this.Prezime = prezime;
            this.Jmbg = jmbg;
            this.Pol = pol;
            this.Adresa = adresa;
            this.Email = email;
            this.Lozinka = lozinka;
            this.TipKorisnika = tipKorisnika;

            this.ImePrezime = Ime + " " + Prezime;
        }

        public Korisnik() {

            this.Adresa = new Adresa();
        }

        public Korisnik(Korisnik k)
        {
            this.Adresa = new Adresa();
            this.Ime = k.Ime;
            this.Prezime = k.Prezime;
            this.Jmbg = k.Jmbg;
            this.Pol = k.Pol;

            this.Adresa.Id = k.Adresa.Id;
            this.Adresa.Ulica = k.Adresa.Ulica;
            this.Adresa.Broj = k.Adresa.Broj;
            this.Adresa.Grad = k.Adresa.Grad;
            this.Adresa.Drzava = k.Adresa.Drzava;

            this.Email = k.Email;
            this.Lozinka = k.Lozinka;
            this.TipKorisnika = k.TipKorisnika;
            this.obrisano = k.obrisano;

            this.ImePrezime = k.Ime + " " + k.Prezime;
        }

        //public string Ime { get => ime; set => ime = value; }
        //public string Prezime { get => prezime; set => prezime = value; }
        //public string Jmbg { get => jmbg; set => jmbg = value; }
        //public EPol Pol { get => pol; set => pol = value; }
        //public Adresa Adresa { get => adresa; set => adresa = value; }
        //public string Email { get => email; set => email = value; }
        //public string Lozinka { get => lozinka; set => lozinka = value; }
        //public ETipKorisnika TipKorisnika { get => tipKorisnika; set => tipKorisnika = value; }

        public string Jmbg
        {
            get
            {
                return jmbg;
            }
            set
            {
                jmbg = value;
                OnPropertyChanged("Jmbg");
            }
        }
        public string Ime
        {
            get
            {
                return ime;
            }
            set
            {
                ime = value;
                OnPropertyChanged("Ime");
            }
        }
        public string Prezime
        {
            get
            {
                return prezime;
            }
            set
            {
                prezime = value;
                OnPropertyChanged("Prezime");
            }
        }
       
        public EPol Pol
        {
            get
            {
                return pol;
            }
            set
            {
                pol = value;
                OnPropertyChanged("Pol");
            }
        }

        public Adresa Adresa
        {
            get
            {
                return adresa;
            }
            set
            {
                adresa = value;
                OnPropertyChanged("Adresa");
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Lozinka
        {
            get
            {
                return lozinka;
            }
            set
            {
                lozinka = value;
                OnPropertyChanged("Lozinka");
            }
        }

        public ETipKorisnika TipKorisnika
        {
            get
            {
                return tipKorisnika;
            }
            set
            {
                tipKorisnika = value;
                OnPropertyChanged("TipKorisnika");
            }
        }

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
            return Ime + " " + Prezime + " " + Jmbg + " " + Pol + " " + Adresa + " " + Email + " " + Lozinka + " " + TipKorisnika;
        }


    }

}