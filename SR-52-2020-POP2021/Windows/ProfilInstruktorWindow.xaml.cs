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
    /// Interaction logic for ProfilInstruktorWindow.xaml
    /// </summary>
    /// 
    // ako se prijavio polaznik, ima opciju na pocetnoj strani, kada selektuje instruktora da otvori ovu formu i zakaze termin treninga
    //ova forma je dostupna i polaznicima i adminima kada zakazuju termin treninga zato sto ujedno su prikazani i podaci po datumima za odabranog instruktora
    public partial class ProfilInstruktorWindow : Window
    {
        Instruktor instruktor = null;//ako nije prosledjen selektovan instruktor iz pocetne strane, setovace se na osnovu: Podaci.Instanca.jmbgPrijavljen s profila instruktora kad je prijavljen

        public ProfilInstruktorWindow(Instruktor instruktor) //prosledjuje se selektovan instruktor s pocetne strane ako je prijavljen polaznik ili admin
        {
            InitializeComponent();
            this.instruktor = instruktor;//null ako nije prijavljen instruktor, setovace se u nastavku
            dtDatumPrikaz.SelectedDate = DateTime.Now;

            inicijalizujVidljivosti();
            inicijalizujDGKolone();
            osveziPrikazTermina();


        }

        void inicijalizujDGKolone()
        {
            DataGridTextColumn textColumn1 = new DataGridTextColumn();
            textColumn1.Header = "Vreme pocetka";
            textColumn1.Binding = new Binding("VremePocetka");
            dgTerminiTreninga.Columns.Add(textColumn1);

            DataGridTextColumn textColumn2 = new DataGridTextColumn();
            textColumn2.Header = "Trajanje(min)";
            textColumn2.Binding = new Binding("TrajanjeTreninga");
            dgTerminiTreninga.Columns.Add(textColumn2);

            DataGridTextColumn textColumn3 = new DataGridTextColumn();
            textColumn3.Header = "Ime i prezime polaznika";
            textColumn3.Binding = new Binding("ImePrezimePolaznika");
            dgTerminiTreninga.Columns.Add(textColumn3);

            //AutoGenerateColumns="False" u xaml kodu
        }

        //obzirom da je forma dostupna za sva 3 tipa korisnika ovde je podeseno kome su koje opcije vidljive/dostupne
        void inicijalizujVidljivosti()
        {
            if (Podaci.Instanca.tipPrijavljen != ETipKorisnika.INSTRUKTOR)//formu je otvorio polaznik ili admin da zakaze termin i prosledjen je selektovani instruktor
            {
                this.Title = "Zakazivanje termina treninga";
                lbInstruktorPodaci.Content = "Instruktor: " + instruktor.Korisnik.Ime + " " + instruktor.Korisnik.Prezime;
                lbInstruktorPodaci.Visibility = Visibility.Visible;
            }
            else
            {
                lbInstruktorPodaci.Visibility = Visibility.Hidden;

            }

            if (Podaci.Instanca.tipPrijavljen == ETipKorisnika.INSTRUKTOR)//samo ako je instruktor prijavljen mogu se prikazati svi detalji njegovog profila
            {
                btnPodaciProfila.Visibility = Visibility.Visible;
                Instruktor instrPrijavljen = Podaci.Instanca.lstInstruktori.Where(ins => ins.Korisnik.Jmbg == Podaci.Instanca.jmbgPrijavljen).FirstOrDefault();
                if (instrPrijavljen != null)
                    this.instruktor = new Instruktor(instrPrijavljen);

            }
            else
                btnPodaciProfila.Visibility = Visibility.Hidden;


            if (Podaci.Instanca.tipPrijavljen == ETipKorisnika.POLAZNIK)
            {
                btnDodajTermin.Visibility = Visibility.Hidden;
                btnBrisiTermin.Visibility = Visibility.Hidden; //polaznik ne moze dodavati i brisati termine
            }
            if (Podaci.Instanca.tipPrijavljen == ETipKorisnika.INSTRUKTOR)
            {
                btnZakaziTermin.Visibility = Visibility.Hidden; //instruktori ne mogu zakazivati termine
                btnOtkaziTermin.Visibility = Visibility.Hidden;
            }
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

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void osveziPrikazTermina() //svaki put kad se promeni datum ili izmeni nesto u terminima po datumu
        {
            if (dtDatumPrikaz.SelectedDate.Value != null && this.instruktor!=null)
            {

    
                dgTerminiTreninga.ItemsSource = Podaci.Instanca.lstTreninzi.Where(t =>
                      t.obrisano == false &&
                      t.Instruktor.Korisnik.Jmbg == this.instruktor.Korisnik.Jmbg &&
                      t.DatumTreninga.Date.Day == dtDatumPrikaz.SelectedDate.Value.Date.Day &&
                      t.DatumTreninga.Date.Month == dtDatumPrikaz.SelectedDate.Value.Date.Month &&
                      t.DatumTreninga.Date.Year == dtDatumPrikaz.SelectedDate.Value.Date.Year
                ).OrderBy(t => int.Parse(t.VremePocetka.Split(':')[0])).ThenBy(t => int.Parse(t.VremePocetka.Split(':')[1])).ToList();

                dgTerminiTreninga.Items.Refresh();

               

                //if (dgTerminiTreninga.Columns.Count > 0)
                //{
                //    dgTerminiTreninga.Columns[0].Header = "Vreme pocetka";
                //    dgTerminiTreninga.Columns[1].Header = "Trajanje(min)";
                //    dgTerminiTreninga.Columns[2].Header = "Ime i prezime polaznika";
                //}

                //dgTerminiTreninga.Items.Refresh();
            }
        }

        private void btnDodajTermin_Click(object sender, RoutedEventArgs e)
        {
            UpisTerminaWindow ut = new UpisTerminaWindow(dtDatumPrikaz.SelectedDate.Value, this.instruktor);
            ut.ShowDialog();

            if (ut.DialogResult == true) //ako je potvrdjeno dodavanje
            {
                osveziPrikazTermina();
            }
        }

        private void dgTerminiTreninga_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (
                (string)e.Column.Header == "Id" || 
                (string)e.Column.Header == "DatumTreninga" ||
                (string)e.Column.Header == "Slobodan" ||
                (string)e.Column.Header == "Polaznik" ||
                (string)e.Column.Header == "ImePrezimeInstruktora" ||
                (string)e.Column.Header == "Instruktor") //da ne prikaze ove podatke u kolonama data grida   
            {
                e.Cancel = true;
            }

            //this.Id = id;
            //this.DatumTreninga = datumTreninga;
            //this.VremePocetka = vremePocetka;
            //this.TrajanjeTreninga = trajanjeTreninga;
            //this.Slobodan = slobodan;
            //this.Instruktor = instruktor;
            //this.Polaznik = polaznik;
        }

        private void dtDatumPrikaz_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            osveziPrikazTermina();
        }

        private void btnZakaziTermin_Click(object sender, RoutedEventArgs e)
        {
            if (dgTerminiTreninga.SelectedIndex > -1)//ako je selektovan termin treninga iz data grida
            {
                Trening selektovanTrening = (Trening)dgTerminiTreninga.SelectedItem;//napravi objekat od selektovanog reda tabele
                if (selektovanTrening.Polaznik != null)
                {
                    MessageBox.Show("Selektovani termin treninga je vec zakazan!");
                }
                else if (selektovanTrening.DatumTreninga < DateTime.Now.AddDays(-1))
                {
                    MessageBox.Show("Ne mozete zakazati trening pre danasnjeg dana!");
                }
                else
                {


                    if (Podaci.Instanca.tipPrijavljen == ETipKorisnika.POLAZNIK)
                    {

                        zakaziTerminTreninga(Podaci.Instanca.jmbgPrijavljen, selektovanTrening.Id);
                        
                    }

                    else if (Podaci.Instanca.tipPrijavljen == ETipKorisnika.ADMINISTRATOR)
                    {
                        if (Podaci.Instanca.lstPolaznici.Count > 0)
                        {
                            ZakazivanjeTerminaWindow ztw = new ZakazivanjeTerminaWindow();
                            ztw.ShowDialog();//otvara prozor za odabir polaznika

                            if (ztw.DialogResult==true && Podaci.Instanca.jmbgZakazivanje != "")
                            {
                                zakaziTerminTreninga(Podaci.Instanca.jmbgZakazivanje, selektovanTrening.Id);
                               
                            }

                        }
                    }


                }

            }
            else
            {
                MessageBox.Show("Prvo morate selektovati trening!");
            }
        }

        void zakaziTerminTreninga(string jmbgPolaznika, int idTreninga)
        {
            Polaznik polaznikZakazi = Podaci.Instanca.lstPolaznici.Where(p => p.Jmbg == jmbgPolaznika).FirstOrDefault();
            if (polaznikZakazi != null)
            {
                Trening treningZakazi = Podaci.Instanca.lstTreninzi.Where(t => t.Id == idTreninga).FirstOrDefault();
                if (treningZakazi != null)
                {
                    treningZakazi.Polaznik = new Polaznik(polaznikZakazi);//setuje polaznika u objektu
                    treningZakazi.ImePrezimePolaznika = polaznikZakazi.Ime + " " + polaznikZakazi.Prezime;
                    osveziPrikazTermina();

                    TreninziServis ts = new TreninziServis();
                    ts.upisFajla(Podaci.Instanca.lstTreninzi);//sacuva modifikovanu listu u fajlu
                }
            }
        }

        private void btnOtkaziTermin_Click(object sender, RoutedEventArgs e)
        {
            if (Podaci.Instanca.tipPrijavljen == ETipKorisnika.POLAZNIK || Podaci.Instanca.tipPrijavljen == ETipKorisnika.ADMINISTRATOR)
            {
                if (dgTerminiTreninga.SelectedIndex > -1)//ako je selektovan termin treninga iz data grida
                {
                    Trening selektovanTrening = (Trening)dgTerminiTreninga.SelectedItem;//napravi objekat od selektovanog reda tabele
                    if (selektovanTrening.Polaznik != null)//moze se otkazati ako je zakazan, i to za prijavljenog polaznika-provera u nastavku
                    {

                        if (Podaci.Instanca.tipPrijavljen == ETipKorisnika.POLAZNIK)
                        {
                            //prvo preuzeti prijavljenog polaznika na osnovu jmbg prijavljenog iz Podaci klase
                            Polaznik prijavljeniPolaznik = Podaci.Instanca.lstPolaznici.Where(p => p.Jmbg == Podaci.Instanca.jmbgPrijavljen).FirstOrDefault();
                            if (prijavljeniPolaznik != null)
                            {
                                if (selektovanTrening.Polaznik.Korisnik.Jmbg == Podaci.Instanca.jmbgPrijavljen)//moze otkazivati samo svoje prijave
                                {
                                    otkaziTrening(selektovanTrening.Id);
                                }
                                else
                                {
                                    MessageBox.Show("Mozete otkazati samo sopstvene prijave termina!");
                                }
                            }
                        }
                        else if(Podaci.Instanca.tipPrijavljen == ETipKorisnika.ADMINISTRATOR) //admin moze otkazati bilo koji zakazan termin
                        {
                            otkaziTrening(selektovanTrening.Id);
                        }



                    }
                    else
                    {
                        MessageBox.Show("Selektovani termin nije zakazan!");
                    }
                }
                else
                {
                    MessageBox.Show("Prvo morate selektovati trening!");
                }
            }
        }

        void otkaziTrening(int idTreninga)
        {
            Trening treningOtkazi = Podaci.Instanca.lstTreninzi.Where(t => t.Id == idTreninga).FirstOrDefault();
            if (treningOtkazi != null)
            {
                treningOtkazi.Polaznik = null;//brise polaznika, setuje na null, otkazana prijava
                treningOtkazi.ImePrezimePolaznika = "";
                osveziPrikazTermina();

                TreninziServis ts = new TreninziServis();
                ts.upisFajla(Podaci.Instanca.lstTreninzi);//sacuva modifikovanu listu u fajlu
            }
        }

        private void btnBrisiTermin_Click(object sender, RoutedEventArgs e)
        {
            if (dgTerminiTreninga.SelectedIndex > -1)
            {
                Trening selektovanTrening = (Trening)dgTerminiTreninga.SelectedItem;//napravi objekat od selektovanog reda tabele
                Trening treningBrisi = Podaci.Instanca.lstTreninzi.Where(t => t.Id == selektovanTrening.Id).FirstOrDefault();
                if (treningBrisi != null)
                {

                    if (Podaci.Instanca.tipPrijavljen == ETipKorisnika.ADMINISTRATOR)
                    {
                        PotvrdaBrisanjaWindow pbw = new PotvrdaBrisanjaWindow();
                        pbw.ShowDialog();
                        if (pbw.DialogResult == true)
                        {
                            treningBrisi.obrisano = true;
                            TreninziServis ts = new TreninziServis();
                            ts.upisFajla(Podaci.Instanca.lstTreninzi);//sacuva modifikovanu listu u fajlu

                            osveziPrikazTermina();
                        }
                    }
                    else if (Podaci.Instanca.tipPrijavljen == ETipKorisnika.INSTRUKTOR)
                    {
                        if (treningBrisi.Polaznik != null)
                        {
                            MessageBox.Show("Instruktor ne moze obrisati zakazan trening!");
                        }
                        else
                        {
                            PotvrdaBrisanjaWindow pbw = new PotvrdaBrisanjaWindow();
                            pbw.ShowDialog();
                            if (pbw.DialogResult == true)
                            {
                                treningBrisi.obrisano = true;
                                TreninziServis ts = new TreninziServis();
                                ts.upisFajla(Podaci.Instanca.lstTreninzi);//sacuva modifikovanu listu u fajlu

                                osveziPrikazTermina();
                            }
                        }
                    }

                }
            }
        }
    }
}
