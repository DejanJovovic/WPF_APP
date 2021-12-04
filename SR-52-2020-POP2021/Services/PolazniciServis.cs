using SR_52_2020_POP2021.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Services
{
    public class PolazniciServis : IAzuriranjaFajlova<Polaznik>
    {
        public ObservableCollection<Polaznik> citanjeFajla()
        {
            ObservableCollection<Polaznik> lst = new ObservableCollection<Polaznik>();

            string[] redovi = File.ReadAllLines(@"..\..\Resourses\polaznici.txt");
            foreach (string red in redovi)
            {
                //Nikola;Nikolic;2;M;3;nikolan@gmail.com;2;INSTRUKTOR;false;1
                string[] podaciReda = red.Split(';');

                string ime = podaciReda[0];
                string prezime = podaciReda[1];
                string jmbg = podaciReda[2];
                EPol pol = (EPol)Enum.Parse(typeof(EPol), podaciReda[3]);

                int idAdresa = int.Parse(podaciReda[4]);
                AdreseServis adrServis = new AdreseServis();
                Adresa adr = adrServis.citanjeFajla().Where(ad => ad.Id == idAdresa).FirstOrDefault();
                Adresa adresa = null;
                if (adr != null)
                    adresa = new Adresa(adr);

                string email = podaciReda[5];
                string lozinka = podaciReda[6];
                ETipKorisnika tipKorisnika = (ETipKorisnika)Enum.Parse(typeof(ETipKorisnika), podaciReda[7]);

                bool obrisano = Convert.ToBoolean(podaciReda[8]);


                Korisnik k = new Korisnik(ime, prezime, jmbg, pol, adresa, email, lozinka, tipKorisnika);
                Polaznik p = new Polaznik(k);
                p.obrisano = obrisano;

                lst.Add(p);
            }

            return lst;
        }

        public void upisFajla(ObservableCollection<Polaznik> lista)
        {
            StreamWriter sw = new StreamWriter(@"..\..\Resourses\polaznici.txt");
            foreach (Polaznik polaznik in Podaci.Instanca.lstPolaznici)
            {


                sw.WriteLine(polaznik.Korisnik.Ime + ";" + polaznik.Korisnik.Prezime + ";" + polaznik.Korisnik.Jmbg + ";" + polaznik.Korisnik.Pol + ";" +
                    polaznik.Korisnik.Adresa.Id + ";" + polaznik.Korisnik.Email + ";" + polaznik.Korisnik.Lozinka + ";" + polaznik.Korisnik.TipKorisnika + ";" +
                    polaznik.obrisano);
            }

            sw.Close();
        }
        //public Polaznik(Korisnik korisnik)
    }
}
