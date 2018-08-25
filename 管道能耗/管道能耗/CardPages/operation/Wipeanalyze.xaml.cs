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
using BLL;
using Model;

namespace 管道能耗.CardPages.operation
{
    /// <summary>
    /// PipelineWork.xaml 的交互逻辑
    /// </summary>
    public partial class PipelineWork : Page
    {

        public PipelineWork()
        {
            InitializeComponent();
            initComboBox();
            res.Visibility = Visibility.Hidden;
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
                //    MasFlow = "600",
                //    Volume_concentration = "0.01"
                MasFlow = oilsdata.Rows[0]["mass_flow"] + "",
                Volume_concentration = oilsdata.Rows[0]["volume_concentration"] + ""
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
            Pump pum = new Pump("", 1, "", "", "", "", "", pumpdata.Rows[0]["in_pressure"] + "", pumpdata.Rows[0]["out_pressure"] + "");


            //public PipeWork(Oils oil, Pipe pipe, Pump pump, double[] 泵流量, double[] 泵扬程, double 当量粗糙度,
            //    double 流速, double 管道起点高程, double 管道终点高程, double 首站进站压力)


            PipeWork pw = new PipeWork(oil, pip, pum, q, H,
                 double.Parse(当量粗糙度.Text),
                double.Parse(流速.Text),
                 double.Parse(管道起点高程.Text),
                 double.Parse(管道终点高程.Text),
                double.Parse(首站进站压力.Text));


            double 扬程, 系统工作流量, A, B;
            pw.getResult(out 系统工作流量, out 扬程, out A, out B);
            DataTable dt1 = new DataTable();
            DataColumn dc1 = new DataColumn("管道名", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("扬程", Type.GetType("System.String"));
            DataColumn dc3 = new DataColumn("工作流量", Type.GetType("System.String"));
            dt1.Columns.Add(dc1);
            dt1.Columns.Add(dc2);
            dt1.Columns.Add(dc3);
            DataRow dr1 = dt1.NewRow();
            dr1["管道名"] = pipedata.Rows[0]["pipe_name"] + "";
            dr1["扬程"] = 扬程;
            dr1["工作流量"] = 系统工作流量;
            dt1.Rows.Add(dr1);
            table1.IsReadOnly = true;
            table1.ItemsSource = null;
            table1.ItemsSource = dt1.DefaultView;
            res.Visibility = Visibility.Visible;
            Simon.Children.Clear();
            CreateChartSpline("水力坡降线", A, B);
        
            Console.WriteLine(A + "   " + B + "   " + 系统工作流量);
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
            CreateChartSpline("曲线泵站特性曲线", 0, 0);
        }

        #region 折线图
        public void CreateChartSpline(string name, double A, double B)
        {
            //H = A + BQ^1.75
            //H是扬程,,Q是系统工作流量(横坐标),B 和 A 可以从getResult 的out参数获得

            List<double> XXXX = new List<double>() { 0, 0.01, 0.02, 0.03, 0.04, 0.05, 0.06, 0.07, 0.08, 0.09, 0.10 };
            List<string> YYYY = new List<string>() {
              10000 *(A +Math.Pow(0,1.75)*B)+"",
              10000*(A +Math.Pow(0.01,1.75)*B)+"",
              10000*(A +Math.Pow(0.02,1.75)*B)+"",
              10000*(A +Math.Pow(0.03,1.75)*B)+"",
              10000*(A +Math.Pow(0.04,1.75)*B)+"",
              10000*(A +Math.Pow(0.05,1.75)*B)+"",
              10000*(A +Math.Pow(0.06,1.75)*B)+"",
              10000*(A +Math.Pow(0.07,1.75)*B)+"",
              10000*(A +Math.Pow(0.08,1.75)*B)+"",
              10000*(A +Math.Pow(0.09,1.75)*B)+"",
              10000*(A +Math.Pow(0.10,1.75)*B)+"",
                };
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
            xaxis.AxisMinimum = -0.05;
            xaxis.Interval = 0.01;
            xaxis.Suffix = "km";
            chart.AxesX.Add(xaxis);

            Axis yAxis = new Axis();
            yAxis.AxisMinimum = A*10000-5;
            yAxis.AxisMaximum = A * 10000+5;
            yAxis.Interval = 0.5;
            yAxis.Suffix = "(/10000)m";
            chart.AxesY.Add(yAxis);

            DataSeries dataSeries = new DataSeries();
            dataSeries.LegendText = "曲线泵站特性曲线";
            dataSeries.RenderAs = RenderAs.Spline;//折线图
            dataSeries.XValueType = ChartValueTypes.Auto;
            DataPoint dataPoint;
            for (int i = 0; i < XXXX.Count; i++)
            {
                dataPoint = new DataPoint();
                dataPoint.XValue = XXXX[i];
                dataPoint.YValue = double.Parse(YYYY[i]);
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
                //    MasFlow = "600",
                //    Volume_concentration = "0.01"
                MasFlow = oilsdata.Rows[0]["mass_flow"] + "",
                Volume_concentration = oilsdata.Rows[0]["volume_concentration"] + ""
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
            Pump pum = new Pump("", 1, "", "", "", "", "", pumpdata.Rows[0]["in_pressure"] + "", pumpdata.Rows[0]["out_pressure"] + "");
            PipeWork pw = new PipeWork(oil, pip, pum, q, H, 0.06, 1.5, 27, 150, 30);
            double 扬程, 系统工作流量, A, B;
            pw.getResult(out 系统工作流量, out 扬程, out A, out B);
            DataTable dt1 = new DataTable();
            DataColumn dc1 = new DataColumn("管道名", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("扬程", Type.GetType("System.String"));
            DataColumn dc3 = new DataColumn("工作流量", Type.GetType("System.String"));
            dt1.Columns.Add(dc1);
            dt1.Columns.Add(dc2);
            dt1.Columns.Add(dc3);
            DataRow dr1 = dt1.NewRow();
            dr1["管道名"] = pipedata.Rows[0]["pipe_name"] + "";
            dr1["扬程"] = 扬程;
            dr1["工作流量"] = 系统工作流量;
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
                exp.export(dt1, path, "Wipeanalyze");
                MessageBox.Show("导出完成");
            }
        }
    }
}
