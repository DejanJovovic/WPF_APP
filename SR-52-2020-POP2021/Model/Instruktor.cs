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

        string imePrezime;

        public Instruktor(Korisnik korisnik, int idFitnesCentra)
        {
            this.Korisnik = korisnik;
            this.IdFitnesCentra = idFitnesCentra;

            this.ImePrezime = korisnik.Ime + " " + korisnik.Prezime;
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

            this.ImePrezime = instr.Korisnik.Ime + " " + instr.Korisnik.Prezime;

        }
        //public Korisnik(string ime, string prezime, string jmbg, EPol pol, Adresa adresa, string email, string lozinka, ETipKorisnika tipKorisnika)
        // public Adresa(int id, string ulica, string broj, string grad, string drzava)

        public Korisnik Korisnik { get => korisnik; set => korisnik = value; }
        public int IdFitnesCentra { get => idFitnesCentra; set => idFitnesCentra = value; }
        public string ImePrezime { get => imePrezime; set => imePrezime = value; }

        public override string ToString()
        {
            return korisnik.ToString() + ", idFitnes centra: " + IdFitnesCentra + ", Obrisan Instr: " + obrisano;
        }
    }
}
