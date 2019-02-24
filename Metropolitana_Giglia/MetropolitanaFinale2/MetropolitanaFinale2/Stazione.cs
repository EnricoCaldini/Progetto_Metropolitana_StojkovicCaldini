using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetropolitanaFinale2
{
    public class Stazione : IEquatable<Stazione>
    {
        public int x;
        public int y;
        public string nome;
        public List<Link> linx = new List<Link>();
        public List<Point> listaPunti = new List<Point>();
        public object pic;
        public object lab;
        public bool selected = false;

        public Stazione(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Equals(Stazione other)
        {
            if (x == other.x && y == other.y && nome == other.nome)
                return true;
            return false;
        }
    }
}
