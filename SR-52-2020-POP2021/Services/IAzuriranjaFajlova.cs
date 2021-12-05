using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Services
{
    public interface IAzuriranjaFajlova<T>   //genericki interfejs, primenjivace se na sve tipove entiteta
    {
        void upisFajla(ObservableCollection<T> lista); //za svaki upis/izmenu/brisanje prebrisu se svi podaci u odgovarajucem fajlu
        ObservableCollection<T> citanjeFajla();//observabilna kolekcija za odredjeni tip, cita iz fajla u listu u klasi Podaci
    }
}
