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
    /// Interaction logic for ZakazivanjeTerminaWindow.xaml
    /// </summary>
    public partial class ZakazivanjeTerminaWindow : Window
    {
        public ZakazivanjeTerminaWindow()
        {
            InitializeComponent();
            inicijalizujCB_Polaznici();
        }

        void inicijalizujCB_Polaznici()
        {
            foreach(Polaznik p in Podaci.Instanca.lstPolaznici)
                cbPolaznici.Items.Add(p.Jmbg + "# " + p.Ime + " " + p.Prezime);
            
            if (cbPolaznici.Items.Count > 0)
                cbPolaznici.SelectedIndex = 0;
        }

        private void btnOdaberi_Click(object sender, RoutedEventArgs e)
        {
            Podaci.Instanca.jmbgZakazivanje = cbPolaznici.SelectedItem.ToString().Split('#')[0];
            DialogResult = true;
        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
