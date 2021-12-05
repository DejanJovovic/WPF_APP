using SR_52_2020_POP2021.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        ICollectionView viewInstruktori;
        public HomeWindow()
        {
            Podaci.Instanca.ucitajFajlove();//pokretanjem aplikacije prvo se ucitaju svi fajlovi u liste koje su u Podaci klasi

            //MessageBox.Show(
            //    "\n#Broj ucitanih adresa: " + Podaci.Instanca.lstAdrese.Count.ToString() +
            //    "\n#Br f centara:" + Podaci.Instanca.lstFitnesCentri.Count.ToString() +
            //    "\n#Br admina:" + Podaci.Instanca.lstAdmini.Count.ToString() + 
            //    "\n#Admin1:" + Podaci.Instanca.lstAdmini.ElementAt(0).ToString() +
            //    "\n#Br instr:" + Podaci.Instanca.lstInstruktori.Count.ToString() +
            //    "\n#Instr1:" + Podaci.Instanca.lstInstruktori.ElementAt(0).ToString() +
            //    "\n#Br polaznika:" + Podaci.Instanca.lstPolaznici.Count.ToString() +
            //    "\n#Polaznik1:" + Podaci.Instanca.lstPolaznici.ElementAt(0).ToString()
            //    );
            InitializeComponent();

            ucitajCombo_FitnesCentri();
            lbPrijavljenKorisnik.Visibility = Visibility.Hidden;//prikazace se podaci u gornjem levom uglu kad se prijavi neki korisnik
        }

        //u combo se ucitaju svi fitnes centri, njihovi nazivi
        void ucitajCombo_FitnesCentri()
        {
            if (Podaci.Instanca.lstFitnesCentri.Count > 0)
            {
                cbFitnesCentri.Items.Add("Svi fitnes centri"); //za prikaz svih
                lbAdresaFitnesCentra.Content = "Adresa:";

                foreach (FitnesCentar fc in Podaci.Instanca.lstFitnesCentri)
                    cbFitnesCentri.Items.Add(fc.Naziv);//dodaju se nazivi fitnes centara u combo box
                if (cbFitnesCentri.Items.Count > 0)
                {
                    cbFitnesCentri.SelectedIndex = 0;//prvi selektovan inicijalno
                    osveziDGInstruktori(cbFitnesCentri.SelectedItem.ToString());//da se prikaze selektovan, tj ovde prvo prikaze sve instruktore iz svih fitnes centaara
                }
            }
        }

        void osveziDGInstruktori(string nazivFC)
        {


            if (nazivFC == "svi")//ako je prosledjeno svi prikazuje sve instruktore
                viewInstruktori = CollectionViewSource.GetDefaultView(Podaci.Instanca.lstInstruktori.Where(ins => ins.obrisano == false).ToList());//log brisanje, neobrisani svi
            else {
                FitnesCentar fc = Podaci.Instanca.lstFitnesCentri.Where(f => f.Naziv == nazivFC).FirstOrDefault();//nadje objekat fc po nazivu 
                if (fc != null) {
                    viewInstruktori = CollectionViewSource.GetDefaultView(Podaci.Instanca.lstInstruktori.Where(ins => ins.obrisano == false && ins.IdFitnesCentra==fc.Id).ToList());
                }
            }


            dgInstruktori.ItemsSource = viewInstruktori;
            dgInstruktori.IsSynchronizedWithCurrentItem = true;
            dgInstruktori.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            if (dgInstruktori.Columns.Count > 0)
                dgInstruktori.Columns[1].Header = "Ime i prezime";//naziv kolone za ime i prezime




        }

        private void cbFitnesCentri_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFitnesCentri.SelectedIndex > 0)  //na promenu selekcije prikaze se adresa fitnes centra
            {
                FitnesCentar fc = Podaci.Instanca.lstFitnesCentri.Where(f => f.Naziv.Equals(cbFitnesCentri.SelectedItem.ToString())).FirstOrDefault();
                if (fc != null)
                {
                    lbAdresaFitnesCentra.Content = "Adresa: " + fc.Adresa.Ulica + " " + fc.Adresa.Broj + " " + fc.Adresa.Grad;//adresa sa desne strane combo boxa
                    osveziDGInstruktori(cbFitnesCentri.SelectedItem.ToString()); //da prikaze instruktore po nazivu fitnes centra
                }
            }
            else  //svi 
            {
                lbAdresaFitnesCentra.Content = "Adresa:";
                osveziDGInstruktori("svi");//prikaze sve instruktore
            }
        }

        private void dgInstruktori_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "IdFitnesCentra" || (string)e.Column.Header == "Korisnik") //da ne prikaze ove podatke u kolonama data grida
            {
                e.Cancel = true;
            }
        }

        private void btnRegistracija_Click(object sender, RoutedEventArgs e)
        {
            if (btnRegistracija.Content.ToString() == "Registracija")//ako na buttonu pise registracija bice registracija, ovaj prikaz se menja u Prikazi profil kad se korisnik prijavi
            {
                Polaznik noviPolaznik = new Polaznik();
                noviPolaznik.Korisnik = new Korisnik();
                noviPolaznik.Korisnik.Adresa = new Adresa();

                //otvara se forma za registraciju novog polaznika, podrazumevano u modu za dodavanje
                RegistracijaPolaznikaWindow regWind = new RegistracijaPolaznikaWindow(noviPolaznik);//mogu se registrovati samo polaznici a ostale moze admin kad se prijavi
                regWind.ShowDialog();

            }else if(btnRegistracija.Content.ToString() == "Prikazi profil")//ako je na buttonu prikazi da prikaze za prijavljeni tip korisnika
            {
                if (Podaci.Instanca.tipPrijavljen == ETipKorisnika.ADMINISTRATOR)//prijavom se promenio tip korisnika u klasi Podaci i sada prikazati profil admina ako je on odabran
                {
                    ProfilAdminWindow pa = new ProfilAdminWindow();
                    pa.ShowDialog();
                }
                else if (Podaci.Instanca.tipPrijavljen == ETipKorisnika.POLAZNIK)
                {
                    ProfilPolaznikWindow pp = new ProfilPolaznikWindow();
                    pp.ShowDialog();
                }
                else if (Podaci.Instanca.tipPrijavljen == ETipKorisnika.INSTRUKTOR)
                {
                    ProfilInstruktorWindow pi = new ProfilInstruktorWindow();
                    pi.ShowDialog();
                }
            }
          
        }

       

        private void btnPrijava_Click(object sender, RoutedEventArgs e)
        {
            if (btnPrijava.Content.ToString() == "Prijava")
            {
                PrijavaWindow pw = new PrijavaWindow();
                pw.ShowDialog();//otvoren prozor za prijavu

                if (pw.DialogResult == true)//ako je uspesna prijava iz otvorene forme za prijavu
                {

                    prikaziPodatkePrijavljen_Lb();//u gornjem levom uglu po prijavi podaci prijavljenog
                    btnRegistracija.Content = "Prikazi profil";//promeni tekst buttona za registraciju 
                    btnPrijava.Content = "Odjava"; //ako je uspesna prijava dugme postaje dugme za odjavu
                }

            }else if(btnPrijava.Content.ToString() == "Odjava") //kada se klikne na odjavu opet se moze prijaviti, ponisten jmbg prijavljenog iz Podaci klase
            {
                btnPrijava.Content = "Prijava";
                btnRegistracija.Content = "Registracija";//resetuj dugmad prikaz po odjavi
                Podaci.Instanca.jmbgPrijavljen = "";//
                lbPrijavljenKorisnik.Visibility = Visibility.Hidden;

            }
        }


        void prikaziPodatkePrijavljen_Lb()
        {
            lbPrijavljenKorisnik.Visibility = Visibility.Visible; //prikazati podatke o prijavljenom korisniku u labelu, gornji levi ugao

            if (Podaci.Instanca.tipPrijavljen == ETipKorisnika.ADMINISTRATOR)
            {
                Korisnik k = Podaci.Instanca.lstAdmini.Where(a => a.Jmbg == Podaci.Instanca.jmbgPrijavljen).FirstOrDefault();
                if (k != null)
                    lbPrijavljenKorisnik.Content = "Prijavljen korisnik: " + k.Ime + " " + k.Prezime + ", Jmbg: " + k.Jmbg + ", Tip korisnika: " + k.TipKorisnika;

            }
            else if (Podaci.Instanca.tipPrijavljen == ETipKorisnika.POLAZNIK)
            {
                Polaznik p = Podaci.Instanca.lstPolaznici.Where(a => a.Korisnik.Jmbg == Podaci.Instanca.jmbgPrijavljen).FirstOrDefault();
                if (p != null)
                    lbPrijavljenKorisnik.Content = "Prijavljen korisnik: " + p.Korisnik.Ime + " " + p.Korisnik.Prezime + ", Jmbg: " + p.Korisnik.Jmbg + ", Tip korisnika: " + p.Korisnik.TipKorisnika;
            }
            else if (Podaci.Instanca.tipPrijavljen == ETipKorisnika.INSTRUKTOR)
            {
                Instruktor instr = Podaci.Instanca.lstInstruktori.Where(a => a.Korisnik.Jmbg == Podaci.Instanca.jmbgPrijavljen).FirstOrDefault();
                if (instr != null)
                    lbPrijavljenKorisnik.Content = "Prijavljen korisnik: " + instr.Korisnik.Ime + " " + instr.Korisnik.Prezime + ", Jmbg: " + instr.Korisnik.Jmbg + ", Tip korisnika: " + instr.Korisnik.TipKorisnika;
            }
        }

        private void btnPrikaziTermine_Click(object sender, RoutedEventArgs e)
        {
            ProfilInstruktorWindow piw = new ProfilInstruktorWindow();
            piw.ShowDialog();
        }
    }
}
