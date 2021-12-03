using SR_52_2020_POP2021.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Services
{
    public class FitnesCentriServis : IAzuriranjaFajlova<FitnesCentar>
    {
        public ObservableCollection<FitnesCentar> citanjeFajla()
        {
            return null;
        }

        public void upisFajla(ObservableCollection<FitnesCentar> lista)
        {
            throw new NotImplementedException();
        }
    }
}
