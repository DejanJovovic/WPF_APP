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
    /// Interaction logic for FitnesCentarUpisIzmena_Window.xaml
    /// </summary>
    public partial class FitnesCentarUpisIzmena_Window : Window
    {
        FitnesCentar fitnesCentar;
        EStatus status;
        public FitnesCentarUpisIzmena_Window(FitnesCentar fitnesCentar, EStatus status = EStatus.DODAJ)
        {
            InitializeComponent();
            this.fitnesCentar = fitnesCentar;
            this.status = status;
            dodajCombo();

            if (status == EStatus.IZMENI)
            {
                btnDodaj.Content = "Izmeni";
                this.Title = "Izmena podataka o fitnes centru";

            }

            tbNaziv.DataContext = fitnesCentar;

            tbUlica.DataContext = fitnesCentar.Adresa;
            tbBroj.DataContext = fitnesCentar.Adresa;
            tbGrad.DataContext = fitnesCentar.Adresa;
            cbDrzava.DataContext = fitnesCentar.Adresa;



        }

        void dodajCombo()
        {

            cbDrzava.Items.Add("Srbija");
            cbDrzava.Items.Add("Bosna i Hercegovina");
            cbDrzava.Items.Add("Crna Gora");
            cbDrzava.Items.Add("Grcka");
            cbDrzava.SelectedIndex = 0;

        }

        bool validanUnos()
        {
            if (tbNaziv.Text == ""  || tbUlica.Text == "" || tbBroj.Text == "" || tbGrad.Text == "")
            {
                MessageBox.Show("Niste uneli sve podatke!");
                return false;
            }
           

            return true;
        }


        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (validanUnos())
            {
                if (status == EStatus.DODAJ)
                {
                    int idFC = 1;
                    if (Podaci.Instanca.lstFitnesCentri.Count > 0)
                        idFC = Podaci.Instanca.lstFitnesCentri.Max(fc => fc.Id) + 1;//generise novi id fitnes centra
                    this.fitnesCentar.Id = idFC;


                    this.fitnesCentar.Adresa.Id = Podaci.Instanca.lstAdrese.Max(adr => adr.Id) + 1;//generise novi id adrese

                    Podaci.Instanca.lstFitnesCentri.Add(fitnesCentar);
                    Podaci.Instanca.lstAdrese.Add(fitnesCentar.Adresa);

                    AzuriranjeBaze<Object>.insertUpdateDelete_Baza("insert into Adresa values(" +
                                                                       fitnesCentar.Adresa.Id + ", '" +
                                                                       fitnesCentar.Adresa.Ulica + "', '" +
                                                                       fitnesCentar.Adresa.Broj + "', '" +
                                                                       fitnesCentar.Adresa.Grad + "', '" +
                                                                       fitnesCentar.Adresa.Drzava + "', '" +
                                                                       fitnesCentar.Adresa.obrisano + "'" +
                                                                       ");");

                    AzuriranjeBaze<Object>.insertUpdateDelete_Baza("insert into FitnesCentar values(" +
                                                                        fitnesCentar.Id + ", '" +
                                                                        fitnesCentar.Naziv + "', " +
                                                                        fitnesCentar.Adresa.Id + ", '" +
                                                                        fitnesCentar.obrisano + "'" +
                                                                        ");");

                    //FitnesCentriServis fcServis = new FitnesCentriServis();
                    //fcServis.upisFajla(Podaci.Instanca.lstFitnesCentri);
                    //AdreseServis adrServis = new AdreseServis();
                    //adrServis.upisFajla(Podaci.Instanca.lstAdrese);

                }
                else if (status == EStatus.IZMENI)
                {


                    Adresa a = Podaci.Instanca.lstAdrese.Where(adr => adr.Id == fitnesCentar.Adresa.Id).FirstOrDefault();//prvo naci adresu iz liste adresa na osnovu id
                    int indexAdrese = Podaci.Instanca.lstAdrese.IndexOf(a);
                    Podaci.Instanca.lstAdrese[indexAdrese] = new Adresa(fitnesCentar.Adresa);//na indeksu te adrese dodeliti modifikovanu adresu

                    AzuriranjeBaze<Object>.insertUpdateDelete_Baza("update Adresa set " +
                                                                      "ulica='" + fitnesCentar.Adresa.Ulica + "', " +
                                                                      "broj='" + fitnesCentar.Adresa.Broj + "', " +
                                                                      "grad='" + fitnesCentar.Adresa.Grad + "', " +
                                                                      "drzava='" + fitnesCentar.Adresa.Drzava + "' " +
                                                                      "where id=" + fitnesCentar.Adresa.Id + ";");

                    AzuriranjeBaze<Object>.insertUpdateDelete_Baza("update FitnesCentar set " +
                                                                       "naziv='" + fitnesCentar.Naziv + "' " +
                                                                       "where id=" + fitnesCentar.Id + ";");

                    //FitnesCentriServis fcServis = new FitnesCentriServis();
                    //fcServis.upisFajla(Podaci.Instanca.lstFitnesCentri);
                    //AdreseServis adrServis = new AdreseServis();
                    //adrServis.upisFajla(Podaci.Instanca.lstAdrese);//overwrite podataka u fajlovima
                }

                DialogResult = true;
            }
        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
