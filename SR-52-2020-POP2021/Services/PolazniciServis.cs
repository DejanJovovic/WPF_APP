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
    public class PolazniciServis : AzuriranjeBaze<Polaznik>, IAzuriranjaFajlova<Polaznik>
    {

        public override ObservableCollection<Polaznik> citanjeBaze()
        {
            ObservableCollection<Polaznik> lst = new ObservableCollection<Polaznik>();

            SqlConnection conn = new SqlConnection(AzuriranjeBaze<Polaznik>.ucitajConnectionString()); //uzima connection string na lokalnu bazu u folderu Baza
            conn.Open();

            SqlCommand sqlCmd = new SqlCommand("select * from Korisnik", conn);//selekcija svih korisnika
            SqlDataReader sqlDataReader = sqlCmd.ExecuteReader();//data reader sadrzi listu svih korisnika ucitanih iz tabele baze

            while (sqlDataReader.Read())//za svaki ucitan red preuzeti pojedinacne vrednosti kolona
            {

                ETipKorisnika tipKorisnika = (ETipKorisnika)Enum.Parse(typeof(ETipKorisnika), sqlDataReader.GetValue(7).ToString());
                if (tipKorisnika == ETipKorisnika.POLAZNIK)
                {

                    string ime = sqlDataReader.GetValue(0).ToString();
                    string prezime = sqlDataReader.GetValue(1).ToString();
                    string jmbg = sqlDataReader.GetValue(2).ToString();
                    EPol pol = (EPol)Enum.Parse(typeof(EPol), sqlDataReader.GetValue(3).ToString());

                    int idAdresa = int.Parse(sqlDataReader.GetValue(4).ToString());
                    AdreseServis adrServis = new AdreseServis();
                    Adresa adr = adrServis.citanjeBaze().Where(ad => ad.Id == idAdresa).FirstOrDefault();
                    Adresa adresa = null;
                    if (adr != null)
                        adresa = new Adresa(adr);

                    string email = sqlDataReader.GetValue(5).ToString();
                    string lozinka = sqlDataReader.GetValue(6).ToString();
                    //ETipKorisnika tipKorisnika = (ETipKorisnika)Enum.Parse(typeof(ETipKorisnika), sqlDataReader.GetValue(7).ToString());

                    bool obrisano = Convert.ToBoolean(sqlDataReader.GetValue(8).ToString());


                    Korisnik k = new Korisnik(ime, prezime, jmbg, pol, adresa, email, lozinka, tipKorisnika);//napravljen objekat korisnika
                    Polaznik p = new Polaznik(k);
                    p.obrisano = obrisano;

                    lst.Add(p);//dodat u listu

                }



            }

            sqlDataReader.Close();
            sqlCmd.Dispose();
            conn.Close();  //oslobadja resurse zauzete otvaranjem konekcije

            return lst;
        }



        public ObservableCollection<Polaznik> citanjeFajla()
        {
            ObservableCollection<Polaznik> lst = new ObservableCollection<Polaznik>();

            string[] redovi = File.ReadAllLines(@"..\..\Resourses\polaznici.txt");
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


                Korisnik k = new Korisnik(ime, prezime, jmbg, pol, adresa, email, lozinka, tipKorisnika);
                Polaznik p = new Polaznik(k);
                p.obrisano = obrisano;

                lst.Add(p);
            }

            return lst;
        }

        public void upisFajla(ObservableCollection<Polaznik> lista)
        {
            StreamWriter sw = new StreamWriter(@"..\..\Resourses\polaznici.txt");
            foreach (Polaznik polaznik in Podaci.Instanca.lstPolaznici)
            {


                sw.WriteLine(polaznik.Korisnik.Ime + ";" + polaznik.Korisnik.Prezime + ";" + polaznik.Korisnik.Jmbg + ";" + polaznik.Korisnik.Pol + ";" +
                    polaznik.Korisnik.Adresa.Id + ";" + polaznik.Korisnik.Email + ";" + polaznik.Korisnik.Lozinka + ";" + polaznik.Korisnik.TipKorisnika + ";" +
                    polaznik.obrisano);
            }

            sw.Close();
        }
        //public Polaznik(Korisnik korisnik)
    }
}
