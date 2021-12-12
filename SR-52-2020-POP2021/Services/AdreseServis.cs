using SR_52_2020_POP2021.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Services
{
    public class AdreseServis :AzuriranjeBaze<Adresa>, IAzuriranjaFajlova<Adresa>
    {

        public override ObservableCollection<Adresa> citanjeBaze()
        {
            ObservableCollection<Adresa> lst = new ObservableCollection<Adresa>();

            SqlConnection conn = new SqlConnection(AzuriranjeBaze<Adresa>.ucitajConnectionString()); //uzima connection string na lokalnu bazu u folderu Baza
            conn.Open();

            SqlCommand sqlCmd = new SqlCommand("select * from Adresa", conn);//selekcija svih adresa
            SqlDataReader sqlDataReader = sqlCmd.ExecuteReader();//data reader sadrzi listu svih adresa ucitanih iz tabele baze

            while (sqlDataReader.Read())//za svaki ucitan red preuzeti pojedinacne vrednosti kolona
            {
                int id = Convert.ToInt32(sqlDataReader.GetValue(0));  //konverzija u int
                string ulica = sqlDataReader.GetValue(1).ToString();
                string broj = sqlDataReader.GetValue(2).ToString();
                string grad = sqlDataReader.GetValue(3).ToString();
                string drzava = sqlDataReader.GetValue(4).ToString();
                bool obrisano = Convert.ToBoolean(sqlDataReader.GetValue(5));

                Adresa adresa = new Adresa(id, ulica, broj, grad, drzava);//napravi objekat od preuzetih 
                adresa.obrisano = obrisano;//setuje obrisano
                lst.Add(adresa);//doda u listu
            }

            sqlDataReader.Close();
            sqlCmd.Dispose();
            conn.Close();  //oslobadja resurse zauzete otvaranjem konekcije

            return lst;
        }

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
