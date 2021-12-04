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
        public ProfilInstruktorWindow()
        {
            InitializeComponent();
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
    }
}
