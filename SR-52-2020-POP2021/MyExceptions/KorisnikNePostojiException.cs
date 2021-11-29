using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_52_2020_POP2021.MyExceptions
{
    public class KorisnikNePostojiException : Exception //svi nasledjuju exception klasu
    {
        public KorisnikNePostojiException() : base()  //ovako se implementira prazan konstruktor
        {

        }
        public KorisnikNePostojiException(string poruka) : base(poruka)//konstruktor sa parametrom
        {

        }

        public KorisnikNePostojiException(string poruka, Exception ex) : base(poruka, ex)
        {

        }
    }
}
