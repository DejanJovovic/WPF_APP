using SR_52_2020_POP2021.Model;
using SR_52_2020_POP2021.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SR_52_2020_POP2021.Windows
{
    /// <summary>
    /// Interaction logic for RegistracijaPolaznikaWindow.xaml
    /// </summary>
    public partial class RegistracijaPolaznikaWindow : Window
    {
        Polaznik polaznik = null;
        EStatus status;
        public RegistracijaPolaznikaWindow(Polaznik polaznik, EStatus status=EStatus.DODAJ)
        {
            InitializeComponent();
            dodajCombo();
            this.polaznik = polaznik;
            this.status = status;
            if (status == EStatus.IZMENI)
            {
                btnDodaj.Content = "Izmeni";
                this.Title = "Izmena podataka o polazniku";
                tbJmbg.IsEnabled = false;
            }

            tbIme.DataContext = polaznik.Korisnik;
            tbPrezime.DataContext = polaznik.Korisnik;
            tbJmbg.DataContext = polaznik.Korisnik;
            cbPol.DataContext = polaznik.Korisnik;
            tbEmail.DataContext = polaznik.Korisnik;
            tbLozinka.DataContext = polaznik.Korisnik;

            tbUlica.DataContext = polaznik.Korisnik.Adresa;
            tbBroj.DataContext = polaznik.Korisnik.Adresa;
            tbGrad.DataContext = polaznik.Korisnik.Adresa;
            cbDrzava.DataContext = polaznik.Korisnik.Adresa;
        }

        void dodajCombo()
        {

            cbPol.Items.Add("M");
            cbPol.Items.Add("Z");
            cbPol.SelectedIndex = 0;

            cbDrzava.Items.Add("Srbija");
            cbDrzava.Items.Add("Bosna i Hercegovina");
            cbDrzava.Items.Add("Crna Gora");
            cbDrzava.Items.Add("Grcka");
            cbDrzava.SelectedIndex = 0;
        }

        bool validanUnos()
        {
            if (tbIme.Text == "" || tbPrezime.Text == "" || tbJmbg.Text == "" || tbEmail.Text == "" || tbLozinka.Text == "" || tbEmail.Text == "" ||
                tbUlica.Text == "" || tbBroj.Text == "" || tbGrad.Text == "")
            {
                MessageBox.Show("Niste uneli sve podatke!");
                return false;
            }
            if (!tbEmail.Text.Contains("@") || !tbEmail.Text.EndsWith(".com"))
            {
                MessageBox.Show("Unet email nije u ispravnom formatu!");
                return false;
            }

            if (
                this.status == EStatus.DODAJ &&
                (
                    Podaci.Instanca.lstAdmini.Any(a => a.Jmbg == tbJmbg.Text) ||
                    Podaci.Instanca.lstInstruktori.Any(ins => ins.Jmbg == tbJmbg.Text) ||
                    Podaci.Instanca.lstPolaznici.Any(p => p.Jmbg == tbJmbg.Text)
                )
               )
            {
                MessageBox.Show("Uneli ste postojeci jmbg!");
                return false;
            }

            return true;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (validanUnos())
            {
                if (status == EStatus.DODAJ)
                {
                    this.polaznik.Korisnik.TipKorisnika = ETipKorisnika.POLAZNIK;

                    int idAdrese = 1;
                    if (Podaci.Instanca.lstAdrese.Count > 0)
                        idAdrese = Podaci.Instanca.lstAdrese.Max(adr => adr.Id) + 1;//generise novi id adrese, max id adrese uvecan za 1. 
                    this.polaznik.Korisnik.Adresa.Id = idAdrese;

                    this.polaznik.ImePrezime = polaznik.Korisnik.Ime + " " + polaznik.Korisnik.Prezime;
                    this.polaznik.Ime = polaznik.Korisnik.Ime;
                    this.polaznik.Prezime= polaznik.Korisnik.Prezime;
                    this.polaznik.Jmbg = polaznik.Korisnik.Jmbg;

                    Podaci.Instanca.lstPolaznici.Add(polaznik);
                    Podaci.Instanca.lstAdrese.Add(polaznik.Korisnik.Adresa);

                    AzuriranjeBaze<Object>.insertUpdateDelete_Baza("insert into Adresa values(" +
                                                                      polaznik.Korisnik.Adresa.Id + ", '" +
                                                                      polaznik.Korisnik.Adresa.Ulica + "', '" +
                                                                      polaznik.Korisnik.Adresa.Broj + "', '" +
                                                                      polaznik.Korisnik.Adresa.Grad + "', '" +
                                                                      polaznik.Korisnik.Adresa.Drzava + "', '" +
                                                                      polaznik.Korisnik.Adresa.obrisano + "'" +
                                                                      ");");

                    AzuriranjeBaze<Object>.insertUpdateDelete_Baza("insert into Korisnik values('" +
                                                                        polaznik.Korisnik.Ime + "', '" +
                                                                        polaznik.Korisnik.Prezime + "', '" +
                                                                        polaznik.Korisnik.Jmbg + "', '" +
                                                                        polaznik.Korisnik.Pol + "', " +
                                                                        polaznik.Korisnik.Adresa.Id + ", '" +
                                                                        polaznik.Korisnik.Email + "', '" +
                                                                        polaznik.Korisnik.Lozinka + "', '" +
                                                                        polaznik.Korisnik.TipKorisnika + "', '" +
                                                                        polaznik.Korisnik.obrisano + "', null" +
                                                                        ");");

                    //PolazniciServis polazniciServis = new PolazniciServis();
                    //polazniciServis.upisFajla(Podaci.Instanca.lstPolaznici);
                    //AdreseServis adrServis = new AdreseServis();
                    //adrServis.upisFajla(Podaci.Instanca.lstAdrese);

                }
                else if (status == EStatus.IZMENI)
                {
                    this.polaznik.ImePrezime = polaznik.Korisnik.Ime + " " + polaznik.Korisnik.Prezime;
                    this.polaznik.Ime = polaznik.Korisnik.Ime;
                    this.polaznik.Prezime = polaznik.Korisnik.Prezime;
                    this.polaznik.Jmbg = polaznik.Korisnik.Jmbg;

                    if (Podaci.Instanca.tipPrijavljen == ETipKorisnika.POLAZNIK)//ako je polaznik promenio svoj jmbg
                        Podaci.Instanca.jmbgPrijavljen = polaznik.Korisnik.Jmbg; //ukoliko je promenjen jmbg promeniti ga u klasi Podaci za prijavljenog polaznika

                    Adresa a = Podaci.Instanca.lstAdrese.Where(adr => adr.Id == polaznik.Korisnik.Adresa.Id).FirstOrDefault();//prvo naci adresu iz liste adresa na osnovu id
                    int indexAdrese = Podaci.Instanca.lstAdrese.IndexOf(a);
                    Podaci.Instanca.lstAdrese[indexAdrese] = new Adresa(polaznik.Korisnik.Adresa);//na indeksu te adrese dodeliti modifikovanu adresu


                    AzuriranjeBaze<Object>.insertUpdateDelete_Baza("update Adresa set " +
                                                                     "ulica='" + polaznik.Korisnik.Adresa.Ulica + "', " +
                                                                     "broj='" + polaznik.Korisnik.Adresa.Broj + "', " +
                                                                     "grad='" + polaznik.Korisnik.Adresa.Grad + "', " +
                                                                     "drzava='" + polaznik.Korisnik.Adresa.Drzava + "' " +
                                                                     "where id=" + polaznik.Korisnik.Adresa.Id + ";");

                    AzuriranjeBaze<Object>.insertUpdateDelete_Baza("update Korisnik set " +
                                                                       "ime='" + polaznik.Korisnik.Ime + "', " +
                                                                       "prezime='" + polaznik.Korisnik.Prezime + "', " +
                                                                       "pol='" + polaznik.Korisnik.Pol + "', " +
                                                                       "email='" + polaznik.Korisnik.Email + "', " +
                                                                       "lozinka='" + polaznik.Korisnik.Lozinka + "' " +
                                                                       "where jmbg=" + polaznik.Korisnik.Jmbg + ";");

                    //PolazniciServis polazniciServis = new PolazniciServis();
                    //polazniciServis.upisFajla(Podaci.Instanca.lstPolaznici);
                    //AdreseServis adrServis = new AdreseServis();
                    //adrServis.upisFajla(Podaci.Instanca.lstAdrese);//overwrite podataka u fajlovima
                }

                DialogResult = true;
            }
        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
