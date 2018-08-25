using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    //水力特性分析
    public class WaterCharacteristics
    {
        //泵数
        private const double N = 4;
        //年输量
        private double Ma;
        //首站进站压力
        private double H1;
        //全线总摩阻
        private double hc;
        //流量Q
        private double[] q;
        //扬程H
        private double[] H;
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
        //高程差
        private double Z;
        //流量
        private double Q;
        //相对粗糙度
        private double ε;
        //雷诺数
        private double Re;


        public WaterCharacteristics(Oils oil, Pipe pipe, Pump pump, double 全线总摩阻, double[] 泵流量, double[] 泵扬程, double 当量粗糙度, double 流速, double 管道起点高程, double 管道终点高程)
        {
            Ma = double.Parse(oil.OutputByYear);
            H1 = double.Parse(pump.InPressure);
            hc = 全线总摩阻;
            q = 泵流量;
            H = 泵扬程;
            ρ = double.Parse(oil.Density);
            L = double.Parse(pipe.Length);
            e = 当量粗糙度;
            ν = double.Parse(oil.Viscosity);
            v = 流速;
            d = double.Parse(pipe.OuterDiameter) - 2 * double.Parse(pipe.WallThickness);
            Z = 管道终点高程 - 管道起点高程;
            Q = (double.Parse(oil.OutputByYear) * 10000000 / 350 / 24 / 3600) / ρ;
            ε = 2 * e / d;
            Re = v * d / 1000 / ν;
        }

        /**
         * 获取结果
         * @out 前站出站压力
         * @out 末站出站压力
         * @out 斜率
         * @out 沿程摩阻
         **/
        public void getResult(out double 首站出站压力, out double 末站出站压力, out double 斜率, out double 沿程摩阻)
        {
            首站出站压力 = Math.Round(getFirstPressure(), 4);
            末站出站压力 = Math.Round(getEndPressure(首站出站压力), 4);
            斜率 = Math.Round(getSlope(), 4);
            沿程摩阻 = Math.Round(getFrictionAlongPipe(), 4);
        }

        /**
         * 获取首站出站压力
         * @return double类型的前站出站压力
         **/
        private double getFirstPressure()
        {
            double result = 0;

            result = H1 + Hi() - 0.01 * h();

            return result;
        }

        /**
         * 获取末站出站压力
         * @return double类型末站出站压力
         **/
        private double getEndPressure(double He)
        {
            double result = 0;

            result = He - i() * L * 1000 - Z;

            return result;
        }

        /**
         * 计算沿程摩阻 
         * @return double类型沿程摩阻
         **/
        private double getFrictionAlongPipe()
        {
            double result = 0;

            double β = 0, m = 1;
            //判断属于哪个流态
            flowArea(out β, out m);
            //分子
            double r1 = Math.Pow(Q, 2 - m) * Math.Pow(ν, m);
            //分母
            double r2 = Math.Pow(d / 1000, 5 - m);
            result = 1.01 * β * r1 / r2 * L * 1000;

            return result;
        }

        /**
         * 计算斜率
         * @return double类型斜率
         **/
        private double getSlope()
        {
            double result = 0;

            result = i();

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
            //分子
            double r1 = tmp1 * tmp2 - tmp3 * tmp4;
            //分母
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
            //分子
            double r1 = tmp2 * tmp3 - x.Length * tmp1;
            //分母
            double r2 = tmp2 * tmp2 - x.Length * tmp4;
            result = r1 / r2;

            return result;
        }

        /**
         * 计算水力坡降 
         * @return double类型水力坡降
         **/
        private double i()
        {
            double result = 0;

            double β = 0, m = 1;
            //判断属于哪个流态
            flowArea(out β, out m);
            //分子
            double r1 = Math.Pow(Q, 2 - m) * Math.Pow(ν, m);
            //分母
            double r2 = Math.Pow(d / 1000, 5 - m);
            result = β * r1 / r2;

            return result;
        }

        /**
         * 总降压
         **/
        private double AllBuck()
        {
            double result = 0;

            double hl = h();
            double hm = hl * 0.01;
            result = hl + hm + Z;

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
    }
}
