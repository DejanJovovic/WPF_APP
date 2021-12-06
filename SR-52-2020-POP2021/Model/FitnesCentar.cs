using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Model
{
    public class FitnesCentar : INotifyPropertyChanged
    {
        int id;
        string naziv;
        Adresa adresa;
        public bool obrisano = false;//logicko brisanje

        public FitnesCentar(int id, string naziv, Adresa adresa)
        {
            this.Id = id;
            this.Naziv = naziv;
            this.Adresa = adresa;
        }
        public FitnesCentar()
        {
            this.Adresa = new Adresa();
        }
        public FitnesCentar(FitnesCentar fc)
        {
            this.Id = fc.Id;
            this.Naziv = fc.Naziv;

            this.Adresa = new Adresa();
            this.Adresa.Id = fc.Adresa.Id;
            this.Adresa.Ulica = fc.Adresa.Ulica;
            this.Adresa.Broj = fc.Adresa.Broj;
            this.Adresa.Grad = fc.Adresa.Grad;
            this.Adresa.Drzava = fc.Adresa.Drzava;

            this.obrisano = fc.obrisano;
        }

        //public int Id { get => id; set => id = value; }
        //public string Naziv { get => naziv; set => naziv = value; }
        //public Adresa Adresa { get => adresa; set => adresa = value; }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Naziv
        {
            get
            {
                return naziv;
            }
            set
            {
                naziv = value;
                OnPropertyChanged("Naziv");
            }
        }

        public Adresa Adresa
        {
            get
            {
                return adresa;
            }
            set
            {
                adresa = value;
                OnPropertyChanged("Adresa");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public override string ToString()
        {
            return Id + " " + Naziv + " " + Adresa;
        }
    }
}
