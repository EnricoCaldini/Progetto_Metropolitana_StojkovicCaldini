using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace metropolitana
{
    public class Stazione
    {
        public int x;
        public int y;
        public string nome;
        public object pic;
        public object lab;
        public bool selected = false;

        public Stazione(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
