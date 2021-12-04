using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Model
{

    public class Adresa: INotifyPropertyChanged
    {

        int id;
        string ulica;
        string broj;
        string grad;
        string drzava;
        public bool obrisano = false;//logicko brisanje

        public Adresa(int id, string ulica, string broj, string grad, string drzava)
        {
            this.Id = id;
            this.Ulica = ulica;
            this.Broj = broj;
            this.Grad = grad;
            this.Drzava = drzava;
        }
        public Adresa() { }
        public Adresa(Adresa a)
        {
            this.Id = a.Id;
            this.Ulica = a.Ulica;
            this.Broj = a.Broj;
            this.Grad = a.Grad;
            this.Drzava = a.Drzava;

            this.obrisano = a.obrisano;
        }

        //public int Id { get => id; set => id = value; }
        //public string Ulica { get => ulica; set => ulica = value; }
        //public string Broj { get => broj; set => broj = value; }
        //public string Grad { get => grad; set => grad = value; }
        //public string Drzava { get => drzava; set => drzava = value; }


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
        public string Ulica
        {
            get
            {
                return ulica;
            }
            set
            {
                ulica = value;
                OnPropertyChanged("Ulica");
            }
        }
        public string Broj
        {
            get
            {
                return broj;
            }
            set
            {
                broj = value;
                OnPropertyChanged("Broj");
            }
        }

        public string Grad
        {
            get
            {
                return grad;
            }
            set
            {
                grad = value;
                OnPropertyChanged("Grad");
            }
        }

        public string Drzava
        {
            get
            {
                return drzava;
            }
            set
            {
                drzava = value;
                OnPropertyChanged("Drzava");
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
            return "Ulica: " + Ulica + " Broj: " + Broj + " Grad: " + Grad + " Drzava: " + Drzava;
        }
    }
}
