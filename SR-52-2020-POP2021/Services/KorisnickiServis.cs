using SR_52_2020_POP2021.Model;
using SR_52_2020_POP2021.MyExceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Services
{
    public class KorisnickiServis : IKorisnickiServis
    {
        public void DeleteUser(string email)
        {

            Korisnik Korisnik = Podaci.Instanca.Korisnici.ToList().Find(korisnik => korisnik.Email.Equals(email));
            if (Korisnik == null)
            {
                throw new KorisnikNePostojiException($"Ne postoji korisnik sa emailom: {email}");
            }

            Korisnik.Aktivan = false;
            Console.WriteLine("Uspesno obrisan korisnik sa emailom:" + email);


            Podaci.Instanca.SacuvajEntitet("korisnici.txt");
        }

        public void ReadUsers(string filename)
        {
            Podaci.Instanca.Korisnici = new ObservableCollection<Korisnik>();

            using (StreamReader file = new StreamReader(@"../../Resources/" + filename))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    string[] korisnikIzFajla = line.Split(';');

                    Enum.TryParse(korisnikIzFajla[5], out EPol pol);
                    Enum.TryParse(korisnikIzFajla[6], out ETipKorisnika tip);
                    Boolean.TryParse(korisnikIzFajla[7], out Boolean status);
                    Korisnik Korisnik = new Korisnik
                    {

                        Ime = korisnikIzFajla[0],
                        Prezime = korisnikIzFajla[1],
                        Email = korisnikIzFajla[2],
                        Lozinka = korisnikIzFajla[3],
                        JMBG = korisnikIzFajla[4],
                        Pol = pol,
                        TipKorisnika = tip,
                        Aktivan = status
                    };

                    Podaci.Instanca.Korisnici.Add(Korisnik);
                }
            }
        }

        public void SaveUsers(string filename)
        {
            using (StreamWriter file = new StreamWriter(@"../../Resources/" + filename))
            {
                foreach (Korisnik Korisnik in Podaci.Instanca.Korisnici)
                {
                    file.WriteLine(Korisnik.KorisnikZaUpisUFajl());
                }
            }
        }
    }
}
