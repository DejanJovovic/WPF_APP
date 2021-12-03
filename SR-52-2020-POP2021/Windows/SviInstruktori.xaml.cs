using SR_52_2020_POP2021.Model;
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
    /// Interaction logic for SviInstruktori.xaml
    /// </summary>
    public partial class SviInstruktori : Window
    {
        ICollectionView view;
        public SviInstruktori()
        {
            InitializeComponent();
            UpdateView();
            //view.Filter = CustomFilter;
        }

        //private bool CustomFilter(object obj)
        //{
        //    Korisnik korisnik = obj as Korisnik;
        //    Korisnik korisnik1 = (Korisnik)obj;

        //    if (korisnik.TipKorisnika.Equals(ETipKorisnika.INSTRUKTOR) && korisnik.Aktivan)
        //    {
        //        if (txtPretraga.Text != "")
        //        {
        //            return korisnik.Ime.Contains(txtPretraga.Text);
        //        }
        //        else
        //            return true;
        //    }
        //    return false;
        //}

        private void UpdateView()
        {
            //DGInstruktori.ItemsSource = null;
            //view = CollectionViewSource.GetDefaultView(Podaci.Instanca.Korisnici);
            //DGInstruktori.ItemsSource = view;
            //DGInstruktori.IsSynchronizedWithCurrentItem = true;

            //DGInstruktori.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DodavanjeInstruktora_Click(object sender, RoutedEventArgs e)
        {
            //Korisnik noviKorisnik = new Korisnik();
            //DodajIzmeniInstruktore dodajIzmeniInstruktore = new DodajIzmeniInstruktore(noviKorisnik);
            //this.Hide();
            //if (!(bool)dodajIzmeniInstruktore.ShowDialog())
            //{

            //}
            //this.Show();
        }

        private void IzmenaInstruktora_Click(object sender, RoutedEventArgs e)
        {
            //Korisnik selectedInstruktor = view.CurrentItem as Korisnik;

            //Korisnik stariInstruktor = selectedInstruktor.Clone();

            //DodajIzmeniInstruktore dodajIzmeniInstruktore = new DodajIzmeniInstruktore(selectedInstruktor, EStatus.IZMENI);
            //this.Hide();
            //if (!(bool)dodajIzmeniInstruktore.ShowDialog())
            //{
            //    int index = Podaci.Instanca.Korisnici.ToList().FindIndex(k => k.Email.Equals(stariInstruktor.Email));
            //    Podaci.Instanca.Korisnici[index] = stariInstruktor;
            //}
            //this.Show();

            //view.Refresh();

        }

        private void BrisanjeInstruktora_Click(object sender, RoutedEventArgs e)
        {
            //Korisnik instruktorZaBrisanje = view.CurrentItem as Korisnik;
            //Podaci.Instanca.DeleteUser(instruktorZaBrisanje.Email);

            //int index = Podaci.Instanca.Korisnici.ToList().FindIndex(korisnik => korisnik.Email.Equals(instruktorZaBrisanje.Email));
            //Podaci.Instanca.Korisnici[index].Aktivan = false;


            //UpdateView();
            //view.Refresh();
        }

        private void DGInstruktori_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
            {
                // e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void txtPretraga_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }
    }
}
