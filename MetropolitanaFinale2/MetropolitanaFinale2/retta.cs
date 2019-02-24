using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetropolitanaFinale2
{
    public class retta
    {
        public int m, q;
        public bool parallelaY;
        public int ValoreX;

        public retta(int m, int q)
        {
            this.m = m;
            this.q = q;
            parallelaY = false;
        }
        public retta(int valX)
        {
            ValoreX = valX;
            parallelaY = true;
        }

        public int calcola(int x)
        {
            if (parallelaY)
                return 30000;
            return m * x + q;
        }
    }
}
