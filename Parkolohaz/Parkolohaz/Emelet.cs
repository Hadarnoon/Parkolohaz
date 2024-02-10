using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkolohaz
{
    internal class Emelet
    {
        public string Szintneve { get; set; }
        public List<int> szamadatok { get; set; }

        public Emelet(string sor)
        {
            var v = sor.Split(';');
            this.Szintneve = v[0];
            this.szamadatok = new List<int>();
            for (int i = 1; i < v.Length; i++)
            {
                this.szamadatok.Add(int.Parse(v[i]));
            }
        }

        public override string ToString()
        {
            return $"{Szintneve} {string.Join(" ", szamadatok)}";
        }
    }
}
