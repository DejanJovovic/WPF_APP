using SR_52_2020_POP2021.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SR_52_2020_POP2021.Services
{
    public class InstruktoriServis : IAzuriranjaFajlova<Instruktor>
    {
        public ObservableCollection<Instruktor> citanjeFajla()
        {
            ObservableCollection<Instruktor> lst = new ObservableCollection<Instruktor>();

            string[] redovi = File.ReadAllLines(@"..\..\Resourses\instruktori.txt");
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
                int idFitnesCentra = int.Parse(podaciReda[9]);


                Korisnik k = new Korisnik(ime, prezime, jmbg, pol, adresa, email, lozinka, tipKorisnika);
                Instruktor instr = new Instruktor(k, idFitnesCentra);
                instr.obrisano = obrisano;

                lst.Add(instr);
            }

            return lst;
        }

        public void upisFajla(ObservableCollection<Instruktor> lista)
        {
            throw new NotImplementedException();
        }
    }
}
