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
    /// Interaction logic for RegistracijaInstruktoraWindow.xaml
    /// </summary>
    public partial class RegistracijaInstruktoraWindow : Window
    {
        Instruktor instruktor = null;
        EStatus status;
        public RegistracijaInstruktoraWindow(Instruktor instruktor, EStatus status = EStatus.DODAJ)
        {
            InitializeComponent();
            dodajCombo();
            this.instruktor = instruktor;
            this.status = status;
            if (status == EStatus.IZMENI)
            {
                btnDodaj.Content = "Izmeni";
                this.Title = "Izmena podataka o instruktoru";
                cbIdFitnesCentra.Text = instruktor.IdFitnesCentra.ToString();

                tbJmbg.IsEnabled = false;

                //cbIdFitnesCentra.Text = instruktor.IdFitnesCentra.ToString();
            }

            tbIme.DataContext = instruktor.Korisnik;
            tbPrezime.DataContext = instruktor.Korisnik;
            tbJmbg.DataContext = instruktor.Korisnik;
            cbPol.DataContext = instruktor.Korisnik;
            tbEmail.DataContext = instruktor.Korisnik;
            tbLozinka.DataContext = instruktor.Korisnik;

            tbUlica.DataContext = instruktor.Korisnik.Adresa;
            tbBroj.DataContext = instruktor.Korisnik.Adresa;
            tbGrad.DataContext = instruktor.Korisnik.Adresa;
            cbDrzava.DataContext = instruktor.Korisnik.Adresa;

            //cbIdFitnesCentra.DataContext = instruktor.IdFitnesCentra;
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


            foreach (FitnesCentar fc in Podaci.Instanca.lstFitnesCentri)
                if(fc.obrisano==false)
                    cbIdFitnesCentra.Items.Add(fc.Id);

            if(cbIdFitnesCentra.Items.Count>0)
                cbIdFitnesCentra.SelectedIndex = 0;
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
                    this.instruktor.Korisnik.TipKorisnika = ETipKorisnika.INSTRUKTOR;

                    int idAdrese = 1;
                    if (Podaci.Instanca.lstAdrese.Count > 0)
                        idAdrese = Podaci.Instanca.lstAdrese.Max(adr => adr.Id) + 1;//generise novi id adrese, max id adrese uvecan za 1. 
                    this.instruktor.Korisnik.Adresa.Id = idAdrese;

                    this.instruktor.ImePrezime = instruktor.Korisnik.Ime + " " + instruktor.Korisnik.Prezime;
                    this.instruktor.Ime = instruktor.Korisnik.Ime;
                    this.instruktor.Prezime = instruktor.Korisnik.Prezime;

                    this.instruktor.Jmbg = instruktor.Korisnik.Jmbg;
                    if (cbIdFitnesCentra.SelectedIndex > -1)
                        this.instruktor.IdFitnesCentra = int.Parse(cbIdFitnesCentra.SelectedItem.ToString());

                    Podaci.Instanca.lstInstruktori.Add(instruktor);
                    Podaci.Instanca.lstAdrese.Add(instruktor.Korisnik.Adresa);


                    AzuriranjeBaze<Object>.insertUpdateDelete_Baza("insert into Adresa values(" +
                                                                      instruktor.Korisnik.Adresa.Id + ", '" +
                                                                      instruktor.Korisnik.Adresa.Ulica + "', '" +
                                                                      instruktor.Korisnik.Adresa.Broj + "', '" +
                                                                      instruktor.Korisnik.Adresa.Grad + "', '" +
                                                                      instruktor.Korisnik.Adresa.Drzava + "', '" +
                                                                      instruktor.Korisnik.Adresa.obrisano + "'" +
                                                                      ");");

                    AzuriranjeBaze<Object>.insertUpdateDelete_Baza("insert into Korisnik values('" +
                                                                        instruktor.Korisnik.Ime + "', '" +
                                                                        instruktor.Korisnik.Prezime + "', '" +
                                                                        instruktor.Korisnik.Jmbg + "', '" +
                                                                        instruktor.Korisnik.Pol + "', " +
                                                                        instruktor.Korisnik.Adresa.Id + ", '" +
                                                                        instruktor.Korisnik.Email + "', '" +
                                                                        instruktor.Korisnik.Lozinka + "', '" +
                                                                        instruktor.Korisnik.TipKorisnika + "', '" +
                                                                        instruktor.Korisnik.obrisano + "', " +
                                                                        instruktor.IdFitnesCentra + 
                                                                        ");");
                    //InstruktoriServis instrServis = new InstruktoriServis();
                    //instrServis.upisFajla(Podaci.Instanca.lstInstruktori);
                    //AdreseServis adrServis = new AdreseServis();
                    //adrServis.upisFajla(Podaci.Instanca.lstAdrese);

                }
                else if (status == EStatus.IZMENI)
                {
                    this.instruktor.ImePrezime = instruktor.Korisnik.Ime + " " + instruktor.Korisnik.Prezime;
                    this.instruktor.Ime = instruktor.Korisnik.Ime;
                    this.instruktor.Prezime = instruktor.Korisnik.Prezime;
                    this.instruktor.Jmbg = instruktor.Korisnik.Jmbg;

                    if (cbIdFitnesCentra.SelectedIndex > -1)
                        this.instruktor.IdFitnesCentra = int.Parse(cbIdFitnesCentra.SelectedItem.ToString());

                    if (Podaci.Instanca.tipPrijavljen == ETipKorisnika.INSTRUKTOR)//ako je instruktor promenio svoj jmbg
                        Podaci.Instanca.jmbgPrijavljen = instruktor.Korisnik.Jmbg; //ukoliko je promenjen jmbg promeniti ga u klasi Podaci za prijavljenog instruktora

                    Adresa a = Podaci.Instanca.lstAdrese.Where(adr => adr.Id == instruktor.Korisnik.Adresa.Id).FirstOrDefault();//prvo naci adresu iz liste adresa na osnovu id
                    int indexAdrese = Podaci.Instanca.lstAdrese.IndexOf(a);
                    Podaci.Instanca.lstAdrese[indexAdrese] = new Adresa(instruktor.Korisnik.Adresa);//na indeksu te adrese dodeliti modifikovanu adresu


                    AzuriranjeBaze<Object>.insertUpdateDelete_Baza("update Adresa set " +
                                                                      "ulica='" + instruktor.Korisnik.Adresa.Ulica + "', " +
                                                                      "broj='" + instruktor.Korisnik.Adresa.Broj + "', " +
                                                                      "grad='" + instruktor.Korisnik.Adresa.Grad + "', " +
                                                                      "drzava='" + instruktor.Korisnik.Adresa.Drzava + "' " +
                                                                      "where id=" + instruktor.Korisnik.Adresa.Id + ";");

                    AzuriranjeBaze<Object>.insertUpdateDelete_Baza("update Korisnik set " +
                                                                       "ime='" + instruktor.Korisnik.Ime + "', " +
                                                                       "prezime='" + instruktor.Korisnik.Prezime + "', " +
                                                                       "pol='" + instruktor.Korisnik.Pol + "', " +
                                                                       "email='" + instruktor.Korisnik.Email + "', " +
                                                                       "lozinka='" + instruktor.Korisnik.Lozinka + "', " +
                                                                       "idFitnesCentar=" + instruktor.IdFitnesCentra + " " +
                                                                       "where jmbg=" + instruktor.Korisnik.Jmbg + ";");

                    //InstruktoriServis instrServis = new InstruktoriServis();
                    //instrServis.upisFajla(Podaci.Instanca.lstInstruktori);
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

        //promena selekcije po id fitnes centra prikazuje naziv fitnes centra u labeli ispod
        private void cbIdFitnesCentra_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbIdFitnesCentra.SelectedIndex > -1)
            {
                int idFc = int.Parse(cbIdFitnesCentra.SelectedItem.ToString());
                FitnesCentar fc = Podaci.Instanca.lstFitnesCentri.Where(f => f.Id == idFc).FirstOrDefault();
                if (fc != null)
                {
                    lbNazivFitnesCentra.Content = "Naziv: " + fc.Naziv;
                }
            }
        }
    }
}
