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
    /// Interaction logic for ProfilPolaznikWindow.xaml
    /// </summary>
    public partial class ProfilPolaznikWindow : Window
    {
        public ProfilPolaznikWindow()
        {
            InitializeComponent();
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
    }
}
