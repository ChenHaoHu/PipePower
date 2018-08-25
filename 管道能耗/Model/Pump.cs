using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Pump
    {
        string name;
        int pumpId;
        string head;
        string displacement;//泵排量
        string power;//质量
        string suctionPressure;
        string runTime;
        string inPressure;
        string outPressure;

        public Pump(string name, int pumpId, string head, string displacement, string power, string suctionPressure, string runTime, string inPressure, string outPressure)
        {
            this.name = name;
            this.pumpId = pumpId;
            this.head = head;
            this.displacement = displacement;
            this.power = power;
            this.suctionPressure = suctionPressure;
            this.runTime = runTime;
            this.inPressure = inPressure;
            this.outPressure = outPressure;
        }

        public string Name { get => name; set => name = value; }
        public int PumpId { get => pumpId; set => pumpId = value; }
        public string Head { get => head; set => head = value; }
        public string Displacement { get => displacement; set => displacement = value; }
        public string Power { get => power; set => power = value; }
        public string SuctionPressure { get => suctionPressure; set => suctionPressure = value; }
        public string RunTime { get => runTime; set => runTime = value; }
        public string InPressure { get => inPressure; set => inPressure = value; }
        public string OutPressure { get => outPressure; set => outPressure = value; }
    }
}
