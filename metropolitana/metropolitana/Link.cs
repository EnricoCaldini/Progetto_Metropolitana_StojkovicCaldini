using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace metropolitana
{
    public class Link
    {
        public Stazione prtnz;
        public Stazione rrv;
        public int dstnz;
        public int offset;

        public Link(Stazione prt, Stazione arr, int distanza)
        {
            prtnz = prt;
            rrv = arr;
            dstnz = distanza;
        }
    }
}
