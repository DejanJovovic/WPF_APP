using SR_52_2020_POP2021.Model;
using SR_52_2020_POP2021.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ProfilAdminWindow.xaml
    /// </summary>
    public partial class ProfilAdminWindow : Window
    {
        ICollectionView viewKorisnici;
        public ProfilAdminWindow()
        {
            InitializeComponent();

            cbTipKorisnika.ItemsSource = Enum.GetValues(typeof(ETipKorisnika)).Cast<ETipKorisnika>();
            cbTipKorisnika.SelectedIndex = 1;
        }

        private void btnUpis_Click(object sender, RoutedEventArgs e)
        {
            if (cbTipKorisnika.SelectedIndex == 0)//admini
            {
                Korisnik admin = new Korisnik();

                RegistracijaAdminaWindow ra = new RegistracijaAdminaWindow(admin);
                ra.ShowDialog();
                if (ra.DialogResult == true)
                {
                    osveziDGKorisnici();
                }
            }
            else if (cbTipKorisnika.SelectedIndex == 1)//polaznici
            {
                Polaznik polaznik = new Polaznik();

                RegistracijaPolaznikaWindow rp = new RegistracijaPolaznikaWindow(polaznik);
                rp.ShowDialog();
                if (rp.DialogResult == true)
                {
                    osveziDGKorisnici();
                }
            }
        }

        private void btnIzmena_Click(object sender, RoutedEventArgs e)
        {
            if (cbTipKorisnika.SelectedIndex == 0)//admini
            {

                Korisnik selektovanAdmin = viewKorisnici.CurrentItem as Korisnik;
                if (selektovanAdmin != null)
                {
                    Korisnik copyAdmin = new Korisnik(selektovanAdmin);
                    

                    RegistracijaAdminaWindow ra = new RegistracijaAdminaWindow(selektovanAdmin, EStatus.IZMENI);
                    ra.ShowDialog();

                    if (ra.DialogResult != true)
                    {
                        
                        int index = Podaci.Instanca.lstAdmini.IndexOf(selektovanAdmin);
                        Podaci.Instanca.lstAdmini[index] = copyAdmin;
                    }

                    if (ra.DialogResult == true)
                    {
                        if (copyAdmin.Jmbg == Podaci.Instanca.jmbgPrijavljen)//ako je admin promenio svoj jmbg azurirati jmbg prijavljenog
                        {
                            Podaci.Instanca.jmbgPrijavljen = selektovanAdmin.Jmbg;
                        }
                    }

                }

                osveziDGKorisnici();
  
            }
            else if (cbTipKorisnika.SelectedIndex == 1)//polaznici
            {

                Polaznik selektovanPolaznik = viewKorisnici.CurrentItem as Polaznik;
                if (selektovanPolaznik != null)
                {
                    Polaznik copyPolaznik = new Polaznik(selektovanPolaznik);


                    RegistracijaPolaznikaWindow rp = new RegistracijaPolaznikaWindow(selektovanPolaznik, EStatus.IZMENI);
                    rp.ShowDialog();

                    if (rp.DialogResult != true)
                    {

                        int index = Podaci.Instanca.lstPolaznici.IndexOf(selektovanPolaznik);
                        Podaci.Instanca.lstPolaznici[index] = copyPolaznik;
                    }

                    

                }

                osveziDGKorisnici();

            }


        }

        private void btnBrisanje_Click(object sender, RoutedEventArgs e)
        {
            if (cbTipKorisnika.SelectedIndex == 0)//admini
            {
                Korisnik selektovanAdmin = viewKorisnici.CurrentItem as Korisnik;
                PotvrdaBrisanjaWindow pb = new PotvrdaBrisanjaWindow();
                pb.ShowDialog();
                if (pb.DialogResult == true)
                {
                    selektovanAdmin.obrisano = true;//logicko brisanje, prikazace se oni kojima je obrisano false
                    AdminiServis admServis = new AdminiServis();
                    admServis.upisFajla(Podaci.Instanca.lstAdmini);//azuriraj fajl

                    osveziDGKorisnici();
                }
            }else if (cbTipKorisnika.SelectedIndex == 1)//polaznici
            {
                Polaznik selektovanPolaznik = viewKorisnici.CurrentItem as Polaznik;
                PotvrdaBrisanjaWindow pb = new PotvrdaBrisanjaWindow();
                pb.ShowDialog();
                if (pb.DialogResult == true)
                {
                    selektovanPolaznik.obrisano = true;
                    PolazniciServis polazServis = new PolazniciServis();
                    polazServis.upisFajla(Podaci.Instanca.lstPolaznici);//azuriraj fajl

                    osveziDGKorisnici();
                }
            }
        }

        private void cbTipKorisnika_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            osveziDGKorisnici();
        }
        void osveziDGKorisnici()
        {

            if (cbTipKorisnika.SelectedIndex == 0)//admini
                viewKorisnici = CollectionViewSource.GetDefaultView(Podaci.Instanca.lstAdmini.Where(ins => ins.obrisano == false).ToList());//log brisanje, neobrisani svi
            else if (cbTipKorisnika.SelectedIndex == 1)//polaznici
                viewKorisnici = CollectionViewSource.GetDefaultView(Podaci.Instanca.lstPolaznici.Where(ins => ins.obrisano == false).ToList());
            else if (cbTipKorisnika.SelectedIndex == 2)//instruktori
                viewKorisnici = CollectionViewSource.GetDefaultView(Podaci.Instanca.lstInstruktori.Where(ins => ins.obrisano == false).ToList());


            dgKorisnici.ItemsSource = viewKorisnici;
            dgKorisnici.IsSynchronizedWithCurrentItem = true;
            dgKorisnici.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);


        }

        private void dgKorisnici_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (
                (string)e.Column.Header == "Ime" ||
                (string)e.Column.Header == "Prezime" ||
                //(string)e.Column.Header == "Jmbg" ||
                (string)e.Column.Header == "Pol" ||
                (string)e.Column.Header == "Adresa" ||
                (string)e.Column.Header == "Email" ||
                (string)e.Column.Header == "Lozinka" ||
                (string)e.Column.Header == "TipKorisnika" ||
                (string)e.Column.Header == "Korisnik" 
                )
            {
                e.Cancel = true;
            }

        }

       
    }
}
