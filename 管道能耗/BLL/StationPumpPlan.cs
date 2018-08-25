using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class StationPumpPlan
    {
        //体积流量
        private double Q;
        //流量q
        private double[] q;
        //扬程y
        private double[] H;
        //高程差
        private double[] Z;
        //终点与起点的高程差
        private double Z_;
        //全线泵站数
        private double N;
        //站内摩阻
        private double hm;
        //管道内径
        private double d;
        //粘度
        private double ν;
        //管线长度
        private double L;
        //雷诺数
        private double Re;
        //相对粗糙度
        private double ε;
        //当量粗糙度
        private double e;
        //泵数
        private double n;
        //首站进站压力
        private double H1;
        //保存末站压力递归次数
        private int flag = 0;
        //保存高程差的值

        public StationPumpPlan(Oils oil, Pipe pipe, double[] 流量q, double[] 扬程H, double[] 管道高程, double 全线泵站数, double 当量粗糙度, double 流速, double 泵数, double 首站进站压力)
        {
            q = 流量q;
            H = 扬程H;
            Z = new double[管道高程.Length - 1];
            for (int i = 0; i < 管道高程.Length - 1; i++)
            {
                Z[i] = 管道高程[i + 1] - 管道高程[i];
            }
            Z_ = 管道高程[管道高程.Length - 1] - 管道高程[0];
            N = 全线泵站数;
            L = double.Parse(pipe.Length);
            d = double.Parse(pipe.OuterDiameter) - 2 * double.Parse(pipe.WallThickness);
            ν = double.Parse(oil.Viscosity);
            e = 当量粗糙度;
            Q = (double.Parse(oil.OutputByYear) * 10000000 / 24 / 350 / 3600) / double.Parse(oil.Density);
            Re = 流速 * d / 1000 / ν;
            ε = 2 * e / d;
            n = 泵数;
            H1 = 首站进站压力;
        }

        public double getResult(out double 总扬程, out double 总降压, out bool 是否合理)
        {
            总扬程 = getAllHead();
            总降压 = getAllBuck();
            是否合理 = getIsComeTrue(out double i);
            return averageStationSpacing();
        }

        public void getResult(out double 总扬程, out double 总降压, out double 末站出站压力, out bool 是否合理)
        {
            总扬程 = getAllHead();
            总降压 = getAllBuck();
            是否合理 = getIsComeTrue(out 末站出站压力);
        }

        /**
         * 总扬程
         **/
        private double getAllHead()
        {
            double result = 0;
            result = n * a() - n * b() * Math.Pow(Q * 3600, 1.75);
            return result;
        }

        /**
         * 总降压
         **/
        private double getAllBuck()
        {
            double result = 0;

            double hl = h();
            double hm = hl * 0.01;
            result = hl + hm + Z_;

            return result;
        }

        /**
         * 平均站间距
         **/
        private double averageStationSpacing()
        {
            double result = 0;

            result = L / N;

            return result;
        }

        /**
         * 判断是否可行 
         **/
        private bool getIsComeTrue(out double 末站进站压力)
        {
            末站进站压力 = getEndStationPressure(H1, false);
            if (末站进站压力 > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /**
         * 通过递归算出末站压力
         * @parm pressure 压力
         * @parm isOutOrIn true 表示进站,false 表示出站
         **/
        private double getEndStationPressure(double pressure, bool isOutOrIn)
        {
            double result = double.NaN;

            if (flag < N * 2)
            {
                if (isOutOrIn)
                {
                    double tmp = pressure - i() * averageStationSpacing() * 1000 - Z[flag / 2];
                    flag++;
                    double tmp2 = getEndStationPressure(tmp, !isOutOrIn);
                    result = double.IsNaN(tmp2) ? tmp : tmp2;
                }
                else
                {
                    double tmp = pressure + getAllHead() - 0.01 * h();
                    flag++;
                    double tmp2 = getEndStationPressure(tmp, !isOutOrIn);
                    result = double.IsNaN(tmp2) ? tmp : tmp2;
                }
            }

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
            result = 1.01 * β * r1 / r2 * averageStationSpacing() * 1000;

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
         * 判断流态
         * @out β
         * @out m
         **/
        private void flowArea(out double β, out double m)
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

