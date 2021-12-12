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
    public class FitnesCentriServis : AzuriranjeBaze<FitnesCentar>, IAzuriranjaFajlova<FitnesCentar>
    {

        public override ObservableCollection<FitnesCentar> citanjeBaze()
        {
            ObservableCollection<FitnesCentar> lst = new ObservableCollection<FitnesCentar>();

            SqlConnection conn = new SqlConnection(AzuriranjeBaze<FitnesCentar>.ucitajConnectionString()); //uzima connection string na lokalnu bazu u folderu Baza
            conn.Open();

            SqlCommand sqlCmd = new SqlCommand("select * from FitnesCentar", conn);//selekcija svih fitnes centara
            SqlDataReader sqlDataReader = sqlCmd.ExecuteReader();//data reader sadrzi listu svih fitnes centara ucitanih iz tabele baze

            while (sqlDataReader.Read())//za svaki ucitan red preuzeti pojedinacne vrednosti kolona
            {
                int id = Convert.ToInt32(sqlDataReader.GetValue(0));
                string naziv = sqlDataReader.GetValue(1).ToString();

                int idAdresa = Convert.ToInt32(sqlDataReader.GetValue(2));
                AdreseServis adrServis = new AdreseServis();
                Adresa adr = adrServis.citanjeBaze().Where(ad => ad.Id == idAdresa).FirstOrDefault();
                Adresa adresa = null;
                if (adr != null)
                    adresa = new Adresa(adr);


                bool obrisano = Convert.ToBoolean(sqlDataReader.GetValue(3));

                FitnesCentar fc = new FitnesCentar(id, naziv, adresa);
                fc.obrisano = obrisano;
                lst.Add(fc);

            }

            sqlDataReader.Close();
            sqlCmd.Dispose();
            conn.Close();  //oslobadja resurse zauzete otvaranjem konekcije

            return lst;
        }


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
            StreamWriter sw = new StreamWriter(@"..\..\Resourses\fitnescentri.txt");
            foreach (FitnesCentar fc in Podaci.Instanca.lstFitnesCentri)
            {


                sw.WriteLine(fc.Id + ";" + fc.Naziv + ";" + fc.Adresa.Id + ";" + fc.obrisano);
            }

            sw.Close();
        }

        //    public FitnesCentar(int id, string naziv, Adresa adresa)
    }
}
