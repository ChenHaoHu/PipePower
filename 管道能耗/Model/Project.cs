using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Project
    {
        int proid;
        string number;
        string name;
        string responsible;
        string buildDate;

        public Project()
        {
        }

        public Project(int proid, string number, string name, string responsible, string buildDate)
        {
            this.Proid = proid;
            this.Number = number;
            this.Name = name;
            this.Responsible = responsible;
            this.BuildDate = buildDate;
        }

        public int Proid { get => proid; set => proid = value; }
        public string Number { get => number; set => number = value; }
        public string Name { get => name; set => name = value; }
        public string Responsible { get => responsible; set => responsible = value; }
        public string BuildDate { get => buildDate; set => buildDate = value; }
    }
}
