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

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (status == EStatus.DODAJ)
            {
                this.polaznik.Korisnik.Adresa.Id = Podaci.Instanca.lstAdrese.Max(adr => adr.Id) + 1;//generise novi id adrese

                Podaci.Instanca.lstPolaznici.Add(polaznik);
                PolazniciServis polazniciServis = new PolazniciServis();
                polazniciServis.upisFajla(Podaci.Instanca.lstPolaznici);
            }

            DialogResult = true;
        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
