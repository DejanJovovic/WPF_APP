using SR_52_2020_POP2021.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Services
{
    public class AdminiServis : IAzuriranjaFajlova<Korisnik>
    {
        public ObservableCollection<Korisnik> citanjeFajla()
        {
            return null;
        }

        public void upisFajla(ObservableCollection<Korisnik> lista)
        {
            throw new NotImplementedException();
        }
    }
}
