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
    /// Interaction logic for FitnesCentriWindow.xaml
    /// </summary>
    public partial class FitnesCentriWindow : Window
    {
        ICollectionView viewFitnesCentri;
        public FitnesCentriWindow()
        {
            InitializeComponent();
            osveziDGFitnesCentri();
        }

        void osveziDGFitnesCentri()
        {

            viewFitnesCentri = CollectionViewSource.GetDefaultView(Podaci.Instanca.lstFitnesCentri.Where(ins => ins.obrisano == false).ToList());//log brisanje, neobrisani svi
            dgFitnesCentri.ItemsSource = viewFitnesCentri;
            dgFitnesCentri.IsSynchronizedWithCurrentItem = true;
            dgFitnesCentri.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            FitnesCentar fc = new FitnesCentar();//novi fitnes centar, prosledice se formi u modu za upis gde ce se dodeliti vrednosti iz text boxova i dodati u listu i u fajl/bazu
            fc.Adresa.Drzava = "Srbija";

            FitnesCentarUpisIzmena_Window ra = new FitnesCentarUpisIzmena_Window(fc);//prosledjen fitnes centar, forma podrazumevano u modu za dodavanje
            ra.ShowDialog();
            if (ra.DialogResult == true)//ako je potvrdjeno dodavanje u otvorenoj formi
            {
                osveziDGFitnesCentri();//refresh podataka nakon dodavanja u datagridu
            }
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            FitnesCentar selektovanFC = viewFitnesCentri.CurrentItem as FitnesCentar; //napravi objekat od selektovanog reda iz datagrid tabele
            if (selektovanFC != null)//ako je selektovan red ima smisla praviti izmenu. 
            {
                FitnesCentar copyFC = new FitnesCentar(selektovanFC);//pravi se kopija objekta za slucaj da na otvorenoj formi nije potvrdjena izmena.
                                                                   //zbog data bindinga podaci se svakako promene u objektu-ako su promene u poljima napravljene, iako nije potvrdjena izmena


                FitnesCentarUpisIzmena_Window FCIzmenaWindow = new FitnesCentarUpisIzmena_Window(selektovanFC, EStatus.IZMENI);//prosledjuje se selektovani objekat formi i tamo 
                                                                                                            //ce se po data bindingu sva polja biti popunjena podacima objekta. Forma je u modu izmene
                FCIzmenaWindow.ShowDialog();

                if (FCIzmenaWindow.DialogResult != true)//ako nije potvrdjena izmena pronaci objekat i vratiti kopiju sacuvano pre otvaranja forme za izmenu
                {

                    int index = Podaci.Instanca.lstFitnesCentri.IndexOf(selektovanFC);//indeks elementa koji se mozda promenio u otvorenoj formi
                    Podaci.Instanca.lstFitnesCentri[index] = copyFC;//vratiti kopiju na taj indeks
                }

            }

            osveziDGFitnesCentri();//refresh datagrida nakon izmene
        }

        private void btnBrisi_Click(object sender, RoutedEventArgs e)
        {
            FitnesCentar selektovanFC = viewFitnesCentri.CurrentItem as FitnesCentar;//napravljen objekat od selektovanog reda datagrida
            PotvrdaBrisanjaWindow pb = new PotvrdaBrisanjaWindow();//otvara formu za potvrdu brisanja. Dialog result je true ako se tamo klikne na Da
            pb.ShowDialog();
            if (pb.DialogResult == true)//ako je kliknuto na Da u formi za potvrdu brisanja
            {
                selektovanFC.obrisano = true;//logicko brisanje, prikazace se oni kojima je obrisano false

                AzuriranjeBaze<Object>.insertUpdateDelete_Baza("update FitnesCentar set " +
                                                                   "obrisano='" + true + "' " +
                                                                   "where id=" + selektovanFC.Id + ";");
                //FitnesCentriServis fcServis = new FitnesCentriServis();
                //fcServis.upisFajla(Podaci.Instanca.lstFitnesCentri);//azuriraj fajl, prebrise sve podatke u fajlu. Dodavanjem baze pozivace se metoda kojoj ce se proslediti sql naredba

                osveziDGFitnesCentri();//refresh nakon brisanja. Prikazuju se samo podaci kojima je obrisan na false
            }
        }
    }
}
