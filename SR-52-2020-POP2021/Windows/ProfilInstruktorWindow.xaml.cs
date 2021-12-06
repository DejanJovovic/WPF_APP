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
    /// Interaction logic for ProfilInstruktorWindow.xaml
    /// </summary>
    public partial class ProfilInstruktorWindow : Window
    {
        Instruktor selektovanInstruktor = null;// ako se prijavio polaznik, ima opciju na pocetnoj strani, kada selektuje instruktora da otvori ovu formu i zakaze termin treninga
        public ProfilInstruktorWindow(Instruktor selektovanInstruktor) //ova forma je dostupna i polaznicima kada zakazuju termin treninga zato sto ujedno su prikazani i podaci po datumima za odabranog instruktora
        {
            InitializeComponent();
            this.selektovanInstruktor = selektovanInstruktor;

            if (selektovanInstruktor != null)//formu je otvorio polaznik da zakaze termin i prosledjen je selektovani instruktor
            {
                this.Title = "Zakazivanje termina treninga";
                lbInstruktorPodaci.Content = "Instruktor: " + selektovanInstruktor.Korisnik.Ime + " " + selektovanInstruktor.Korisnik.Prezime;
                lbInstruktorPodaci.Visibility = Visibility.Visible;
            }
            else
            {
                lbInstruktorPodaci.Visibility = Visibility.Hidden;

            }

            if (Podaci.Instanca.tipPrijavljen == ETipKorisnika.INSTRUKTOR) //samo ako je instruktor prijavljen mogu se prikazati svi detalji njegovog profila
                btnPodaciProfila.Visibility = Visibility.Visible;
            else
                btnPodaciProfila.Visibility = Visibility.Hidden;


            dtDatumPrikaz.SelectedDate = DateTime.Now;
        }

        private void btnPodaciProfila_Click(object sender, RoutedEventArgs e)
        {
            Instruktor instruktor = Podaci.Instanca.lstInstruktori.Where(ins => ins.Korisnik.Jmbg == Podaci.Instanca.jmbgPrijavljen).FirstOrDefault();//naci objekat instruktora koji je prijavljen
            if (instruktor != null)
            {

                Instruktor copyInstruktor = new Instruktor(instruktor);

                RegistracijaInstruktoraWindow ri = new RegistracijaInstruktoraWindow(instruktor, EStatus.IZMENI);
                ri.ShowDialog();

                if (ri.DialogResult != true)//ako nije potvrdjena izmena a zbog bindinga neki podaci promenjeni u objektu vratiti na kopiju objekta pre otvaranja forme
                {
                    int index = Podaci.Instanca.lstInstruktori.IndexOf(instruktor);
                    Podaci.Instanca.lstInstruktori[index] = copyInstruktor;
                }
            }
        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
