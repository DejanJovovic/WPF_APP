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
    /// Interaction logic for RegistracijaAdminaWindow.xaml
    /// </summary>
    public partial class RegistracijaAdminaWindow : Window
    {
        Korisnik admin = null;
        EStatus status;
        public RegistracijaAdminaWindow(Korisnik admin, EStatus status = EStatus.DODAJ) //forma radi za dodavanje i izmenu zavisno od moda
        {
            InitializeComponent();
            dodajCombo();
            this.admin = admin;
            this.status = status;
            if (status == EStatus.IZMENI)//promeni natpise na dugmetu Dodaj i natpis forme
            {
                btnDodaj.Content = "Izmeni";
                this.Title = "Izmena podataka o adminu";
                tbJmbg.IsEnabled = false;
            }

            tbIme.DataContext = admin;  //Text="{Binding Ime}"  stavljeno u xaml kodu 
            tbPrezime.DataContext = admin;
            tbJmbg.DataContext = admin;
            cbPol.DataContext = admin;
            tbEmail.DataContext = admin;
            tbLozinka.DataContext = admin;

            tbUlica.DataContext = admin.Adresa;
            tbBroj.DataContext = admin.Adresa;
            tbGrad.DataContext = admin.Adresa;
            cbDrzava.DataContext = admin.Adresa;  //data context zbog data bindinga. Ako se forma otvori za izmenu polja ce se popuniti atributima prosledjenog objekta

            
        }
        void dodajCombo()
        {

            cbPol.Items.Add("M");
            cbPol.Items.Add("Z");
            cbPol.SelectedIndex = 0; //za combo pol popuni

            cbDrzava.Items.Add("Srbija");
            cbDrzava.Items.Add("Bosna i Hercegovina");
            cbDrzava.Items.Add("Crna Gora");
            cbDrzava.Items.Add("Grcka"); //za combo drzave
            cbDrzava.SelectedIndex = 0;
        }

        bool validanUnos()
        {
            if(tbIme.Text=="" || tbPrezime.Text=="" || tbJmbg.Text=="" || tbEmail.Text=="" || tbLozinka.Text=="" || tbEmail.Text=="" || 
                tbUlica.Text=="" || tbBroj.Text=="" || tbGrad.Text == "")
            {
                MessageBox.Show("Niste uneli sve podatke!");
                return false;
            }
            if (!tbEmail.Text.Contains("@") || !tbEmail.Text.EndsWith(".com"))
            {
                MessageBox.Show("Unet email nije u ispravnom formatu!");
                return false;
            }

            if (
                this.status==EStatus.DODAJ && 
                (
                    Podaci.Instanca.lstAdmini.Any(a=>a.Jmbg==tbJmbg.Text) ||
                    Podaci.Instanca.lstInstruktori.Any(ins => ins.Jmbg == tbJmbg.Text) ||
                    Podaci.Instanca.lstPolaznici.Any(p => p.Jmbg == tbJmbg.Text) 
                )
               )
            {
                MessageBox.Show("Uneli ste postojeci jmbg!");
                return false;
            }

            return true;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (validanUnos())
            {
                if (status == EStatus.DODAJ)//ako je forma u modu za dodavanje
                {
                    this.admin.TipKorisnika = ETipKorisnika.ADMINISTRATOR;//setuje ovo polje zato sto se ne unosi
                    int idAdrese = 1;
                    if (Podaci.Instanca.lstAdrese.Count > 0)
                        idAdrese = Podaci.Instanca.lstAdrese.Max(adr => adr.Id) + 1;//generise novi id adrese, max id adrese uvecan za 1. 
                    this.admin.Adresa.Id = idAdrese;
                    this.admin.ImePrezime = this.admin.Ime + " " + this.admin.Prezime;//ovo polje postoji u adminu i sluzi za prikaz u data gridu

                    Podaci.Instanca.lstAdmini.Add(admin);
                    Podaci.Instanca.lstAdrese.Add(admin.Adresa);//dodavanje u liste. Postoji posebna lista adresa 


                    AzuriranjeBaze<Object>.insertUpdateDelete_Baza("insert into Adresa values(" +
                                                                       admin.Adresa.Id + ", '" +
                                                                       admin.Adresa.Ulica + "', '" +
                                                                       admin.Adresa.Broj + "', '" +
                                                                       admin.Adresa.Grad + "', '" +
                                                                       admin.Adresa.Drzava + "', '" +
                                                                       admin.Adresa.obrisano + "'" +
                                                                       ");");

                    AzuriranjeBaze<Object>.insertUpdateDelete_Baza("insert into Korisnik values('" +
                                                                        admin.Ime + "', '" +
                                                                        admin.Prezime + "', '" +
                                                                        admin.Jmbg + "', '" +
                                                                        admin.Pol + "', " +
                                                                        admin.Adresa.Id + ", '" +
                                                                        admin.Email + "', '" +
                                                                        admin.Lozinka + "', '" +
                                                                        admin.TipKorisnika + "', '" +
                                                                        admin.obrisano + "', null" +
                                                                        ");");
                   


                    //AdminiServis adminiServis = new AdminiServis();
                    //adminiServis.upisFajla(Podaci.Instanca.lstAdmini);
                    //AdreseServis adrServis = new AdreseServis();
                    //adrServis.upisFajla(Podaci.Instanca.lstAdrese); //azuriranje fajlova za admine i adrese, prebrisu se podaci

                }
                else if (status == EStatus.IZMENI)
                {
                    //if(Podaci.Instanca.tipPrijavljen==ETipKorisnika.ADMINISTRATOR)//ako je prijavljen admin i prikazuje se njegov profil
                    //    Podaci.Instanca.jmbgPrijavljen = admin.Jmbg; //ukoliko je promenjen jmbg promeniti ga u klasi Podaci za prijavljenog admina

                    this.admin.ImePrezime = this.admin.Ime + " " + this.admin.Prezime;//ImePrezime je polje za prikaz u data gridu

                    Adresa a = Podaci.Instanca.lstAdrese.Where(adr => adr.Id == admin.Adresa.Id).FirstOrDefault();//prvo naci adresu iz liste adresa na osnovu id
                    int indexAdrese = Podaci.Instanca.lstAdrese.IndexOf(a);
                    Podaci.Instanca.lstAdrese[indexAdrese] = new Adresa(admin.Adresa);//na indeksu te adrese dodeliti modifikovanu adresu


                    AzuriranjeBaze<Object>.insertUpdateDelete_Baza("update Adresa set " +
                                                                       "ulica='" + admin.Adresa.Ulica + "', " +
                                                                       "broj='" + admin.Adresa.Broj + "', " +
                                                                       "grad='" + admin.Adresa.Grad + "', " +
                                                                       "drzava='" + admin.Adresa.Drzava + "' " +
                                                                       "where id=" + admin.Adresa.Id + ";");

                    AzuriranjeBaze<Object>.insertUpdateDelete_Baza("update Korisnik set " +
                                                                       "ime='" + admin.Ime + "', " +
                                                                       "prezime='" + admin.Prezime + "', " +
                                                                       "pol='" + admin.Pol + "', " +
                                                                       "email='" + admin.Email + "', " +
                                                                       "lozinka='" + admin.Lozinka + "' " +
                                                                       "where jmbg=" + admin.Jmbg + ";");



                    //AdminiServis adminiServis = new AdminiServis();
                    //adminiServis.upisFajla(Podaci.Instanca.lstAdmini);
                    //AdreseServis adrServis = new AdreseServis();
                    //adrServis.upisFajla(Podaci.Instanca.lstAdrese);//overwrite podataka u fajlovima
                }

                DialogResult = true;//indikator formi koja je otvorila ovu formu da se desilo dodavanje/izmena
            }
        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
