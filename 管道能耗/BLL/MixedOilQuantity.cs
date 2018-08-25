using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    //混油量计算类
  public class MixedOilQuantity
    {
        //A油品的运动粘度
        private double Va;
        //B油品的运动粘度
        private double Vb;
        //管线长度
        private double L;
        //管线内径
        private double d;
        //流速
        private double v;
        //混油浓度
        private int Ka;
        //平均粘度
        private double νpj;
        //雷诺数
        private double Re;

        public MixedOilQuantity(Oils oilA, Oils oilB, Pipe pipe, double 切割浓度, double 流速)
        {
            Va = double.Parse(oilA.Viscosity);
            Vb = double.Parse(oilB.Viscosity);
            L = double.Parse(pipe.Length);
            d = double.Parse(pipe.OuterDiameter) - 2 * double.Parse(pipe.WallThickness);
            v = 流速;
            Ka = (int)(切割浓度 * 100);
            νpj = (Va + Vb) / 2;
            Re = v * d / 1000 / νpj;
        }

        /**
         * 返回计算结果
         * @return double类型计算结果
         **/
        public double getResult()
        {
            double result = 0;
            //分子
            double r1 = νpj * (3000 + 60.7 * Math.Pow(Re, 0.545));
            //分母
            double r2 = L * v * 1000;
            //4VgZ
            double l1 = 4 * Vg() * Z();
            double t = r1 / r2;
            //计算结果
            result = l1 * Math.Pow(r1 / r2, 0.5);

            return Math.Round(result, 4);
        }

        /**
         *计算管道总体积 
         * @return 计算结果
         **/
        public double Vg()
        {
            double result;
            result = 3.14 * (d * d / 1000) * L / 4;
            return result;
        }

        /**
         * 查表返回Z值      
         * @return 返回Z值
         **/
        private double Z()
        {
            double result = 0;
            switch (Ka)
            {
                case 99:
                    result = 1.645;
                    break;
                case 98:
                    result = 1.452;
                    break;
                case 97:
                    result = 1.330;
                    break;
                case 96:
                    result = 1.238;
                    break;
                case 95:
                    result = 1.163;
                    break;
                case 94:
                    result = 1.099;
                    break;
                case 93:
                    result = 1.044;
                    break;
                case 92:
                    result = 0.994;
                    break;
                case 91:
                    result = 0.948;
                    break;
                case 90:
                    result = 0.906;
                    break;
                case 85:
                    result = 0.733;
                    break;
                case 80:
                    result = 0.595;
                    break;
                case 75:
                    result = 0.477;
                    break;
                case 70:
                    result = 0.371;
                    break;
                case 65:
                    result = 0.272;
                    break;
                case 60:
                    result = 0.180;
                    break;
                case 55:
                    result = 0.089;
                    break;
                case 50:
                    result = 0;
                    break;
                case 45:
                    result = -0.089;
                    break;
                case 40:
                    result = -0.180;
                    break;
                case 35:
                    result = -0.272;
                    break;
                case 30:
                    result = -0.371;
                    break;
                case 25:
                    result = -0.477;
                    break;
                case 20:
                    result = -0.595;
                    break;
                case 15:
                    result = -0.733;
                    break;
                case 10:
                    result = -0.906;
                    break;
                case 9:
                    result = -0.948;
                    break;
                case 8:
                    result = -0.994;
                    break;
                case 7:
                    result = -1.044;
                    break;
                case 6:
                    result = -1.099;
                    break;
                case 5:
                    result = -1.163;
                    break;
                case 4:
                    result = -1.238;
                    break;
                case 3:
                    result = -1.330;
                    break;
                case 2:
                    result = -1.452;
                    break;
                case 1:
                    result = -1.645;
                    break;
            }
            return result;
        }
    }
}
