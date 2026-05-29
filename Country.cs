using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PR17_romanov
{
    class Country
    {
        private int population;
        private string name;

        public int Population
        {
            get { return population; }
            set { population = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Country(string Name, int Population)
        {
            this.Name = Name;
            this.Population = Population;
        }

        
    }

}
