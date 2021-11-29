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
    public class InstruktorServis : IInstruktorServis
    {
        public List<Korisnik> FindallClients(string email)
        {
            throw new NotImplementedException();
        }

        public void ReadUsers(string filename)
        {
            Podaci.Instanca.Instruktori = new ObservableCollection<Instruktor>();
            using (StreamReader file = new StreamReader(@"../../Resources/" + filename))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    string[] instruktorIzFajla = line.Split(';');
                    Korisnik registrovaniKorisnik = Podaci.Instanca.Korisnici.ToList().Find(korisnik => korisnik.Email.Equals(instruktorIzFajla[0]));

                    Instruktor instruktor = new Instruktor
                    {

                        Korisnik = registrovaniKorisnik,
                    };

                    Podaci.Instanca.Instruktori.Add(instruktor);
                }
            }
        }

        public void SaveUsers(string filename)
        {
            using (StreamWriter file = new StreamWriter(@"../../Resources/" + filename))
            {
                foreach (Instruktor instruktor in Podaci.Instanca.Instruktori)
                {
                    file.WriteLine(instruktor.InstruktorZaUpisUFajl());
                }
            }
        }
    }
}

