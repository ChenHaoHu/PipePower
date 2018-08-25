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

namespace 管道能耗.CardPages.Programme
{
    /// <summary>
    /// BestProgrammer.xaml 的交互逻辑
    /// </summary>
    public partial class RunningPower : Page
    {
    
        public RunningPower()
        {
            InitializeComponent();
            initComboBox();
            //    getRes();
        }

        void getRes(object sender, RoutedEventArgs e)
        {
            OilsDAL oilsDal = new OilsDAL();
            ComboBoxItem item = (ComboBoxItem)oils.SelectedItem;
            DataTable oilsdata = oilsDal.getSingleOilosData(int.Parse(item.Tag + ""));
            Oils oil = new Oils()
            {
                // Density = "754",
                Density = oilsdata.Rows[0]["oils_density"] + "",
                //  Viscosity = "1.08E-06",
                Viscosity = oilsdata.Rows[0]["oils_viscosity"] + "",
                //    OutputByYear = "270",
                OutputByYear = oilsdata.Rows[0]["output_year"] + "",
                //  MasFlow = "600",
                MasFlow = oilsdata.Rows[0]["mass_flow"] + "",
                //  Volume_concentration = "0.01"
                Volume_concentration = oilsdata.Rows[0]["volume_concentration"] + "",
            };
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
            Pump pum = new Pump("", 1, "", "", pumpdata.Rows[0]["power"] + "", "", "", pumpdata.Rows[0]["in_pressure"] + "", pumpdata.Rows[0]["out_pressure"] + "");

            OtherDAL otherDAL = new OtherDAL();
            item = (ComboBoxItem)other.SelectedItem;
            DataTable otherdata = otherDAL.getSingleOtherData(int.Parse(item.Tag + ""));
            //  PowerCost pc = new PowerCost(oil, pip, pum, 350, 0.87, 27, 150, double.Parse(otherdata.Rows[0]["cost_operating"] + ""), 1.5, 0.06, 9.8, 30);
            double[] q = new double[] { 300, 500, 600 };
            double[] H = new double[] { 650, 600, 550 };

            ////public PowerCost(Oils oils, Pipe pipe, Pump pump, double[] 泵流量, double[] 泵扬程, 
            //double 年工作时间, double 电机效率, double 管道起点高程, double 管道终点高程, 
            //    double 电费, double 流速, double 当量粗糙度, double 重力加速度, double 首站进站压力)
            PowerCost pc = new PowerCost(oil, pip, pum, q, H,
                double.Parse(年工作时间.Text),
                double.Parse(电机效率.Text) ,
                double.Parse(管道起点高程.Text) ,
                double.Parse(管道终点高程.Text) , 
                double.Parse(otherdata.Rows[0]["cost_operating"] + ""),
                double.Parse(流速.Text),
                 double.Parse(当量粗糙度.Text),
                 double.Parse(重力加速度.Text),
                double.Parse(首站进站压力.Text));



            double 运行能耗 = pc.getResult();
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("运行能耗:{0}", 运行能耗);
          
            res.Content = 运行能耗 + "元";

            resa.Visibility = Visibility.Visible;
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
        //private void look_other(object sender, RoutedEventArgs e)
        //{
        //    ComboBoxItem item = (ComboBoxItem)other.SelectedItem;
        //    Console.WriteLine(item.Tag);
        //    Oth_Edit oth_Edit = new Oth_Edit(int.Parse(item.Tag + " "));
        //    oth_Edit.ShowDialog();
        //}

    }
}
