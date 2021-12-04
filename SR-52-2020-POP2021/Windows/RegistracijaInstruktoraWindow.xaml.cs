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
                cbIdFitnesCentra.Items.Add(fc.Id);

            if(cbIdFitnesCentra.Items.Count>0)
                cbIdFitnesCentra.SelectedIndex = 0;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (status == EStatus.DODAJ)
            {
                this.instruktor.Korisnik.TipKorisnika = ETipKorisnika.INSTRUKTOR;
                this.instruktor.Korisnik.Adresa.Id = Podaci.Instanca.lstAdrese.Max(adr => adr.Id) + 1;//generise novi id adrese
                this.instruktor.ImePrezime = instruktor.Korisnik.Ime + " " + instruktor.Korisnik.Prezime;
                this.instruktor.Jmbg = instruktor.Korisnik.Jmbg;
                if (cbIdFitnesCentra.SelectedIndex > -1)
                    this.instruktor.IdFitnesCentra = int.Parse(cbIdFitnesCentra.SelectedItem.ToString());

                Podaci.Instanca.lstInstruktori.Add(instruktor);
                Podaci.Instanca.lstAdrese.Add(instruktor.Korisnik.Adresa);

                InstruktoriServis instrServis = new InstruktoriServis();
                instrServis.upisFajla(Podaci.Instanca.lstInstruktori);
                AdreseServis adrServis = new AdreseServis();
                adrServis.upisFajla(Podaci.Instanca.lstAdrese);

            }
            else if (status == EStatus.IZMENI)
            {
                this.instruktor.ImePrezime = instruktor.Korisnik.Ime + " " + instruktor.Korisnik.Prezime;
                this.instruktor.Jmbg = instruktor.Korisnik.Jmbg;
                if (cbIdFitnesCentra.SelectedIndex > -1)
                    this.instruktor.IdFitnesCentra = int.Parse(cbIdFitnesCentra.SelectedItem.ToString());

                if (Podaci.Instanca.tipPrijavljen == ETipKorisnika.INSTRUKTOR)//ako je instruktor promenio svoj jmbg
                    Podaci.Instanca.jmbgPrijavljen = instruktor.Korisnik.Jmbg; //ukoliko je promenjen jmbg promeniti ga u klasi Podaci za prijavljenog instruktora

                Adresa a = Podaci.Instanca.lstAdrese.Where(adr => adr.Id == instruktor.Korisnik.Adresa.Id).FirstOrDefault();//prvo naci adresu iz liste adresa na osnovu id
                int indexAdrese = Podaci.Instanca.lstAdrese.IndexOf(a);
                Podaci.Instanca.lstAdrese[indexAdrese] = new Adresa(instruktor.Korisnik.Adresa);//na indeksu te adrese dodeliti modifikovanu adresu

                InstruktoriServis instrServis = new InstruktoriServis();
                instrServis.upisFajla(Podaci.Instanca.lstInstruktori);
                AdreseServis adrServis = new AdreseServis();
                adrServis.upisFajla(Podaci.Instanca.lstAdrese);//overwrite podataka u fajlovima
            }

            DialogResult = true;
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
