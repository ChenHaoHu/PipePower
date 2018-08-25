using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Other
    {
        //     other_id other_name cost_build n_recovery cost_operating cost_oil cost_electricity
        int otherId;
        string name;
        string buildCost;
        string recoveryN;
        string operatingCost;
        string oilCost;
        string electricityCost;

        public Other(int otherId, string name, string buildCost, string recoveryN, string operatingCost, string oilCost, string electricityCost)
        {
            this.otherId = otherId;
            this.name = name;
            this.buildCost = buildCost;
            this.recoveryN = recoveryN;
            this.operatingCost = operatingCost;
            this.oilCost = oilCost;
            this.electricityCost = electricityCost;
        }

        public int OtherId { get => otherId; set => otherId = value; }
        public string Name { get => name; set => name = value; }
        public string BuildCost { get => buildCost; set => buildCost = value; }
        public string RecoveryN { get => recoveryN; set => recoveryN = value; }
        public string OperatingCost { get => operatingCost; set => operatingCost = value; }
        public string OilCost { get => oilCost; set => oilCost = value; }
        public string ElectricityCost { get => electricityCost; set => electricityCost = value; }
    }
}
