using BLL;
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
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
using Visifire.Charts;
using 管道能耗.CardPages.dataoprate;

namespace 管道能耗.CardPages.programme
{
    /// <summary>
    /// MixedOil.xaml 的交互逻辑
    /// </summary>
    public partial class MixedOil : Page
    {
        static int x = 580;
        static int y = 30;
        public MixedOil()
        {
            InitializeComponent();
            initChart();
            initComboBox();
            //    getRes();
        }

        void getRes(object sender, RoutedEventArgs e)
        {
            
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
            for (int i = 0; i < soildata.Rows.Count; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = soildata.Rows[i]["soil_name"];
                item.Tag = soildata.Rows[i]["soil_id"] + " ";
                if (i == soildata.Rows.Count - 1)
                {
                    item.IsSelected = true;
                }
                soil.Items.Add(item);
            }
            for (int i = 0; i < otherdata.Rows.Count; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = otherdata.Rows[i]["other_name"];
                item.Tag = otherdata.Rows[i]["other_id"] + " ";
                if (i == otherdata.Rows.Count - 1)
                {
                    item.IsSelected = true;
                }
                other.Items.Add(item);
            }

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
        private void look_soil(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)oils.SelectedItem;
            Console.WriteLine(item.Tag);
            Soil_Edit soil_Edit = new Soil_Edit(int.Parse(item.Tag + " "));
            soil_Edit.ShowDialog();
        }
        private void look_pump(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)pump.SelectedItem;
            Console.WriteLine(item.Tag);
            Pum_Edit pum_Edit = new Pum_Edit(int.Parse(item.Tag + " "));
            pum_Edit.ShowDialog();

        }
        private void look_other(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)other.SelectedItem;
            Console.WriteLine(item.Tag);
            Oth_Edit oth_Edit = new Oth_Edit(int.Parse(item.Tag + " "));
            oth_Edit.ShowDialog();
        }




        private void initChart()
        {
            Simon.Children.Clear();
            CreateChartSpline("水力坡降线", 0, 0);
        }

        #region 折线图
        public void CreateChartSpline(string name, int x, int y)
        {
            List<int> lsTime = new List<int>() { x, 0 };
            List<string> cherry = new List<string>() { "0", y + "" };
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
            xaxis.Interval = 100;
            xaxis.Suffix = "km";
            chart.AxesX.Add(xaxis);

            Axis yAxis = new Axis();
            yAxis.AxisMinimum = 0;
            yAxis.Interval = 5;
            yAxis.Suffix = "m";
            chart.AxesY.Add(yAxis);

            DataSeries dataSeries = new DataSeries();
            dataSeries.LegendText = "水力坡降线";
            dataSeries.RenderAs = RenderAs.Spline;//折线图
            dataSeries.XValueType = ChartValueTypes.Auto;
            DataPoint dataPoint;
            for (int i = 0; i < lsTime.Count; i++)
            {
                dataPoint = new DataPoint();
                dataPoint.XValue = lsTime[i];
                dataPoint.YValue = double.Parse(cherry[i]);
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

    }
}
