using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public  class  Oils
    {
        int oilsId;//编号
        string name;//名字
        string density;//密度
        string viscosity;//油品运动粘度
        string masFlow;//质量流量Q
        string outputByYear;//年输量
        string volume_concentration;//体积浓度

        public Oils()
        {
        }

        public Oils(int oilsId, string name, string density, string viscosity, string masFlow, string outputByYear, string volume_concentration)
        {
            this.oilsId = oilsId;
            this.name = name;
            this.density = density;
            this.viscosity = viscosity;
            this.masFlow = masFlow;
            this.outputByYear = outputByYear;
            this.volume_concentration = volume_concentration;
        }

        public int OilsId { get => oilsId; set => oilsId = value; }
        public string Name { get => name; set => name = value; }
        public string Density { get => density; set => density = value; }
        public string Viscosity { get => viscosity; set => viscosity = value; }
        public string MasFlow { get => masFlow; set => masFlow = value; }
        public string OutputByYear { get => outputByYear; set => outputByYear = value; }
        public string Volume_concentration { get => volume_concentration; set => volume_concentration = value; }
    }
}
