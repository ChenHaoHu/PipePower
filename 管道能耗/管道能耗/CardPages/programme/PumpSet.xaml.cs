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
    /// PumpSet.xaml 的交互逻辑
    /// </summary>
    public partial class PumpSet : Page
    {
        static int x = 580;
        static int y = 30;
        public PumpSet()
        {
            InitializeComponent();
            initComboBox();
            //    getRes();
        }

        void getRes(object sender, RoutedEventArgs e)
        {
            double[] q = new double[] { 400, 500, 600 };
            double[] H = new double[] { 650, 600, 550 };
            double[] 高程1 = new double[] { 30, 70 };
            double[] 高程2 = new double[] { 30, 120, 70 };
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
            PipeDAL pipeDal = new PipeDAL();
            item = (ComboBoxItem)pipe.SelectedItem;
            ProDAL proDal = new ProDAL();
            int proid = proDal.getNowPro();
            DataTable pipedata = pipeDal.getSinglePipeData(proid, int.Parse(item.Tag + ""));
            Pipe pip = new Pipe(1, 1, "", "", "", pipedata.Rows[0]["pipe_length"] + ""
                , pipedata.Rows[0]["pipe_outer_diameter"] + "", pipedata.Rows[0]["wall_thickness"] + "", "", "", "", "", "", "", "", "");

            StationPumpPlan fspp1 = new StationPumpPlan(oil, pip, q, H, 高程1,1,
                        double.Parse(当量粗糙度.Text),
                        double.Parse(流速.Text),
                       3,
                        double.Parse(首站进站压力.Text));
      
            StationPumpPlan fspp2 = new StationPumpPlan(oil, pip, q, H, 高程2, 2,
                       double.Parse(当量粗糙度.Text),
                       double.Parse(流速.Text),
                      3,
                       double.Parse(首站进站压力.Text));

            double 总扬程, 总降压, 站间距;
            bool 是否合理;
           
            DataTable dt1 = new DataTable();
            DataColumn dc1 = new DataColumn("泵站数", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("泵数", Type.GetType("System.String"));
            DataColumn dc3 = new DataColumn("总扬程", Type.GetType("System.String"));
            DataColumn dc4 = new DataColumn("总压降", Type.GetType("System.String"));
            DataColumn dc5 = new DataColumn("站间距", Type.GetType("System.String"));
            DataColumn dc6 = new DataColumn("是否合理", Type.GetType("System.String"));
            dt1.Columns.Add(dc1);
            dt1.Columns.Add(dc2);
            dt1.Columns.Add(dc3);
            dt1.Columns.Add(dc4);
            dt1.Columns.Add(dc5);
            dt1.Columns.Add(dc6);
            站间距 = fspp1.getResult(out 总扬程, out 总降压, out 是否合理);
            Console.WriteLine("一个泵站 两个泵: 总扬程{0}\t总降压{1}\t站间距:{2}\t是否合理:{3}", 总扬程, 总降压, 站间距, 是否合理 ? "合理" : "不合理");
            DataRow dr1 = dt1.NewRow();
            dr1["泵站数"] = "一个泵站";
            dr1["泵数"] = "两个泵";
            dr1["总扬程"] = 总扬程;
            dr1["总压降"] = 总降压;
            dr1["站间距"] = 站间距;
            dr1["是否合理"] = 是否合理 ? "合理" : "不合理";
            dt1.Rows.Add(dr1);
            站间距 = fspp2.getResult(out 总扬程, out 总降压, out 是否合理);
            Console.WriteLine("两个泵站 两个泵: 总扬程{0}\t总降压{1}\t站间距:{2}\t是否合理:{3}", 总扬程, 总降压, 站间距, 是否合理 ? "合理" : "不合理");
            DataRow dr2 = dt1.NewRow();
            dr2["泵站数"] = "两个泵站";
            dr2["泵数"] = "两个泵";
            dr2["总扬程"] = 总扬程;
            dr2["总压降"] = 总降压;
            dr2["站间距"] = 站间距;
            dr2["是否合理"] = 是否合理 ? "合理" : "不合理";
            dt1.Rows.Add(dr2);
            table.IsReadOnly = true;
            table.ItemsSource = null;
            table.ItemsSource = dt1.DefaultView;
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
            //for (int i = 0; i < pumpdata.Rows.Count; i++)
            //{
            //    ComboBoxItem item = new ComboBoxItem();
            //    item.Content = pumpdata.Rows[i]["name"];
            //    item.Tag = pumpdata.Rows[i]["pump_id"] + " ";
            //    if (i == pumpdata.Rows.Count - 1)
            //    {
            //        item.IsSelected = true;
            //    }
            //    pump.Items.Add(item);
            //}
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
        private void look_soil(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)oils.SelectedItem;
            Console.WriteLine(item.Tag);
            Soil_Edit soil_Edit = new Soil_Edit(int.Parse(item.Tag + " "));
            soil_Edit.ShowDialog();
        }
        //private void look_pump(object sender, RoutedEventArgs e)
        //{
        //    ComboBoxItem item = (ComboBoxItem)pump.SelectedItem;
        //    Console.WriteLine(item.Tag);
        //    Pum_Edit pum_Edit = new Pum_Edit(int.Parse(item.Tag + " "));
        //    pum_Edit.ShowDialog();

        //}
        //private void look_other(object sender, RoutedEventArgs e)
        //{
        //    ComboBoxItem item = (ComboBoxItem)other.SelectedItem;
        //    Console.WriteLine(item.Tag);
        //    Oth_Edit oth_Edit = new Oth_Edit(int.Parse(item.Tag + " "));
        //    oth_Edit.ShowDialog();
        //}
    }
}
