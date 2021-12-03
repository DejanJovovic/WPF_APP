using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Model
{

    public class Korisnik
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
        }

        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public string Jmbg { get => jmbg; set => jmbg = value; }
        public EPol Pol { get => pol; set => pol = value; }
        public Adresa Adresa { get => adresa; set => adresa = value; }
        public string Email { get => email; set => email = value; }
        public string Lozinka { get => lozinka; set => lozinka = value; }
        public ETipKorisnika TipKorisnika { get => tipKorisnika; set => tipKorisnika = value; }

       
    }

}