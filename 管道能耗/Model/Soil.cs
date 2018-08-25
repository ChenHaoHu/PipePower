using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Soil
    {
        int soidId;
        string soilName;
        string soilTemp;
        string soilDiff;

        public Soil(int soidId, string soilName, string soilTemp, string soilDiff)
        {
            this.soidId = soidId;
            this.soilName = soilName;
            this.soilTemp = soilTemp;
            this.soilDiff = soilDiff;
        }

        public int SoidId { get => soidId; set => soidId = value; }
        public string SoilName { get => soilName; set => soilName = value; }
        public string SoilTemp { get => soilTemp; set => soilTemp = value; }
        public string SoilDiff { get => soilDiff; set => soilDiff = value; }
    }
}
