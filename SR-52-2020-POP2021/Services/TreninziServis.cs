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
    public class TreninziServis : IAzuriranjaFajlova<Trening>
    {
        public ObservableCollection<Trening> citanjeFajla()
        {
            ObservableCollection<Trening> lst = new ObservableCollection<Trening>();

            string[] redovi = File.ReadAllLines(@"..\..\Resourses\treninzi.txt");
            foreach (string red in redovi)
            {
                string[] podaciReda = red.Split(';');

                int id = int.Parse(podaciReda[0]);
                DateTime datumTreninga = Convert.ToDateTime(podaciReda[1]);
                string vremePocetka = podaciReda[2];
                int trajanjeTreninga = int.Parse(podaciReda[3]);
                bool slobodan = Convert.ToBoolean(podaciReda[4]);

                string jmbgInstruktor = podaciReda[5];
                Instruktor instruktor = null;
                InstruktoriServis instrServ = new InstruktoriServis();
                Instruktor instr=instrServ.citanjeFajla().Where(ins => ins.Korisnik.Jmbg == jmbgInstruktor).FirstOrDefault();
                if (instr != null)
                    instruktor = new Instruktor(instr);

                string jmbgPolaznik = podaciReda[6];
                Polaznik polaznik=null;
                if (jmbgPolaznik != "null")//trening nema dodeljenog polaznika ako jos nije rezervisan
                {
                    PolazniciServis polaznikServ = new PolazniciServis();
                    Polaznik polaz = polaznikServ.citanjeFajla().Where(pk => pk.Korisnik.Jmbg == jmbgPolaznik).FirstOrDefault();
                    if (polaz != null)
                        polaznik = new Polaznik(polaz);
                }


                bool obrisano = Convert.ToBoolean(podaciReda[7]);


                Trening t = new Trening(id, datumTreninga, vremePocetka, trajanjeTreninga, slobodan, instruktor, polaznik);
                if(polaznik!=null)
                    t.ImePrezimePolaznika = polaznik.Ime + " " + polaznik.Prezime;
                if(instruktor!=null)
                    t.ImePrezimeInstruktora = instruktor.Ime + " " + instruktor.Prezime;
                t.obrisano = obrisano;




                lst.Add(t);
            }

            return lst;
        }

        public void upisFajla(ObservableCollection<Trening> lista)
        {
            StreamWriter sw = new StreamWriter(@"..\..\Resourses\treninzi.txt");
            foreach(Trening t in Podaci.Instanca.lstTreninzi)
            {
                string idPolaznik = "null";//trening je inicijalno slobodan termin i polaznik mozda nije setovan
                if (t.Polaznik != null)
                    idPolaznik = t.Polaznik.Korisnik.Jmbg;

                sw.WriteLine(t.Id + ";" + t.DatumTreninga + ";" + t.VremePocetka + ";" + t.TrajanjeTreninga + ";" +
                    t.Slobodan + ";" + t.Instruktor.Korisnik.Jmbg + ";" + idPolaznik + ";" + t.obrisano);
            }

            sw.Close();

        }

        //  public Trening(int id, DateTime datumTreninga, string vremePocetka, int trajanjeTreninga, bool slobodan, Instruktor instruktor, Polaznik polaznik)  obrisano
    }
}
