using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    //管道工作特性
   public class PipeWork
    {
        //全线泵数量
        private double N = 4;
        //流量Q组
        private double[] q;
        //扬程H组
        private double[] H;
        //管道终点高程
        private double Zz;
        //管道起点高程
        private double Zq;
        //年输量
        private double Ma;
        //密度
        private double ρ;
        //管道长度
        private double L;
        //当量粗糙度
        private double e;
        //粘度
        private double ν;
        //流速
        private double v;
        //管道内径
        private double d;
        //出站压力
        private double outPressure;
        //高程差
        private double Z;
        //流量
        private double Q;
        //雷诺数
        private double Re;
        //相对粗糙度
        private double ε;
        //首站进站压力
        private double Hs;

        public PipeWork(Oils oil, Pipe pipe, Pump pump, double[] 泵流量, double[] 泵扬程, double 当量粗糙度, double 流速, double 管道起点高程, double 管道终点高程, double 首站进站压力)
        {
            Ma = double.Parse(oil.OutputByYear);
            q = 泵流量;
            H = 泵扬程;
            ρ = double.Parse(oil.Density);
            L = double.Parse(pipe.Length);
            e = 当量粗糙度;
            ν = double.Parse(oil.Viscosity);
            v = 流速;
            d = double.Parse(pipe.OuterDiameter) - 2 * double.Parse(pipe.WallThickness);
            outPressure = double.Parse(pump.OutPressure);
            Zz = 管道终点高程;
            Zq = 管道起点高程;
            Z = 管道终点高程 - 管道起点高程;
            Hs = 首站进站压力;
            Q = (Ma * 10000000 / 24 / 350 / 3600) / ρ;
            Re = v * d / 1000 / ν;
            ε = 2 * e / d;
        }

        /**
         * 获取结果
         * @out 系统工作流量
         * @out 扬程
         **/
        public void getResult(out double 系统工作流量, out double 扬程, out double A, out double B)
        {
            系统工作流量 = Math.Round(getSystemWorkFlow(), 4);
            扬程 = Math.Round(getHead(), 4);
            A = Math.Round(N * a(), 4);
            B = Math.Round(N * b(), 4);
        }

        /**
         * 获取系统工作流量
         * @return double类型 系统工作流量
         **/
        public double getSystemWorkFlow()
        {
            double result = 0;

            double A = N * a();
            double B = N * b();
            double hc = 0.01 * h();
            double r1 = Hs + 1 * A - Z - 1 * hc - outPressure;
            double r2 = 1 * Math.Abs(B) + f() * L * 1000;
            double m, β;
            flowArea(out β, out m);
            result = Math.Pow(r1 / r2, 1 / (2 - m));

            return result;
        }

        /**
        * 计算扬程
        * @return double类型的扬程值
        **/
        public double getHead()
        {
            double result = 0;

            result = N * a() + N * b() * Math.Pow(Q * 3600, 1.75);

            return result;
        }

        /**
         * 计算沿程摩阻 
         * @return double类型沿程摩阻
         **/
        private double h()
        {
            double result = 0;

            double β = 0, m = 0;
            flowArea(out β, out m);
            double r1 = Math.Pow(Q, 2 - m) * Math.Pow(ν, m);
            double r2 = Math.Pow(d / 1000, 5 - m);
            result = 1.01 * β * r1 / r2 * L * 1000;

            return result;
        }

        /**
         * 判断流态
         * @out β
         * @out m
         **/
        public void flowArea(out double β, out double m)
        {
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
            else
            {
                double A = 0.11 * Math.Pow(e / d, 0.25);
                β = 0.0826 * A;
                m = 0;
            }
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
         * 计算单位流量的水力坡降 
         * @return double类型单位流量的水力坡降 
         **/
        private double f()
        {
            double result = 0;

            double β = 0, m = 1;
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
            double r1 = Math.Pow(ν, m);
            double r2 = Math.Pow(d / 1000, 5 - m);
            result = β * r1 / r2;

            return result;
        }
    }
}
