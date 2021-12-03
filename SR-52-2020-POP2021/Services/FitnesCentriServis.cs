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
    public class FitnesCentriServis : IAzuriranjaFajlova<FitnesCentar>
    {
        public ObservableCollection<FitnesCentar> citanjeFajla()
        {
            ObservableCollection<FitnesCentar> lst = new ObservableCollection<FitnesCentar>();

            string[] redovi = File.ReadAllLines(@"..\..\Resourses\fitnescentri.txt");
            foreach (string red in redovi)
            {
                string[] podaciReda = red.Split(';');

                int id= int.Parse(podaciReda[0]);
                string naziv = podaciReda[1];

                int idAdresa= int.Parse(podaciReda[2]);
                AdreseServis adrServis= new AdreseServis();
                Adresa adr = adrServis.citanjeFajla().Where(ad => ad.Id == idAdresa).FirstOrDefault();
                Adresa adresa = null;
                if (adr != null)
                    adresa = new Adresa(adr);

                bool obrisano = Convert.ToBoolean(podaciReda[3]);


                FitnesCentar fc = new FitnesCentar(id, naziv, adresa);
                fc.obrisano = obrisano;
                lst.Add(fc);
            }

            return lst;
        }

        public void upisFajla(ObservableCollection<FitnesCentar> lista)
        {
            throw new NotImplementedException();
        }
    }
}
