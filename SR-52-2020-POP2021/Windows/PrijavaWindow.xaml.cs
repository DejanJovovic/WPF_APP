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
    /// Interaction logic for PrijavaWindow.xaml
    /// </summary>
    public partial class PrijavaWindow : Window
    {
        public PrijavaWindow()
        {
            InitializeComponent();
            cbTipoviKorisnika.ItemsSource = Enum.GetValues(typeof(ETipKorisnika)).Cast<ETipKorisnika>();
            cbTipoviKorisnika.SelectedIndex = 1;
        }

        private void btnPrijava_Click(object sender, RoutedEventArgs e)
        {
            if(tbKorisnicko.Text=="" || pbLozinka.Password == "")
            {
                MessageBox.Show("Morate uneti korisnicko ime i lozinku!");
            }
            else
            {
                bool pronadjen = false;
                if (cbTipoviKorisnika.SelectedIndex == 0)//admini
                {
                    foreach(Korisnik k in Podaci.Instanca.lstAdmini)
                    {
                        if(k.Jmbg==tbKorisnicko.Text && k.Lozinka == pbLozinka.Password)
                        {
                            pronadjen = true;
                            Podaci.Instanca.jmbgPrijavljen = k.Jmbg;
                            Podaci.Instanca.tipPrijavljen = k.TipKorisnika;
                            DialogResult = true;
                        }
                    }
                }
                else if (cbTipoviKorisnika.SelectedIndex == 1)//polaznici
                {
                    foreach (Polaznik p in Podaci.Instanca.lstPolaznici)
                    {
                        if (p.Korisnik.Jmbg == tbKorisnicko.Text && p.Korisnik.Lozinka == pbLozinka.Password)
                        {
                            pronadjen = true;
                            Podaci.Instanca.jmbgPrijavljen = p.Korisnik.Jmbg;
                            Podaci.Instanca.tipPrijavljen = p.Korisnik.TipKorisnika;
                            DialogResult = true;
                        }
                    }
                }
                else if (cbTipoviKorisnika.SelectedIndex == 2)//instruktori
                {
                    foreach (Instruktor instr in Podaci.Instanca.lstInstruktori)
                    {
                        if (instr.Korisnik.Jmbg == tbKorisnicko.Text && instr.Korisnik.Lozinka == pbLozinka.Password)
                        {
                            pronadjen = true;
                            Podaci.Instanca.jmbgPrijavljen = instr.Korisnik.Jmbg;
                            Podaci.Instanca.tipPrijavljen = instr.Korisnik.TipKorisnika;
                            DialogResult = true;
                        }
                    }
                }


                if (!pronadjen)
                    MessageBox.Show("Uneli ste nepostojecu kombinaciju korisnickog imena i lozinke!");
            }
        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
