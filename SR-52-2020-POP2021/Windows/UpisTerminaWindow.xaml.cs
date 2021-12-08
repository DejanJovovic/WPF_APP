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
    /// Interaction logic for UpisTerminaWindow.xaml
    /// </summary>
    public partial class UpisTerminaWindow : Window
    {
        DateTime datumTreninga;
        Instruktor instruktor;
        public UpisTerminaWindow(DateTime datumTreninga, Instruktor instruktor)
        {
            InitializeComponent();
            this.datumTreninga = datumTreninga;
            this.instruktor = instruktor;

            lbDatumTermina.Content = "Datum treninga: " + datumTreninga.Date.Day + "." + datumTreninga.Date.Month + "." + datumTreninga.Date.Year + ".";
            inicijalizujComboVremena();
        }

        void inicijalizujComboVremena()
        {
            for (int i = 7; i <= 20; i++)
                cbH.Items.Add(i.ToString());
            cbH.SelectedIndex = 3;

            //cbMin.Items.Add("0");
            //cbMin.Items.Add("15");
            //cbMin.Items.Add("30");
            //cbMin.Items.Add("45");
            for (int i = 0; i < 60; i++)
                cbMin.Items.Add(i.ToString());
            cbMin.SelectedIndex = 0;

            cbTrajanje.Items.Add("30");
            cbTrajanje.Items.Add("45");
            cbTrajanje.Items.Add("60");
            cbTrajanje.Items.Add("90");
            cbTrajanje.SelectedIndex = 1;

        }

        string validno()
        {
            string greske = "Greske:";
            return greske;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            
            if (validno() == "Greske:")
            {
                int id = 1;
                if(Podaci.Instanca.lstTreninzi.Count>0)
                    id=Podaci.Instanca.lstTreninzi.Max(t => t.Id) + 1;//generise novi id treninga

                DateTime datumTreninga = this.datumTreninga;
                string vremePocetka = cbH.SelectedItem.ToString() + ":" + cbMin.SelectedItem.ToString();
                int trajanjeTreninga = int.Parse(cbTrajanje.SelectedItem.ToString());
                bool slobodan = true;
                Instruktor instruktor=new Instruktor(this.instruktor);
                Polaznik polaznik=null;

                TimeSpan ts = new TimeSpan(int.Parse(cbH.SelectedItem.ToString()), int.Parse(cbMin.SelectedItem.ToString()), 0);
                datumTreninga = datumTreninga.Date + ts; //dodato vreme u datum treninga, preuzeto s combo boxova za H i Min

                bool postojiPreklapanje = false;
                foreach(Trening t in Podaci.Instanca.lstTreninzi)
                {
                    if(
                        t.Instruktor.Korisnik.Jmbg==instruktor.Korisnik.Jmbg && 
                        t.DatumTreninga.Date.Day==datumTreninga.Date.Day &&
                        t.DatumTreninga.Date.Month == datumTreninga.Date.Month &&
                        t.DatumTreninga.Date.Year == datumTreninga.Date.Year &&
                        t.obrisano==false
                        ) //prvo pronaci sve neobrisane treninge ovog instruktora za odabrani datum
                    {
                        DateTime datTek_TrajanjeDo = t.DatumTreninga.AddMinutes(t.TrajanjeTreninga);//iz tekuceg treninga odrediti dokle traje, DatumTreninga sadrzi prethodno setovano vreme

                        DateTime dat_TrajanjeDo = datumTreninga.AddMinutes(trajanjeTreninga);//odrediti dokle bi trajao trening koji se upravo upisuje

                        //sada proveriti da li postoje neka preklapanja, ako da, setovati indikator postojiPreklapanje na true
                        if(
                            (datumTreninga>t.DatumTreninga.AddMinutes(-1) && datumTreninga<datTek_TrajanjeDo.AddMinutes(1)) ||
                            (dat_TrajanjeDo > t.DatumTreninga.AddMinutes(-1) && dat_TrajanjeDo < datTek_TrajanjeDo.AddMinutes(1))
                            )
                        {
                            postojiPreklapanje = true;
                            break;
                        }
                    }
                }

                if (postojiPreklapanje)
                {
                    MessageBox.Show("Ne mozete uneti termin treninga zato sto postoji preklapanje s postojecim terminom!");
                }
                else
                {

                    Trening noviTrening = new Trening(id, datumTreninga, vremePocetka, trajanjeTreninga, slobodan, instruktor, polaznik);
                    noviTrening.obrisano = false;
                    Podaci.Instanca.lstTreninzi.Add(noviTrening);

                    TreninziServis trenServis = new TreninziServis();
                    trenServis.upisFajla(Podaci.Instanca.lstTreninzi);//upis u fajl


                    DialogResult = true;
                }

            }
            else
            {
                MessageBox.Show(validno());
            }

          
        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

      
    }
}
