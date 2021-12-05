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
        public ObservableCollection<Korisnik> citanjeFajla() //cita sve admine, admini su klasa Korisnik
        {
            ObservableCollection<Korisnik> lst = new ObservableCollection<Korisnik>();//prvo prazna lista

            string[] redovi = File.ReadAllLines(@"..\..\Resourses\admini.txt");//iz admini.txt se cita
            foreach (string red in redovi)
            {
                string[] podaciReda = red.Split(';');//za svaki red podeli ti po delimiteru ; i za svaki property izdvojiti podatak

                string ime = podaciReda[0];
                string prezime = podaciReda[1];
                string jmbg = podaciReda[2];
                EPol pol = (EPol)Enum.Parse(typeof(EPol), podaciReda[3]);

                int idAdresa = int.Parse(podaciReda[4]);
                AdreseServis adrServis = new AdreseServis();
                Adresa adr = adrServis.citanjeFajla().Where(ad => ad.Id == idAdresa).FirstOrDefault();  //pozvan servis za adresu, u fajlu admina stoji id adrese, iz fajla adresa pronaci adresu
                Adresa adresa = null;
                if (adr != null)
                    adresa = new Adresa(adr);//setovana pronadjena adresa za ovog admina

                string email = podaciReda[5];
                string lozinka = podaciReda[6];
                ETipKorisnika tipKorisnika= (ETipKorisnika)Enum.Parse(typeof(ETipKorisnika), podaciReda[7]);

                bool obrisano = Convert.ToBoolean(podaciReda[8]);


                Korisnik admin = new Korisnik(ime, prezime, jmbg, pol, adresa, email, lozinka, tipKorisnika);//napravljen objekat admina
                admin.obrisano = obrisano;

                lst.Add(admin);//dodat u listu
            }

            return lst;
        }

        //prosledjuje se lista kod upisa/izmena/brisanja..  U bazi ce biti dovoljna jedna metoda za ove operacije i prosledjivace se odredjena sql naredba
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
