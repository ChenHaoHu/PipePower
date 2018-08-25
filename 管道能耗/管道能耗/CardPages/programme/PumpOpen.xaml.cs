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
    /// FirstOpen.xaml 的交互逻辑
    /// </summary>
    public partial class FirstOpen : Page
    {
        static int x = 580;
        static int y = 30;
        public FirstOpen()
        {
            InitializeComponent();
            initComboBox();
            //    getRes();
        }

        void getRes(object sender, RoutedEventArgs e)
        {
            double[] q = new double[] { 400, 500, 600 };
            double[] H = new double[] { 650, 600, 550 };
            double[] 起点高程1 = new double[] { 0 };
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

            DataTable dt1 = new DataTable();
            DataColumn dc1 = new DataColumn("泵数", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("总扬程", Type.GetType("System.String"));
            DataColumn dc3 = new DataColumn("总压降", Type.GetType("System.String"));
            DataColumn dc4 = new DataColumn("末站出站压力", Type.GetType("System.String"));
            DataColumn dc5 = new DataColumn("是否合理", Type.GetType("System.String"));
            dt1.Columns.Add(dc1);
            dt1.Columns.Add(dc2);
            dt1.Columns.Add(dc3);
            dt1.Columns.Add(dc4);
            dt1.Columns.Add(dc5);

            for (int i = 1; i< 5;i++)
                {
                    ComboBoxItem sss = (ComboBoxItem)泵站.SelectedItem;
                StationPumpPlan fspp = null;

                //public StationPumpPlan(Oils oil, Pipe pipe, double[] 流量q, double[] 扬程H,
                //double[] 管道高程, double 全线泵站数, double 当量粗糙度, double 流速, double 泵数, double 首站进站压力)



                if (sss.Tag.Equals("1"))
                {
                     fspp = new StationPumpPlan(oil, pip, q, H, 高程1, int.Parse(sss.Tag+""),
                          double.Parse(当量粗糙度.Text),
                          double.Parse(流速.Text),
                         i,
                          double.Parse(首站进站压力.Text));
                }
                else{
                    fspp = new StationPumpPlan(oil, pip, q, H, 高程1, int.Parse(sss.Tag + ""),
                         double.Parse(当量粗糙度.Text),
                         double.Parse(流速.Text),
                        i,
                         double.Parse(首站进站压力.Text));
                }
                double 总扬程, 总降压, 末站出站压力;
                    bool 是否合理;
                    fspp.getResult(out 总扬程, out 总降压, out 末站出站压力, out 是否合理);
                    DataRow dr1 = dt1.NewRow();
                    dr1["泵数"] = i;
                    dr1["总扬程"] = 总扬程;
                    dr1["总压降"] = 总降压;
                    dr1["末站出站压力"] = 末站出站压力;
                    dr1["是否合理"] = 是否合理 ? "合理" : "不合理";
                    dt1.Rows.Add(dr1);
                }
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
                if (i == 0)
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
                if (i == 0)
                {
                    item.IsSelected = true;
                }
                oils.Items.Add(item);
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


    }
}


