using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Model
{
    public class Instruktor : INotifyPropertyChanged
    {
        Korisnik korisnik;
        int idFitnesCentra;
        public ObservableCollection<Trening> lstTreninzi = new ObservableCollection<Trening>();

        public bool obrisano = false;//logicko brisanje

        string jmbg;
        string imePrezime;

        public Instruktor(Korisnik korisnik, int idFitnesCentra)
        {
            this.Korisnik = korisnik;
            this.IdFitnesCentra = idFitnesCentra;

            this.Jmbg = korisnik.Jmbg;
            this.ImePrezime = korisnik.Ime + " " + korisnik.Prezime;
        }
        public Instruktor()
        {
            this.Korisnik = new Korisnik();
            this.Korisnik.Adresa = new Adresa();
            this.idFitnesCentra = 1;
        }

        public Instruktor(Instruktor instr)
        {
            this.Korisnik = new Korisnik();

            this.Korisnik.Ime = instr.Korisnik.Ime;
            this.Korisnik.Prezime = instr.Korisnik.Prezime;
            this.Korisnik.Jmbg = instr.Korisnik.Jmbg;
            this.Korisnik.Pol = instr.Korisnik.Pol;

            this.Korisnik.Adresa.Id = instr.Korisnik.Adresa.Id;
            this.Korisnik.Adresa.Ulica = instr.Korisnik.Adresa.Ulica;
            this.Korisnik.Adresa.Broj = instr.Korisnik.Adresa.Broj;
            this.Korisnik.Adresa.Grad = instr.Korisnik.Adresa.Grad;
            this.Korisnik.Adresa.Drzava = instr.Korisnik.Adresa.Drzava;

            this.Korisnik.Email = instr.Korisnik.Email;
            this.Korisnik.Lozinka = instr.Korisnik.Lozinka;
            this.Korisnik.TipKorisnika = instr.Korisnik.TipKorisnika;

            this.IdFitnesCentra = instr.IdFitnesCentra;

            this.obrisano = instr.obrisano;

            this.Jmbg = instr.Jmbg;
            this.ImePrezime = instr.Korisnik.Ime + " " + instr.Korisnik.Prezime;

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
        //public int IdFitnesCentra { get => idFitnesCentra; set => idFitnesCentra = value; }
        public int IdFitnesCentra
        {
            get
            {
                return idFitnesCentra;
            }
            set
            {
                idFitnesCentra = value;
                OnPropertyChanged("IdFitnesCentra");
            }
        }



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
            return korisnik.ToString() + ", idFitnes centra: " + IdFitnesCentra + ", Obrisan Instr: " + obrisano;
        }
    }
}
