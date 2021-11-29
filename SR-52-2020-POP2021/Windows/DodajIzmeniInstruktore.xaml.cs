using SR_52_2020_POP2021.Model;
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
    /// Interaction logic for DodajIzmeniInstruktore.xaml
    /// </summary>
    public partial class DodajIzmeniInstruktore : Window
    {
        private EStatus odabraniStatus;
        private Korisnik odabraniInstruktor;

        public DodajIzmeniInstruktore(Korisnik instruktor, EStatus status = EStatus.DODAJ)
        {
            InitializeComponent();


            this.DataContext = instruktor;
            ComboTip.ItemsSource = Enum.GetValues(typeof(ETipKorisnika)).Cast<ETipKorisnika>();

            odabraniInstruktor = instruktor;
            odabraniStatus = status;

            if (status.Equals(EStatus.IZMENI) && instruktor != null)
            {
                this.Title = "Izmeni instruktora";

            }
            else
            {
                this.Title = "Dodaj Instruktora";
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

            if (odabraniStatus.Equals(EStatus.DODAJ))
            {
                odabraniInstruktor.Aktivan = true;
                Instruktor instruktor = new Instruktor
                {

                    Korisnik = odabraniInstruktor
                };
                Podaci.Instanca.Korisnici.Add(odabraniInstruktor);
                Podaci.Instanca.Instruktori.Add(instruktor);
            }

            Podaci.Instanca.SacuvajEntitet("korisnici.txt");
            Podaci.Instanca.SacuvajEntitet("instruktori.txt");

            this.DialogResult = true;
            this.Close();

        }
    }
}
