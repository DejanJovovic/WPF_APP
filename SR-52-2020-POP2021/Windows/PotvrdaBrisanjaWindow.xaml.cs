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
    /// Interaction logic for PotvrdaBrisanjaWindow.xaml
    /// </summary>
    public partial class PotvrdaBrisanjaWindow : Window
    {
        public PotvrdaBrisanjaWindow()
        {
            InitializeComponent();
        }

        private void btnDa_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;//ovo se prati iz forme koja je pozvala da je potvrdjeno brisanje
        }

        private void btnNe_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
