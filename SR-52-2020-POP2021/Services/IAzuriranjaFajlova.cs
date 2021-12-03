using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Services
{
    public interface IAzuriranjaFajlova<T>
    {
        void upisFajla(ObservableCollection<T> lista);
        ObservableCollection<T> citanjeFajla();
    }
}
