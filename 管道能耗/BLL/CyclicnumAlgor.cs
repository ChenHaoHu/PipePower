using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    //最优循环次数计算类
   public class CyclicnumAlgor
    {
        //投资年回收系数
        private double E;
        //单位有效容积储罐的建设费用
        private double JZ;
        //单位有效容积储罐的经营费用
        private double G;
        //A油价格
        private double aPrice;
        //B油价格
        private double bPrice;
        //年管道输量
        private double[] M;
        //管道输量
        private double Qm;
        //储罐有效容积
        private double v;
        //A油品的密度
        private double ρA;
        //B油品的密度
        private double ρB;
        //B混油浓度
        private double kAPB;
        //A混油浓度
        private double kBPA;
        //混入A油罐中B体积量
        private double vPB;
        //混入B油罐中A体积量
        private double vPA;
        //管道长度
        private double L;
        //A油品运动粘度
        private double Va;
        //B油品运动粘度
        private double Vb;
        //管线内径
        private double d;
        //雷诺数
        private double Re;
        //输油管每年工作时间
        private double D;

        public CyclicnumAlgor(Oils oilA, Oils oilB, Pipe pipe, double A油品费用, double B油品费用, double 混入B油罐中A体积量, double 混入A油罐中B体积量, double 流速, double 年工作时间, double 单位有效容积储罐的经营费用, double 单位有效容积储罐的建设费用, double 投资年回收系数)
        {
            aPrice = A油品费用;
            bPrice = B油品费用;
            JZ = 单位有效容积储罐的建设费用;
            E = 投资年回收系数;
            G = 单位有效容积储罐的经营费用;
            D = 年工作时间;
            Qm = double.Parse(oilA.MasFlow) / D;
            M = new double[]
            {
                double.Parse(oilA.OutputByYear),
                double.Parse(oilB.OutputByYear)
            };
            v = 流速;
            ρA = double.Parse(oilA.Density);
            ρB = double.Parse(oilB.Density);
            kAPB = double.Parse(oilB.Volume_concentration);
            kBPA = double.Parse(oilA.Volume_concentration);
            vPB = 混入A油罐中B体积量;
            vPA = 混入B油罐中A体积量;
            d = double.Parse(pipe.OuterDiameter) - 2 * double.Parse(pipe.WallThickness);
            L = double.Parse(pipe.Length);

            Va = double.Parse(oilA.Viscosity);
            Vb = double.Parse(oilB.Viscosity);
            Re = v * d / 1000 / ((Va + Vb) / 2);
        }

        /**
         * 返回计算结果
         * @return 结果
         **/
        public double getResult()
        {
            double result = 0;

            //分子
            double r1 = B() * (JZ * E + G);
            //分母
            double r2 = A() + (JZ * E + G) * vH_vS();
            //结果
            result = Math.Pow(r1 / r2, 0.5);

            return Math.Ceiling(result);
        }

        /**
         *计算B值 
         * @return B的计算值
         **/
        private double B()
        {
            double result = 0;

            double q = 0, dp = 0;
            q = (M[0] * 10000000 / 350 / 24 / 3600 / ρA) * 3600;
            dp = M[0] / Qm;
            result += (q * (D - dp)) * 2;

            q = (M[1] * 10000000 / 350 / 24 / 3600 / ρB) * 3600;
            dp = M[1] / Qm;
            result += (q * (D - dp)) * 2;

            return result;
        }

        /**
         *计算A值 
         * @return A的计算值
         **/
        private double A()
        {
            double result = 0;

            double detaS = aPrice - bPrice;
            if (detaS < 0)
            {
                detaS = -detaS;
            }
            result = 2 * detaS * (ρB * kAPB * vPB / 1000 - ρA * kBPA * vPA / 1000);

            return result;
        }

        /**
         * 计算Vh-Vs的值
         * @return 返回Vh-Vs的值
         **/
        private double vH_vS()
        {
            double result = 0;

            result = 2.4 * C() * 3.14 * Math.Pow(d / 1000, 2) / 4;

            return result;
        }

        /**
         * 计算C值 
         * @return 返回C的计算值
         **/
        private double C()
        {
            double result = 0;

            double Rej = 10000 * Math.Exp(2.72 * Math.Pow(d / 1000, 0.5));
            if (Re > Rej)
            {
                result = 18384 * Math.Pow(d / 1000, 0.5) * Math.Pow(L * 1000, 0.5) * Math.Pow(Re, -0.9) * Math.Exp(2.18 * Math.Pow(d / 1000, 0.5));
            }
            else
            {
                result = 11.75 * Math.Pow(d / 1000, 0.5) * Math.Pow(L * 1000, 0.5) * Math.Pow(Re, -0.1);
            }

            return result;
        }
    }
}
