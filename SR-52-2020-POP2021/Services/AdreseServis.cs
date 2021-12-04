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
    public class AdreseServis : IAzuriranjaFajlova<Adresa>
    {
        public ObservableCollection<Adresa> citanjeFajla()
        {
            
            ObservableCollection<Adresa> lst = new ObservableCollection<Adresa>();

            string[] redovi = File.ReadAllLines(@"..\..\Resourses\adrese.txt");
            foreach (string red in redovi)
            {
                string[] podaciReda = red.Split(';');
                int id = int.Parse(podaciReda[0]);
                string ulica = podaciReda[1];
                string broj = podaciReda[2];
                string grad = podaciReda[3];
                string drzava = podaciReda[4];
                bool obrisano = Convert.ToBoolean(podaciReda[5]);

                Adresa a = new Adresa(id, ulica, broj, grad, drzava);
                a.obrisano = obrisano;
                lst.Add(a);
            }

            return lst;
        }

        public void upisFajla(ObservableCollection<Adresa> lista)
        {
            StreamWriter sw = new StreamWriter(@"..\..\Resourses\adrese.txt");
            foreach (Adresa adresa in Podaci.Instanca.lstAdrese)
            {


                sw.WriteLine(adresa.Id + ";" + adresa.Ulica + ";" + adresa.Broj + ";" + adresa.Grad + ";" +
                    adresa.Drzava + ";" + adresa.obrisano);
            }

            sw.Close();
        }
        // public Adresa(int id, string ulica, string broj, string grad, string drzava)
    }
}
