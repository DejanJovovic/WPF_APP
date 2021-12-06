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

        }

        private void btnPrijava_Click(object sender, RoutedEventArgs e)
        {
            if(tbKorisnicko.Text=="" || pbLozinka.Password == "") //provera da podaci moraju biti uneti
            {
                MessageBox.Show("Morate uneti korisnicko ime i lozinku!");
            }
            else
            {
                bool pronadjen = false;

                foreach(Korisnik k in Podaci.Instanca.lstAdmini)//pretraga kroz listu admina
                {
                    if(k.Jmbg==tbKorisnicko.Text && k.Lozinka == pbLozinka.Password)//ako se odredjenom adminu poklapaju korisnicko i lozinka s unetim
                    {
                        pronadjen = true;//indikator promenjen da je pronadjen
                        Podaci.Instanca.jmbgPrijavljen = k.Jmbg;
                        Podaci.Instanca.tipPrijavljen = k.TipKorisnika; //setovana ova dva podatka u klasi Podaci da se zna u proverama kome se sta prikazuje kao opcija
                        DialogResult = true;
                    }
                }

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
