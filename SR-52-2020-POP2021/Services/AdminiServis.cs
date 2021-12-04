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
    public class AdminiServis : IAzuriranjaFajlova<Korisnik>
    {
        public ObservableCollection<Korisnik> citanjeFajla()
        {
            ObservableCollection<Korisnik> lst = new ObservableCollection<Korisnik>();

            string[] redovi = File.ReadAllLines(@"..\..\Resourses\admini.txt");
            foreach (string red in redovi)
            {
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
                ETipKorisnika tipKorisnika= (ETipKorisnika)Enum.Parse(typeof(ETipKorisnika), podaciReda[7]);

                bool obrisano = Convert.ToBoolean(podaciReda[8]);


                Korisnik admin = new Korisnik(ime, prezime, jmbg, pol, adresa, email, lozinka, tipKorisnika);
                admin.obrisano = obrisano;

                lst.Add(admin);
            }

            return lst;
        }

        public void upisFajla(ObservableCollection<Korisnik> lista)
        {
            StreamWriter sw = new StreamWriter(@"..\..\Resourses\admini.txt");
            foreach (Korisnik admin in Podaci.Instanca.lstAdmini)
            {
               

                sw.WriteLine(admin.Ime + ";" + admin.Prezime + ";" + admin.Jmbg + ";" + admin.Pol + ";" +
                    admin.Adresa.Id + ";" + admin.Email + ";" + admin.Lozinka + ";" + admin.TipKorisnika + ";" + admin.obrisano);
            }

            sw.Close();
        }

        //public Korisnik(string ime, string prezime, string jmbg, EPol pol, Adresa adresa, string email, string lozinka, ETipKorisnika tipKorisnika)
    }
}
