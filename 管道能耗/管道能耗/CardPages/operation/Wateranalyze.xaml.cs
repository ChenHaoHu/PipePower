using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using 管道能耗.CardPages.dataoprate;
using Visifire.Charts;
using Model;
using BLL;

namespace 管道能耗.CardPages.operation
{
    /// <summary>
    /// Wateranalyze.xaml 的交互逻辑
    /// </summary>
    public partial class AnalysisHydraulic : Page
    {

        static double x = 580;
        static double y = 30;
        static double m = 30;

        public AnalysisHydraulic()
        {
            InitializeComponent();
            //initChart();
            initComboBox();
            res.Visibility = Visibility.Hidden;
           //    getRes();
        }

        void getRes(object sender, RoutedEventArgs e)
        {
            OilsDAL oilsDal = new OilsDAL();
            ComboBoxItem item = (ComboBoxItem)oils.SelectedItem;
            DataTable oilsdata = oilsDal.getSingleOilosData(int.Parse(item.Tag + ""));
            Oils oil = new Oils()
            {
                //Density = "754",
                Density = oilsdata.Rows[0]["oils_density"] + "",
                //Viscosity = "1.08e-6",
                Viscosity = oilsdata.Rows[0]["oils_viscosity"] + "",
                //OutputByYear = "270",
                OutputByYear = oilsdata.Rows[0]["output_year"] + "",
            };


            double[] q = new double[] { 300, 500, 600 };
            double[] H = new double[] { 650, 600, 550 };

            PipeDAL pipeDal = new PipeDAL();
            item = (ComboBoxItem)pipe.SelectedItem;
            ProDAL proDal = new ProDAL();
            int proid = proDal.getNowPro();
            DataTable pipedata = pipeDal.getSinglePipeData(proid, int.Parse(item.Tag + ""));
            Pipe pip = new Pipe(1, 1, "", "", "", pipedata.Rows[0]["pipe_length"] + ""
                , pipedata.Rows[0]["pipe_outer_diameter"] + "", pipedata.Rows[0]["wall_thickness"] + "", "", "", "", "", "", "", "", "");

            PumpDAL pumpDal = new PumpDAL();
            item = (ComboBoxItem)pump.SelectedItem;
            DataTable pumpdata = pumpDal.getSinglePumpData(int.Parse(item.Tag + ""));
            Pump pum = new Pump("", 1, "", "", "", "", "", pumpdata.Rows[0]["in_pressure"] + "", "");

            //public WaterCharacteristics(Oils oil, Pipe pipe, Pump pump, double 全线总摩阻, 
            //double[] 泵流量, double[] 泵扬程, double 当量粗糙度, double 流速, double 管道起点高程, double 管道终点高程)


            //获取输入框信息


            //WaterCharacteristics water = new WaterCharacteristics(oil, pip, pum, 500, q, H, 0.06, 1.5, 27, 150);

            WaterCharacteristics water = new WaterCharacteristics(oil, pip, pum,
                double.Parse(全线总摩阻.Text), q, H,
                  double.Parse(当量粗糙度.Text),
                   double.Parse(流速.Text),
                  double.Parse(管道起点高程.Text) ,
                   double.Parse(管道终点高程.Text));
            double 前站出站压力, 末站进站压力, 斜率, 沿程摩阻;
            water.getResult(out 前站出站压力, out 末站进站压力, out 斜率, out 沿程摩阻);
            Console.WriteLine("前站出站压力:{0}\n末站进站压力{1}\n斜率:{2}\n沿程摩阻:{3}", 前站出站压力, 末站进站压力, 斜率, 沿程摩阻);


            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataColumn dc1 = new DataColumn("油品名称", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("前站出站压力", Type.GetType("System.String"));
            DataColumn dc3 = new DataColumn("末站进站压力", Type.GetType("System.String"));
            DataColumn dc4 = new DataColumn("管道总压降", Type.GetType("System.String"));
            DataColumn dc5 = new DataColumn("沿程摩阻", Type.GetType("System.String"));
            DataColumn dc6 = new DataColumn("斜率", Type.GetType("System.String"));
            dt1.Columns.Add(dc1);
            dt1.Columns.Add(dc2);
            dt1.Columns.Add(dc3);
            dt2.Columns.Add(dc4);
            dt2.Columns.Add(dc5);
            dt2.Columns.Add(dc6);
            //以上代码完成了DataTable的构架，但是里面是没有任何数据的
            DataRow dr1 = dt1.NewRow();
            DataRow dr2 = dt2.NewRow();
            dr1["油品名称"] = oilsdata.Rows[0]["oils_name"] + "";
            dr1["前站出站压力"] = 前站出站压力;
            dr1["末站进站压力"] = 末站进站压力;
            dr2["沿程摩阻"] = 沿程摩阻;
            dr2["管道总压降"] = Math.Round(Math.Abs(前站出站压力- 末站进站压力),4);
            dr2["斜率"] = 斜率;
            dt1.Rows.Add(dr1);
            dt2.Rows.Add(dr2);
            table1.IsReadOnly = true;
            table1.ItemsSource = null;
            table1.ItemsSource = dt1.DefaultView;
            table2.IsReadOnly = true;
            table2.ItemsSource = null;
            table2.ItemsSource = dt2.DefaultView;

            //设置x,y
            x = (double)(int.Parse(pum.InPressure) + 斜率 * double.Parse(pip.Length)); 
             y = (double)(int.Parse(pip.Length));
            m = (double)(double.Parse(pum.InPressure));
            Console.WriteLine(x + "  " + y);
            res.Visibility = Visibility.Visible;
            Simon.Children.Clear();
            CreateChartSpline("水力坡降线",y,x,m);
            /*
            PipeWork pw = new PipeWork();
            double 扬程 = pw.getHead(30,123,270, 754,1.5,518, 1.08E-06,0.06,580);
            Console.WriteLine("扬程:{0}",扬程);
            double 系统工作流量 = pw.getSystemWorkFlow(30, 1, q, H, 150, 27, 270, 754, 1.08E-06, 518, 0.06, 1.5, 580, 123, 50, 15000);
            Console.WriteLine("系统工作流量{0}", 系统工作流量);
            */
        }

        void initComboBox()
        {
            PipeDAL pipeDAL = new PipeDAL();
            ProDAL proDal = new ProDAL();
            int proid = proDal.getNowPro();
            DataTable pipedata = pipeDAL.getPipeBaisc(proid);
            DataTable oilsdata = new OilsDAL().getOilosData();
            DataTable pumpdata = new PumpDAL().getPumpData();
            DataTable soildata = new SoilDAL().getSoilData();
            DataTable otherdata = new OtherDAL().getOtherData();

            for (int i = 0; i < pipedata.Rows.Count; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = pipedata.Rows[i]["pipe_name"];
                item.Tag = pipedata.Rows[i]["pipe_id"];
                if (i == pipedata.Rows.Count - 1)
                {
                    item.IsSelected = true;
                }
                pipe.Items.Add(item);
            }
            for (int i = 0; i < oilsdata.Rows.Count; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = oilsdata.Rows[i]["oils_name"];
                item.Tag = oilsdata.Rows[i]["oils_id"] + " ";
                if (i == oilsdata.Rows.Count - 1)
                {
                    item.IsSelected = true;
                }
                oils.Items.Add(item);
            }
            for (int i = 0; i < pumpdata.Rows.Count; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = pumpdata.Rows[i]["name"];
                item.Tag = pumpdata.Rows[i]["pump_id"] + " ";
                if (i == pumpdata.Rows.Count - 1)
                {
                    item.IsSelected = true;
                }
                pump.Items.Add(item);
            }
            //for (int i = 0; i < soildata.Rows.Count; i++)
            //{
            //    ComboBoxItem item = new ComboBoxItem();
            //    item.Content = soildata.Rows[i]["soil_name"];
            //    item.Tag = soildata.Rows[i]["soil_id"] + " ";
            //    if (i == soildata.Rows.Count - 1)
            //    {
            //        item.IsSelected = true;
            //    }
            //    soil.Items.Add(item);
            //}
            //for (int i = 0; i < otherdata.Rows.Count; i++)
            //{
            //    ComboBoxItem item = new ComboBoxItem();
            //    item.Content = otherdata.Rows[i]["other_name"];
            //    item.Tag = otherdata.Rows[i]["other_id"] + " ";
            //    if (i == otherdata.Rows.Count - 1)
            //    {
            //        item.IsSelected = true;
            //    }
            //    other.Items.Add(item);
            //}

        }

        private void look_pipe(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)pipe.SelectedItem;
            Console.WriteLine(item.Tag);
            ProDAL proDal = new ProDAL();
            int proid = proDal.getNowPro();
            PiP_Edit piP_Edit = new PiP_Edit(proid, int.Parse(item.Tag + " "));
            piP_Edit.ShowDialog();
        }
        private void look_oils(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)oils.SelectedItem;
            Console.WriteLine(item.Tag);
            Phy_Edit phy_Edit = new Phy_Edit(int.Parse(item.Tag + " "));
            phy_Edit.ShowDialog();
        }
        //private void look_soil(object sender, RoutedEventArgs e)
        //{
        //    ComboBoxItem item = (ComboBoxItem)oils.SelectedItem;
        //    Console.WriteLine(item.Tag);
        //    Soil_Edit soil_Edit = new Soil_Edit(int.Parse(item.Tag + " "));
        //    soil_Edit.ShowDialog();
        //}
        private void look_pump(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)pump.SelectedItem;
            Console.WriteLine(item.Tag);
            Pum_Edit pum_Edit = new Pum_Edit(int.Parse(item.Tag + " "));
            pum_Edit.ShowDialog();

        }
        //private void look_other(object sender, RoutedEventArgs e)
        //{
        //    ComboBoxItem item = (ComboBoxItem)other.SelectedItem;
        //    Console.WriteLine(item.Tag);
        //    Oth_Edit oth_Edit = new Oth_Edit(int.Parse(item.Tag + " "));
        //    oth_Edit.ShowDialog();
        //}




        private void initChart()
        {
            Simon.Children.Clear();
            CreateChartSpline("水力坡降线",0,0,0);
        }

        #region 折线图
        public void CreateChartSpline(string name, double x, double y, double m)
        {
            Console.WriteLine(y);
            Console.WriteLine(m);
            List<double> XXXX = new List<double>() { x, 0 };
            List<string> XXXXY = new List<string>() { m* 1000 + "", y* 1000 + "" };
            Chart chart = new Chart();
            chart.Width = 500;
            chart.Height = 400;
            chart.Margin = new Thickness(0, 5, 0, 5);
            chart.ToolBarEnabled = false;
            chart.ScrollingEnabled = false;//是否启用或禁用滚动
                                           //  chart.View3D = true;//3D效果显示
            Title title = new Title();
            title.Text = name;
            title.Padding = new Thickness(0, 10, 5, 0);
            chart.Titles.Add(title);
            Axis xaxis = new Axis();
            xaxis.AxisMinimum = 0;
            xaxis.AxisMaximum = 2*x;
            xaxis.Interval = x;
            xaxis.Suffix = "km";
            chart.AxesX.Add(xaxis);

            Axis yAxis = new Axis();
            yAxis.AxisMinimum = m* 1000 - 100;
            yAxis.AxisMaximum = y * 1000;
            //yAxis.Interval = y*10000;
            yAxis.Suffix = "(/1000)m";
            chart.AxesY.Add(yAxis);

            DataSeries dataSeries = new DataSeries();
            dataSeries.LegendText = "水力坡降线";
            dataSeries.RenderAs = RenderAs.Spline;//折线图
            dataSeries.XValueType = ChartValueTypes.Auto;
            DataPoint dataPoint;
            for (int i = 0; i < XXXX.Count; i++)
            {
                dataPoint = new DataPoint();
                dataPoint.XValue = XXXX[i];
                dataPoint.YValue = double.Parse(XXXXY[i]);
                dataPoint.MarkerSize = 6;
                dataPoint.Color = new SolidColorBrush(Colors.Black);
                dataPoint.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
                dataSeries.DataPoints.Add(dataPoint);
            }
            chart.Series.Add(dataSeries);
            Grid gr = new Grid();
            gr.Children.Add(chart);

            Simon.Children.Add(gr);
        }
        #endregion

        #region 点击事件
        //点击事件
        void dataPoint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //DataPoint dp = sender as DataPoint;
            //MessageBox.Show(dp.YValue.ToString());
        }
        #endregion


        public void button_export(object sender, RoutedEventArgs e)
        {
            OilsDAL oilsDal = new OilsDAL();
            ComboBoxItem item = (ComboBoxItem)oils.SelectedItem;
            DataTable oilsdata = oilsDal.getSingleOilosData(int.Parse(item.Tag + ""));
            Oils oil = new Oils()
            {
                //Density = "754",
                Density = oilsdata.Rows[0]["oils_density"] + "",
                //Viscosity = "1.08e-6",
                Viscosity = oilsdata.Rows[0]["oils_viscosity"] + "",
                //OutputByYear = "270",
                OutputByYear = oilsdata.Rows[0]["output_year"] + "",
            };


            double[] q = new double[] { 300, 500, 600 };
            double[] H = new double[] { 650, 600, 550 };

            PipeDAL pipeDal = new PipeDAL();
            item = (ComboBoxItem)pipe.SelectedItem;
            ProDAL proDal = new ProDAL();
            int proid = proDal.getNowPro();
            DataTable pipedata = pipeDal.getSinglePipeData(proid, int.Parse(item.Tag + ""));
            Pipe pip = new Pipe(1, 1, "", "", "", pipedata.Rows[0]["pipe_length"] + ""
                , pipedata.Rows[0]["pipe_outer_diameter"] + "", pipedata.Rows[0]["wall_thickness"] + "", "", "", "", "", "", "", "", "");

            PumpDAL pumpDal = new PumpDAL();
            item = (ComboBoxItem)pump.SelectedItem;
            DataTable pumpdata = pumpDal.getSinglePumpData(int.Parse(item.Tag + ""));
            Pump pum = new Pump("", 1, "", "", "", "", "", pumpdata.Rows[0]["in_pressure"] + "", "");
            WaterCharacteristics water = new WaterCharacteristics(oil, pip, pum, 500, q, H, 0.06, 1.5, 27, 150);
            double 前站出站压力, 末站进站压力, 斜率, 沿程摩阻;
            water.getResult(out 前站出站压力, out 末站进站压力, out 斜率, out 沿程摩阻);
            Console.WriteLine("前站出站压力:{0}\n末站进站压力{1}\n斜率:{2}\n沿程摩阻:{3}", 前站出站压力, 末站进站压力, 斜率, 沿程摩阻);
            DataTable dt1 = new DataTable();
            DataColumn dc1 = new DataColumn("油品名称", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("前站出站压力", Type.GetType("System.String"));
            DataColumn dc3 = new DataColumn("末站进站压力", Type.GetType("System.String"));
            DataColumn dc4 = new DataColumn("管道总压降", Type.GetType("System.String"));
            DataColumn dc5 = new DataColumn("沿程摩阻", Type.GetType("System.String"));
            DataColumn dc6 = new DataColumn("斜率", Type.GetType("System.String"));
            dt1.Columns.Add(dc1);
            dt1.Columns.Add(dc2);
            dt1.Columns.Add(dc3);
            dt1.Columns.Add(dc4);
            dt1.Columns.Add(dc5);
            dt1.Columns.Add(dc6);
            //以上代码完成了DataTable的构架，但是里面是没有任何数据的
            DataRow dr1 = dt1.NewRow();
            dr1["油品名称"] = oilsdata.Rows[0]["oils_name"] + "";
            dr1["前站出站压力"] = 前站出站压力;
            dr1["末站进站压力"] = 末站进站压力;
            dr1["沿程摩阻"] = 沿程摩阻;
            dr1["管道总压降"] = Math.Round(Math.Abs(前站出站压力 - 末站进站压力), 4);
            dr1["斜率"] = 斜率;
            dt1.Rows.Add(dr1);
            string path = string.Empty;
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "Files (*.xlsx*)|*.xls*"//如果需要筛选txt文件（"Files (*.txt)|*.txt"）
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                path = openFileDialog.FileName;
            }
            if (path.Length > 0)
            {
                SoilDAL dAL = new SoilDAL();
                Console.WriteLine(path);
                exportExcel exp = new exportExcel();
                exp.export(dt1, path, "Wateranalyze");
                MessageBox.Show("导出完成");
            }
        }
    }
}
