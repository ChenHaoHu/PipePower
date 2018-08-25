using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /**
         * 输油泵计算类 
         * 通过getResult()获取返回值
         **/
  public class PowerCost
    {
        //泵数
        private double N = 4;
        //当量粗糙度
        private double e;
        //重力加速度
        private double g;
        //高程差
        private double Z;
        //体积流量
        private double Q;
        //原油运动粘度
        private double ν;
        //管线长度
        private double L;
        //管线内径
        private double d;
        //电力价格
        private double ed;
        //泵运行时间
        private double tj;
        //电机效率
        private double ηee;
        //相对粗糙度
        private double ε;
        //流速
        private double v;
        //雷诺数
        private double Re;
        //输油泵的吸入压力
        private double Ppein;
        //输油泵的原油流量
        private double Gpe;
        //输油泵的输入功率
        private double Npe;
        //输油泵的吸入压力
        private double Ppeout;
        //年工作量
        private double D;
        //首站进站压力
        private double Hs1;
        //流量Q
        private double[] q;
        //扬程H
        private double[] H;

        public PowerCost(Oils oils, Pipe pipe, Pump pump, double[] 泵流量, double[] 泵扬程, double 年工作时间, double 电机效率, double 管道起点高程, double 管道终点高程, double 电费, double 流速, double 当量粗糙度, double 重力加速度, double 首站进站压力)
        {
            g = 重力加速度;
            D = 年工作时间;
            v = 流速;
            e = 当量粗糙度;
            g = 重力加速度;
            Hs1 = 首站进站压力;
            q = 泵流量;
            H = 泵扬程;
            Q = double.Parse(oils.OutputByYear) * 10000000 / 24 / 3600 / 350 / double.Parse(oils.Density);
            ν = double.Parse(oils.Viscosity);
            L = double.Parse(pipe.Length);
            d = double.Parse(pipe.OuterDiameter) - 2 * double.Parse(pipe.WallThickness);
            ed = 电费;
            tj = double.Parse(oils.OutputByYear) / (double.Parse(oils.MasFlow) / 350);
            ηee = 电机效率;
            Z = 管道终点高程 - 管道起点高程;
            Gpe = double.Parse(oils.OutputByYear) * 10000000 / D / 24 / 3600;
            Ppeout = double.Parse(pump.OutPressure);
            Ppein = double.Parse(pump.InPressure);
            Npe = double.Parse(pump.Power);
            ε = 2 * e / d;
            Re = v * d / 1000 / ν;
        }

        /**
         * 获取结果 
         * @return double类型结果
         **/
        public double getResult()
        {
            double result;

            //分子
            double r1 = Hi() * Gpe * g * tj * 24;
            //分母
            double r2 = 1000 * ηpe() * ηee;
            result = ed * r1 / r2;
            return result;
        }

        /**
         * 计算沿程摩阻 
         * @return double类型沿程摩阻
         **/
        private double h()
        {
            double result = 0, β = 0, m = 1;
            //判断属于哪个流态
            if (Re < 2000)
            {
                β = 4.15;
                m = 1;
            }
            else if (3000 < Re && Re < (59.5 / Math.Pow(ε, 8.0 / 7)))
            {
                β = 0.0246;
                m = 0.25;
            }
            else if ((59.5 / Math.Pow(ε, 8.0 / 7)) < Re && Re < (665 - 765 * Math.Log10(ε)))
            {
                double A = Math.Pow(10, 0.127 * Math.Log10(ε / d) - 0.627);
                β = 0.0802 * A;
                m = 0.123;
            }
            else if ((665 - 765 * Math.Log10(ε)) < Re)
            {
                double A = 0.11 * Math.Pow(e / d, 0.25);
                β = 0.0826 * A;
                m = 0;
            }
            double r1 = Math.Pow(Q, 2 - m) * Math.Pow(ν, m);
            double r2 = Math.Pow(d / 1000, 5 - m);
            result = 1.01 * β * r1 / r2 * L * 1000;
            return result;
        }

        /**
         * 计算扬程
         * @return double类型的扬程值
         **/
        private double Hi()
        {
            double result = 0;

            result = N * a() + N * b() * Math.Pow(Q * 3600, 1.75);

            return result;
        }

        /**
         * 计算a
         * @return double类型a值
         **/
        private double a()
        {
            double result = 0;

            double[] x = new double[q.Length];
            double[] y = new double[q.Length];
            for (int i = 0; i < q.Length; i++)
            {
                x[i] = Math.Pow(q[i], 1.75);
                y[i] = H[i];
            }
            double tmp1 = 0, tmp2 = 0, tmp3 = 0, tmp4 = 0;
            for (int i = 0; i < x.Length; i++)
            {
                tmp1 += x[i] * y[i];
                tmp2 += x[i];
                tmp3 += y[i];
                tmp4 += x[i] * x[i];
            }
            double r1 = tmp1 * tmp2 - tmp3 * tmp4;
            double r2 = tmp2 * tmp2 - x.Length * tmp4;
            result = r1 / r2;

            return result;
        }

        /**
         * 计算b
         * @return double类型b值
         **/
        private double b()
        {
            double result = 0;

            double[] x = new double[q.Length];
            double[] y = new double[q.Length];
            for (int i = 0; i < q.Length; i++)
            {
                x[i] = Math.Pow(q[i], 1.75);
                y[i] = H[i];
            }
            double tmp1 = 0, tmp2 = 0, tmp3 = 0, tmp4 = 0;
            for (int i = 0; i < x.Length; i++)
            {
                tmp1 += x[i] * y[i];
                tmp2 += x[i];
                tmp3 += y[i];
                tmp4 += x[i] * x[i];
            }
            double r1 = tmp2 * tmp3 - x.Length * tmp1;
            double r2 = tmp2 * tmp2 - x.Length * tmp4;
            result = r1 / r2;

            return result;
        }

        /**
         * 计算泵效率
         * @return double类型的泵效率
         **/
        private double ηpe()
        {
            double result = 0;
            //分子
            double r1 = Ppeout * Gpe / 3.6;
            //分母
            double r2 = Npe + Ppein * Gpe / 3.6;
            //结果
            result = r1 / r2;
            return result;
        }
    }
}

