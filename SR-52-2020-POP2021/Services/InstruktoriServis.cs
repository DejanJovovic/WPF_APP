using SR_52_2020_POP2021.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SR_52_2020_POP2021.Services
{
    public class InstruktoriServis : AzuriranjeBaze<Instruktor>, IAzuriranjaFajlova<Instruktor>
    {

        public override ObservableCollection<Instruktor> citanjeBaze()
        {
            ObservableCollection<Instruktor> lst = new ObservableCollection<Instruktor>();

            SqlConnection conn = new SqlConnection(AzuriranjeBaze<Instruktor>.ucitajConnectionString()); //uzima connection string na lokalnu bazu u folderu Baza
            conn.Open();

            SqlCommand sqlCmd = new SqlCommand("select * from Korisnik", conn);//selekcija svih korisnika
            SqlDataReader sqlDataReader = sqlCmd.ExecuteReader();//data reader sadrzi listu svih korisnika ucitanih iz tabele baze

            while (sqlDataReader.Read())//za svaki ucitan red preuzeti pojedinacne vrednosti kolona
            {

                ETipKorisnika tipKorisnika = (ETipKorisnika)Enum.Parse(typeof(ETipKorisnika), sqlDataReader.GetValue(7).ToString());
                if (tipKorisnika == ETipKorisnika.INSTRUKTOR)
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
                    int idFitnesCentar = int.Parse(sqlDataReader.GetValue(9).ToString());


                    Korisnik k = new Korisnik(ime, prezime, jmbg, pol, adresa, email, lozinka, tipKorisnika);//napravljen objekat korisnika
                    Instruktor instr = new Instruktor(k, idFitnesCentar);
                    instr.obrisano = obrisano;

                    lst.Add(instr);//dodat u listu

                }



            }

            sqlDataReader.Close();
            sqlCmd.Dispose();
            conn.Close();  //oslobadja resurse zauzete otvaranjem konekcije

            return lst;
        }


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
            StreamWriter sw = new StreamWriter(@"..\..\Resourses\instruktori.txt");
            foreach (Instruktor instr in Podaci.Instanca.lstInstruktori)
            {


                sw.WriteLine(instr.Korisnik.Ime + ";" + instr.Korisnik.Prezime + ";" + instr.Korisnik.Jmbg + ";" + instr.Korisnik.Pol + ";" +
                    instr.Korisnik.Adresa.Id + ";" + instr.Korisnik.Email + ";" + instr.Korisnik.Lozinka + ";" + instr.Korisnik.TipKorisnika + ";" +
                    instr.obrisano + ";" + instr.IdFitnesCentra);
            }

            sw.Close();
        }
        // public Instruktor(Korisnik korisnik, int idFitnesCentra)
        //public Korisnik(string ime, string prezime, string jmbg, EPol pol, Adresa adresa, string email, string lozinka, ETipKorisnika tipKorisnika)
        // public Adresa(int id, string ulica, string broj, string grad, string drzava)
    }
}
