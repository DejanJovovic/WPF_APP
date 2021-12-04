using SR_52_2020_POP2021.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Model
{
    public sealed class Podaci
    {


        private static readonly Podaci instanca = new Podaci();

        private AdreseServis adreseServis;
        private FitnesCentriServis fitnesCentriServis;
        private TreninziServis treninziServis;
        private AdminiServis adminiServis;
        private InstruktoriServis instruktoriServis;
        private PolazniciServis polazniciServis;


        private Podaci()
        {
            adreseServis = new AdreseServis();
            fitnesCentriServis = new FitnesCentriServis();
            treninziServis = new TreninziServis();
            adminiServis = new AdminiServis();
            polazniciServis = new PolazniciServis();
            instruktoriServis = new InstruktoriServis();
        }

        static Podaci() { }

        public static Podaci Instanca
        {
            get
            {
                return instanca;
            }
        }

        public ObservableCollection<Adresa> lstAdrese = new ObservableCollection<Adresa>();
        public ObservableCollection<FitnesCentar> lstFitnesCentri = new ObservableCollection<FitnesCentar>();
        public ObservableCollection<Trening> lstTreninzi = new ObservableCollection<Trening>();
        public ObservableCollection<Korisnik> lstAdmini = new ObservableCollection<Korisnik>();
        public ObservableCollection<Instruktor> lstInstruktori = new ObservableCollection<Instruktor>();
        public ObservableCollection<Polaznik> lstPolaznici = new ObservableCollection<Polaznik>();

        public void ucitajFajlove()
        {
            lstAdrese = adreseServis.citanjeFajla();
            lstFitnesCentri = fitnesCentriServis.citanjeFajla();
            lstInstruktori = instruktoriServis.citanjeFajla();
            lstAdmini = adminiServis.citanjeFajla();
            lstPolaznici = polazniciServis.citanjeFajla();
            lstTreninzi = treninziServis.citanjeFajla();

            ucitajTreninge_InstruktoriPolaznici();
        }

        //ucitane treninge treba dodati u liste odgovarajucih instruktora i polaznika
        void ucitajTreninge_InstruktoriPolaznici()
        {
            foreach(Trening t in lstTreninzi)
            {
                foreach(Instruktor instr in lstInstruktori)
                {
                    if (instr.Korisnik.Jmbg == t.Instruktor.Korisnik.Jmbg)
                        instr.lstTreninzi.Add(new Trening(t));
                }
                foreach (Polaznik polaznik in lstPolaznici)
                {
                    if(t.Polaznik!=null)
                        if (polaznik.Korisnik.Jmbg == t.Polaznik.Korisnik.Jmbg)
                            polaznik.lstTreninzi.Add(new Trening(t));
                }
            }
        }




        //public void Initialize()
        //{

        //    Korisnici = new ObservableCollection<Korisnik>();
        //    Instruktori = new ObservableCollection<Instruktor>();

        //    Adresa adresa = new Adresa
        //    {
        //        Grad = "Novi Sad",
        //        Drzava = "Srbija",
        //        Ulica = "ulica1",
        //        Broj = "22",
        //        ID = "1"
        //    };

        //    Korisnik korisnik1 = new Korisnik();
        //    korisnik1.Ime = "Pera";
        //    korisnik1.Prezime = "Peric";
        //    korisnik1.Email = "pera@gmail.com";
        //    korisnik1.JMBG = "121346";
        //    korisnik1.Lozinka = "peki";
        //    // korisnik1.Adresa = adresa;
        //    korisnik1.Pol = EPol.M;
        //    korisnik1.TipKorisnika = ETipKorisnika.ADMINISTRATOR;
        //    korisnik1.Aktivan = true;

        //    Korisnik korisnik2 = new Korisnik
        //    {
        //        Email = "mika@gmail.com",
        //        Ime = "mika",
        //        Prezime = "Mikic",
        //        JMBG = "121346",
        //        Lozinka = "zika",
        //        Pol = EPol.Z,
        //        TipKorisnika = ETipKorisnika.INSTRUKTOR,
        //        //Adresa = adresa
        //        Aktivan = true
        //    };

        //    Instruktor korisnik4 = new Instruktor
        //    {
        //        Korisnik = korisnik2,
        //    };

        //    Korisnici.Add(korisnik1);
        //    Korisnici.Add(korisnik2);
        //    Instruktori.Add(korisnik4);
        //}

        //public void SacuvajEntitet(string filename)
        //{
        //    if (filename.Contains("korisnici"))
        //    {
        //        userService.SaveUsers(filename);
        //    }
        //    else if (filename.Contains("instruktori"))
        //    {
        //        instruktorService.SaveUsers(filename);
        //    }
        //}

        //public void CitanjeEntiteta(string filename)
        //{
        //    if (filename.Contains("korisnici"))
        //    {
        //        userService.ReadUsers(filename);
        //    }
        //    else if (filename.Contains("instruktori"))
        //    {
        //        instruktorService.ReadUsers(filename);
        //    }
        //}

        //public void DeleteUser(string email)
        //{
        //    userService.DeleteUser(email);
        //}

        //public void SacuvajUBin(string filename)
        //{
        //    IFormatter formatter = new BinaryFormatter();
        //    using (Stream stream = new FileStream(@"../../Resources/" + filename, FileMode.Create, FileAccess.Write))
        //    {
        //        if (filename.Contains("korisnici"))
        //        {
        //            formatter.Serialize(stream, Podaci.Instanca.Korisnici);
        //        }
        //        else if (filename.Contains("instruktori"))
        //        {
        //            formatter.Serialize(stream, Podaci.Instanca.Instruktori);
        //        }
        //    }
        //}

        //public void CitajIzBin(string filename)
        //{
        //    IFormatter formatter = new BinaryFormatter();
        //    using (Stream stream = new FileStream(@"../../Resources/" + filename, FileMode.Open, FileAccess.Read))
        //    {
        //        if (filename.Contains("korisnici"))
        //        {
        //            Korisnici = (ObservableCollection<Korisnik>)formatter.Deserialize(stream);
        //        }
        //        else if (filename.Contains("instruktori"))
        //        {
        //            Instruktori = (ObservableCollection<Instruktor>)formatter.Deserialize(stream);
        //        }
        //    }
        //}
    }
}
