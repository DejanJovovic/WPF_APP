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
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            Podaci.Instanca.ucitajFajlove();
            MessageBox.Show("Broj ucitanih adresa iz txt fajla: " + Podaci.Instanca.lstAdrese.Count.ToString());
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            SviInstruktori sviInstruktori = new SviInstruktori();
            this.Hide();
            sviInstruktori.Show();
        }
    }
}
