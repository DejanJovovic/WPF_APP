using SR_52_2020_POP2021.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Services
{
    public interface IInstruktorServis
    {

        void SaveUsers(string filename);
        void ReadUsers(string filename);
        List<Korisnik> FindallClients(String email);
    }
}
