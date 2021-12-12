using SR_52_2020_POP2021.Model;
using SR_52_2020_POP2021.Services;
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
    /// Interaction logic for ProfilAdminWindow.xaml
    /// </summary>
    public partial class ProfilAdminWindow : Window
    {
        ICollectionView viewKorisnici;
        public ProfilAdminWindow()
        {
            InitializeComponent();

            cbTipKorisnika.ItemsSource = Enum.GetValues(typeof(ETipKorisnika)).Cast<ETipKorisnika>(); //inicijalizuje combo za tipove korisnika
            cbTipKorisnika.SelectedIndex = 1;//selektovani polaznici prvo
        }

        private void btnUpis_Click(object sender, RoutedEventArgs e)
        {
            if (cbTipKorisnika.SelectedIndex == 0)//admini selektovani u combu, otvorice se forma za upis admina
            {
                Korisnik admin = new Korisnik();//novi admin ce se proslediti formi za reg admina i tamo ce preko data bindinga da se unesu podaci i da bude dodat u listu i u fajl
                admin.Adresa.Drzava = "Srbija";

                RegistracijaAdminaWindow ra = new RegistracijaAdminaWindow(admin);//prosledjen admin, forma podrazumevano u modu za dodavanje
                ra.ShowDialog();
                if (ra.DialogResult == true)//ako je potvrdjeno dodavanje u otvorenoj formi
                {
                    osveziDGKorisnici();//refresh podataka nakon dodavanja u datagridu
                }
            }
            else if (cbTipKorisnika.SelectedIndex == 1)//polaznici
            {
                Polaznik polaznik = new Polaznik();
                polaznik.Korisnik.Adresa.Drzava = "Srbija";

                RegistracijaPolaznikaWindow rp = new RegistracijaPolaznikaWindow(polaznik);
                rp.ShowDialog();
                if (rp.DialogResult == true)
                {
                    osveziDGKorisnici();
                }
            }
            else if (cbTipKorisnika.SelectedIndex == 2)//instruktori
            {
                Instruktor instr = new Instruktor();
                instr.Korisnik.Adresa.Drzava = "Srbija";

                RegistracijaInstruktoraWindow ri = new RegistracijaInstruktoraWindow(instr);
                ri.ShowDialog();
                if (ri.DialogResult == true)
                {
                    osveziDGKorisnici();
                }
            }
        }

        private void btnIzmena_Click(object sender, RoutedEventArgs e)
        {
            if (cbTipKorisnika.SelectedIndex == 0)//admini selektovana opcija i combo
            {

                Korisnik selektovanAdmin = viewKorisnici.CurrentItem as Korisnik; //napravi objekat od selektovanog reda iz datagrid tabele
                if (selektovanAdmin != null)//ako je selektovan reed ima smisla praviti izmenu. 
                {
                    Korisnik copyAdmin = new Korisnik(selektovanAdmin);//pravi se kopija objekta za slucaj da na otvorenoj formi nije potvrdjena izmena.
                                //zbog data bindinga podaci se svakako promene u objektu-ako su promene u poljima napravljene, iako nije potvrdjena izmena
                    

                    RegistracijaAdminaWindow ra = new RegistracijaAdminaWindow(selektovanAdmin, EStatus.IZMENI);//prosledjuje se selektovani objekat formi i tamo 
                                            //ce se po data bindingu sva polja biti popunjena podacima objekta. Forma je u modu izmene
                    ra.ShowDialog();

                    if (ra.DialogResult != true)//ako nije potvrdjena izmena pronaci objekat i vratiti kopiju sacuvano pre otvaranja forme za izmenu
                    {
                        
                        int index = Podaci.Instanca.lstAdmini.IndexOf(selektovanAdmin);//indeks elementa koji se mozda promenio u otvorenoj formi
                        Podaci.Instanca.lstAdmini[index] = copyAdmin;//vratiti kopiju na taj indeks
                    }

                    if (ra.DialogResult == true)
                    {
                        if (copyAdmin.Jmbg == Podaci.Instanca.jmbgPrijavljen)//ako je admin promenio svoj jmbg azurirati jmbg prijavljenog
                        {
                            Podaci.Instanca.jmbgPrijavljen = selektovanAdmin.Jmbg;
                        }
                    }

                }

                osveziDGKorisnici();//refresh datagrida nakon izmene
  
            }
            else if (cbTipKorisnika.SelectedIndex == 1)//polaznici
            {

                Polaznik selektovanPolaznik = viewKorisnici.CurrentItem as Polaznik;
                if (selektovanPolaznik != null)
                {
                    Polaznik copyPolaznik = new Polaznik(selektovanPolaznik);


                    RegistracijaPolaznikaWindow rp = new RegistracijaPolaznikaWindow(selektovanPolaznik, EStatus.IZMENI);
                    rp.ShowDialog();

                    if (rp.DialogResult != true)
                    {

                        int index = Podaci.Instanca.lstPolaznici.IndexOf(selektovanPolaznik);
                        Podaci.Instanca.lstPolaznici[index] = copyPolaznik;
                    }

                    

                }

                osveziDGKorisnici();

            }
            else if (cbTipKorisnika.SelectedIndex == 2)//instruktori
            {

                Instruktor selektovanInstruktor = viewKorisnici.CurrentItem as Instruktor;
                if (selektovanInstruktor != null)
                {
                    Instruktor copyInstruktor = new Instruktor(selektovanInstruktor);


                    RegistracijaInstruktoraWindow ri = new RegistracijaInstruktoraWindow(selektovanInstruktor, EStatus.IZMENI);
                    ri.ShowDialog();

                    if (ri.DialogResult != true)
                    {

                        int index = Podaci.Instanca.lstInstruktori.IndexOf(selektovanInstruktor);
                        Podaci.Instanca.lstInstruktori[index] = copyInstruktor;
                    }



                }

                osveziDGKorisnici();

            }


        }

        private void btnBrisanje_Click(object sender, RoutedEventArgs e)
        {
            if (cbTipKorisnika.SelectedIndex == 0)//admini selektovana opcija combo boxa
            {
                Korisnik selektovanAdmin = viewKorisnici.CurrentItem as Korisnik;//napravljen objekat od selektovanog reda datagrida
                PotvrdaBrisanjaWindow pb = new PotvrdaBrisanjaWindow();//otvara formu za potvrdu brisanja. Dialog result je true ako se tamo klikne na Da
                pb.ShowDialog();
                if (pb.DialogResult == true)//ako je kliknuto na Da u formi za potvrdu brisanja
                {
                    selektovanAdmin.obrisano = true;//logicko brisanje, prikazace se oni kojima je obrisano false
                    AdminiServis admServis = new AdminiServis();
                    admServis.upisFajla(Podaci.Instanca.lstAdmini);//azuriraj fajl, prebrise sve podatke u fajlu. Dodavanjem baze pozivace se metoda kojoj ce se proslediti sql naredba

                    osveziDGKorisnici();//refresh nakon brisanja. Prikazuju se samo podaci kojima je obrisan na false
                }
            }else if (cbTipKorisnika.SelectedIndex == 1)//polaznici
            {
                Polaznik selektovanPolaznik = viewKorisnici.CurrentItem as Polaznik;
                PotvrdaBrisanjaWindow pb = new PotvrdaBrisanjaWindow();
                pb.ShowDialog();
                if (pb.DialogResult == true)
                {
                    selektovanPolaznik.obrisano = true;
                    PolazniciServis polazServis = new PolazniciServis();
                    polazServis.upisFajla(Podaci.Instanca.lstPolaznici);//azuriraj fajl

                    osveziDGKorisnici();
                }
            }
            else if (cbTipKorisnika.SelectedIndex == 2)//instruktori
            {
                Instruktor selektovanInstruktor = viewKorisnici.CurrentItem as Instruktor;
                PotvrdaBrisanjaWindow pb = new PotvrdaBrisanjaWindow();
                pb.ShowDialog();
                if (pb.DialogResult == true)
                {
                    selektovanInstruktor.obrisano = true;
                    InstruktoriServis instrServis = new InstruktoriServis();
                    instrServis.upisFajla(Podaci.Instanca.lstInstruktori);//azuriraj fajl

                    osveziDGKorisnici();
                }
            }
        }

        private void cbTipKorisnika_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            osveziDGKorisnici();//na promenu selekcije combo za tip korisnika osvezava se prikaz odredjenog tipa korisnika
        }
        void osveziDGKorisnici()
        {

            if (cbTipKorisnika.SelectedIndex == 0)//admini selektovani u combo
                viewKorisnici = CollectionViewSource.GetDefaultView(Podaci.Instanca.lstAdmini.Where(ins => ins.obrisano == false && ins.Jmbg!=Podaci.Instanca.jmbgPrijavljen).ToList());
            //log brisanje, neobrisani svi. Ne prikazuju se oni kojima je obrisano=true i admin koji se prijavio za admine
            else if (cbTipKorisnika.SelectedIndex == 1)//polaznici
                viewKorisnici = CollectionViewSource.GetDefaultView(Podaci.Instanca.lstPolaznici.Where(ins => ins.obrisano == false).ToList());
            else if (cbTipKorisnika.SelectedIndex == 2)//instruktori
                viewKorisnici = CollectionViewSource.GetDefaultView(Podaci.Instanca.lstInstruktori.Where(ins => ins.obrisano == false).ToList());


            dgKorisnici.ItemsSource = viewKorisnici;
            dgKorisnici.IsSynchronizedWithCurrentItem = true;
            dgKorisnici.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);


        }

        private void dgKorisnici_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (
                //(string)e.Column.Header == "Ime" ||
                //(string)e.Column.Header == "Prezime" ||
                //(string)e.Column.Header == "Jmbg" ||
                (string)e.Column.Header == "Pol" ||
                (string)e.Column.Header == "Adresa" ||
                (string)e.Column.Header == "Email" ||
                (string)e.Column.Header == "Lozinka" ||
                (string)e.Column.Header == "TipKorisnika" ||
                (string)e.Column.Header == "Korisnik" ||
                (string)e.Column.Header == "ImePrezime"
                )  //ove kolone se nece prikazati u data gridu
            {
                e.Cancel = true;
            }

        }

        private void btnPodaciProfila_Click(object sender, RoutedEventArgs e)
        {
            Korisnik admin = Podaci.Instanca.lstAdmini.Where(a => a.Jmbg == Podaci.Instanca.jmbgPrijavljen).FirstOrDefault();//naci objekat admina koji je prijavljen
            if (admin != null)
            {

                Korisnik copyAdmin = new Korisnik(admin);//kopija koja ce se sacuvati pre otvaranja forme u modu izmene, ako se tamo desila izmena a nije potvrdjeno da se vrati na prethodno stanje

                RegistracijaAdminaWindow ra = new RegistracijaAdminaWindow(admin, EStatus.IZMENI);
                ra.ShowDialog();

                if (ra.DialogResult != true)//ako nije potvrdjena izmena a zbog bindinga neki podaci promenjeni u objektu vratiti na kopiju objekta pre otvaranja forme
                {
                    int index = Podaci.Instanca.lstAdmini.IndexOf(admin);
                    Podaci.Instanca.lstAdmini[index] = copyAdmin;//setuj prethodni objekat
                }
                if (ra.DialogResult == true)
                {
                    if (copyAdmin.Jmbg == Podaci.Instanca.jmbgPrijavljen)//ako je admin promenio svoj jmbg azurirati jmbg prijavljenog
                    {
                        Podaci.Instanca.jmbgPrijavljen = admin.Jmbg;
                    }
                }
            }
        }

        private void btnFitnesCentri_Click(object sender, RoutedEventArgs e)
        {
            FitnesCentriWindow fcw = new FitnesCentriWindow();
            fcw.ShowDialog();
        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tbPretragaInstruktora_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (tbPretraga.Text == "")
            {
                osveziDGKorisnici();
            }
            else
            {
                if (cbTipKorisnika.SelectedIndex == 0)//admini selektovani u combo
                    viewKorisnici = CollectionViewSource.GetDefaultView(Podaci.Instanca.lstAdmini.Where(
                            a => a.obrisano == false && a.Jmbg != Podaci.Instanca.jmbgPrijavljen &&
                            (
                               a.Ime.ToLower().Contains(tbPretraga.Text.ToLower()) ||
                               a.Prezime.ToLower().Contains(tbPretraga.Text.ToLower()) ||
                               a.Email.ToLower().Contains(tbPretraga.Text.ToLower()) ||
                               a.Adresa.Ulica.ToLower().Contains(tbPretraga.Text.ToLower()) ||
                               a.Adresa.Broj.ToLower().Contains(tbPretraga.Text.ToLower()) ||
                               a.Adresa.Grad.ToLower().Contains(tbPretraga.Text.ToLower()) ||
                               a.Adresa.Drzava.ToLower().Contains(tbPretraga.Text.ToLower()) 
                            )
                        
                        ).ToList());
                //log brisanje, neobrisani svi. Ne prikazuju se oni kojima je obrisano=true i admin koji se prijavio za admine
                //prvi uslov da podatak nije obrisan a ostali uslovi medjusobno iskljucivi ako je u text boxu unet deo vrednosti property-ja. toLower() da bi bilo case insensitive
                
                else if (cbTipKorisnika.SelectedIndex == 1)//polaznici
                    viewKorisnici = CollectionViewSource.GetDefaultView(Podaci.Instanca.lstPolaznici.Where(
                        p => p.obrisano == false &&
                        (
                            p.Korisnik.Ime.ToLower().Contains(tbPretraga.Text.ToLower()) ||
                            p.Korisnik.Prezime.ToLower().Contains(tbPretraga.Text.ToLower()) ||
                            p.Korisnik.Email.ToLower().Contains(tbPretraga.Text.ToLower()) ||
                            p.Korisnik.Adresa.Ulica.ToLower().Contains(tbPretraga.Text.ToLower()) ||
                            p.Korisnik.Adresa.Broj.ToLower().Contains(tbPretraga.Text.ToLower()) ||
                            p.Korisnik.Adresa.Grad.ToLower().Contains(tbPretraga.Text.ToLower()) ||
                            p.Korisnik.Adresa.Drzava.ToLower().Contains(tbPretraga.Text.ToLower())
                        )
                        
                        ).ToList());
                
                else if (cbTipKorisnika.SelectedIndex == 2)//instruktori
                    viewKorisnici = CollectionViewSource.GetDefaultView(Podaci.Instanca.lstInstruktori.Where(
                        ins => ins.obrisano == false &&
                        (
                            ins.Korisnik.Ime.ToLower().Contains(tbPretraga.Text.ToLower()) ||
                            ins.Korisnik.Prezime.ToLower().Contains(tbPretraga.Text.ToLower()) ||
                            ins.Korisnik.Email.ToLower().Contains(tbPretraga.Text.ToLower()) ||
                            ins.Korisnik.Adresa.Ulica.ToLower().Contains(tbPretraga.Text.ToLower()) ||
                            ins.Korisnik.Adresa.Broj.ToLower().Contains(tbPretraga.Text.ToLower()) ||
                            ins.Korisnik.Adresa.Grad.ToLower().Contains(tbPretraga.Text.ToLower()) ||
                            ins.Korisnik.Adresa.Drzava.ToLower().Contains(tbPretraga.Text.ToLower())
                        )
                        ).ToList());


                dgKorisnici.ItemsSource = viewKorisnici;
                dgKorisnici.IsSynchronizedWithCurrentItem = true;
                dgKorisnici.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            }


        }
    }
}
