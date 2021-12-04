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
    /// Interaction logic for RegistracijaAdminaWindow.xaml
    /// </summary>
    public partial class RegistracijaAdminaWindow : Window
    {
        Korisnik admin = null;
        EStatus status;
        public RegistracijaAdminaWindow(Korisnik admin, EStatus status = EStatus.DODAJ)
        {
            InitializeComponent();
            dodajCombo();
            this.admin = admin;
            this.status = status;
            if (status == EStatus.IZMENI)
            {
                btnDodaj.Content = "Izmeni";
                this.Title = "Izmena podataka o adminu";
            }

            tbIme.DataContext = admin;
            tbPrezime.DataContext = admin;
            tbJmbg.DataContext = admin;
            cbPol.DataContext = admin;
            tbEmail.DataContext = admin;
            tbLozinka.DataContext = admin;

            tbUlica.DataContext = admin.Adresa;
            tbBroj.DataContext = admin.Adresa;
            tbGrad.DataContext = admin.Adresa;
            cbDrzava.DataContext = admin.Adresa;
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
                this.admin.TipKorisnika = ETipKorisnika.ADMINISTRATOR;
                this.admin.Adresa.Id = Podaci.Instanca.lstAdrese.Max(adr => adr.Id) + 1;//generise novi id adrese
                this.admin.ImePrezime = this.admin.Ime + " " + this.admin.Prezime;

                Podaci.Instanca.lstAdmini.Add(admin);
                Podaci.Instanca.lstAdrese.Add(admin.Adresa);

                AdminiServis adminiServis = new AdminiServis();
                adminiServis.upisFajla(Podaci.Instanca.lstAdmini);
                AdreseServis adrServis = new AdreseServis();
                adrServis.upisFajla(Podaci.Instanca.lstAdrese);

            }
            else if (status == EStatus.IZMENI)
            {
                //if(Podaci.Instanca.tipPrijavljen==ETipKorisnika.ADMINISTRATOR)//ako je prijavljen admin i prikazuje se njegov profil
                //    Podaci.Instanca.jmbgPrijavljen = admin.Jmbg; //ukoliko je promenjen jmbg promeniti ga u klasi Podaci za prijavljenog admina

                this.admin.ImePrezime = this.admin.Ime + " " + this.admin.Prezime;

                Adresa a = Podaci.Instanca.lstAdrese.Where(adr => adr.Id == admin.Adresa.Id).FirstOrDefault();//prvo naci adresu iz liste adresa na osnovu id
                int indexAdrese = Podaci.Instanca.lstAdrese.IndexOf(a);
                Podaci.Instanca.lstAdrese[indexAdrese] = new Adresa(admin.Adresa);//na indeksu te adrese dodeliti modifikovanu adresu

                AdminiServis adminiServis = new AdminiServis();
                adminiServis.upisFajla(Podaci.Instanca.lstAdmini);
                AdreseServis adrServis = new AdreseServis();
                adrServis.upisFajla(Podaci.Instanca.lstAdrese);//overwrite podataka u fajlovima
            }

            DialogResult = true;
        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
