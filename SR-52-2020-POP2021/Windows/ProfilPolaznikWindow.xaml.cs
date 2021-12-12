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
    /// Interaction logic for ProfilPolaznikWindow.xaml
    /// </summary>
    public partial class ProfilPolaznikWindow : Window
    {
        public ProfilPolaznikWindow()
        {
            InitializeComponent();

            dtDatumPrikaz.SelectedDate = DateTime.Now;
            osveziPrikazDataGrid();
           
        }

        void osveziPrikazDataGrid()
        {
            dgTreninzi.ItemsSource = Podaci.Instanca.lstTreninzi.Where(
                   t => t.obrisano == false &&
                   t.Polaznik != null &&
                   t.Polaznik.Jmbg == Podaci.Instanca.jmbgPrijavljen &&  //prikaze termine treninga prijavljenog polaznika
                   t.DatumTreninga.Date.Day == dtDatumPrikaz.SelectedDate.Value.Date.Day &&
                   t.DatumTreninga.Date.Month == dtDatumPrikaz.SelectedDate.Value.Date.Month &&
                   t.DatumTreninga.Date.Year == dtDatumPrikaz.SelectedDate.Value.Date.Year
               ).OrderBy(t => int.Parse(t.VremePocetka.Split(':')[0])).ThenBy(t => int.Parse(t.VremePocetka.Split(':')[1])).ToList();
        }

        private void btnPodaciProfila_Click(object sender, RoutedEventArgs e)
        {
            Polaznik polaznik = Podaci.Instanca.lstPolaznici.Where(p => p.Korisnik.Jmbg == Podaci.Instanca.jmbgPrijavljen).FirstOrDefault();//naci objekat polaznika koji je prijavljen
            if (polaznik != null)
            {

                Polaznik copyPolaznik = new Polaznik(polaznik);
                //Adresa copyAdresa = copyPolaznik.Korisnik.Adresa;

                RegistracijaPolaznikaWindow rp = new RegistracijaPolaznikaWindow(polaznik, EStatus.IZMENI);
                rp.ShowDialog();

                if (rp.DialogResult != true)//ako nije potvrdjena izmena a zbog bindinga neki podaci promenjeni u objektu vratiti na kopiju objekta pre otvaranja forme
                {
                    int index = Podaci.Instanca.lstPolaznici.IndexOf(polaznik);
                    Podaci.Instanca.lstPolaznici[index] = copyPolaznik;
                }
            }

           
        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgTreninzi_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (
               (string)e.Column.Header == "Id" ||
               (string)e.Column.Header == "DatumTreninga" ||
               (string)e.Column.Header == "Slobodan" ||
               (string)e.Column.Header == "Polaznik" ||
               (string)e.Column.Header == "Instruktor") //da ne prikaze ove podatke u kolonama data grida
            {
                e.Cancel = true;
            }
        }

        private void dtDatumPrikaz_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            osveziPrikazDataGrid();
        }

        private void btnOtkaziSelektovan_Click(object sender, RoutedEventArgs e)
        {
            if (dgTreninzi.SelectedIndex > -1)
            {
                Trening treningSelektovan = (Trening)dgTreninzi.SelectedItem;
                Trening treningOtkazi = Podaci.Instanca.lstTreninzi.Where(t => t.Id == treningSelektovan.Id).FirstOrDefault();
                if (treningOtkazi != null)
                {
                    treningOtkazi.Polaznik = null;
                    treningOtkazi.ImePrezimePolaznika = "";

                    TreninziServis ts = new TreninziServis();
                    ts.upisFajla(Podaci.Instanca.lstTreninzi);
                    osveziPrikazDataGrid();
                }
            }
        }
    }
}
