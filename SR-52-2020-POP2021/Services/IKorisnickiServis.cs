using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.Services
{
    public interface IKorisnickiServis  // interfejs gledamo kao popis svih metoda
    {
        void SaveUsers(string filename);
        void ReadUsers(string filename);
        void DeleteUser(string email);
    }

}
