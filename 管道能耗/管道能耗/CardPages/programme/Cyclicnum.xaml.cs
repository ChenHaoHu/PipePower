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
    public partial class BestProgrammer : Page
    {
        static int x = 580;
        static int y = 30;
        public BestProgrammer()
        {
            InitializeComponent();
            initComboBox();
        }

        void getRes(object sender, RoutedEventArgs e)
        {
            PipeDAL pipeDal = new PipeDAL();
           ComboBoxItem item = (ComboBoxItem)pipe.SelectedItem;
            ProDAL proDal = new ProDAL();
            int proid = proDal.getNowPro();
            DataTable pipedata = pipeDal.getSinglePipeData(proid, int.Parse(item.Tag + ""));
            Pipe pip = new Pipe(1, 1, "", "", "", pipedata.Rows[0]["pipe_length"] + ""
                , pipedata.Rows[0]["pipe_outer_diameter"] + "", pipedata.Rows[0]["wall_thickness"] + "", "", "", "", "", "", "", "", "");

          

            //find oil
            OilsDAL oilsDAL = new OilsDAL();
            DataTable oilsdata = oilsDAL.getOilosData();
            Oils oilB = null;
            Oils oilA = null;
            Console.WriteLine(pipedata.Rows[0]["tank_type_a"] + "");
            for (int i = 0; i < oilsdata.Rows.Count; i++)
            {
                Console.WriteLine(oilsdata.Rows[i]["oils_name"] + "");
                if (((string)pipedata.Rows[0]["tank_type_a"]).Trim().Equals(((string)oilsdata.Rows[i]["oils_name"]).Trim()))
                {
                    Console.WriteLine("lalallalal  aaaa ");
                    oilA = new Oils()
                    {
                        Name = oilsdata.Rows[i]["oils_name"] + "",
                        // Density = "754",
                        Density = oilsdata.Rows[i]["oils_density"] + "",
                        //  Viscosity = "1.08E-06",
                        Viscosity = oilsdata.Rows[i]["oils_viscosity"] + "",
                        //    OutputByYear = "270",
                        OutputByYear = oilsdata.Rows[i]["output_year"] + "",
                        //  MasFlow = "600",
                        MasFlow = oilsdata.Rows[i]["mass_flow"] + "",
                        //  Volume_concentration = "0.01"
                        Volume_concentration = oilsdata.Rows[i]["volume_concentration"] + "",
                    };
                }

                if (((string)pipedata.Rows[0]["tank_type_b"]).Trim().Equals(((string)oilsdata.Rows[i]["oils_name"]).Trim()))
                {
                    Console.WriteLine("lalallalal  bbbbb ");
                    oilB = new Oils()
                    {
                        Name = oilsdata.Rows[i]["oils_name"] + "",
                        // Density = "754",
                        Density = oilsdata.Rows[i]["oils_density"] + "",
                        //  Viscosity = "1.08E-06",
                        Viscosity = oilsdata.Rows[i]["oils_viscosity"] + "",
                        //    OutputByYear = "270",
                        OutputByYear = oilsdata.Rows[i]["output_year"] + "",
                        //  MasFlow = "600",
                        MasFlow = oilsdata.Rows[i]["mass_flow"] + "",
                        //  Volume_concentration = "0.01"
                        Volume_concentration = oilsdata.Rows[i]["volume_concentration"] + "",
                    };
                }
            }
               if(oilA == null || oilB  == null)
            {
                MessageBox.Show("请确认管道的油灌类型的数据存在数据库中");
            }
            else
            {
                OtherDAL otherDAL = new OtherDAL();
                item = (ComboBoxItem)other.SelectedItem;
                DataTable otherdata = otherDAL.getSingleOtherData(int.Parse(item.Tag + ""));
                //public Cyclicnum(Oils oilA, Oils oilB, Pipe pipe, double A油品费用, double B油品费用, double 混入B油罐中A体积量, 
                //    double 混入A油罐中B体积量, double 流速, double 年工作时间, 
                //    double 单位有效容积储罐的经营费用, double 单位有效容积储罐的建设费用, double 投资年回收系数)
                //double 单位有效容积储罐的经营费用,double 单位有效容积储罐的建设费用,double 投资年回收系数
                //public Cyclicnum(Oils oilA, Oils oilB, Pipe pipe, double A油品费用, double B油品费用, double 混入B油罐中A体积量, double 混入A油罐中B体积量, double 流速, double 年工作时间, double 单位有效容积储罐的经营费用, double 单位有效容积储罐的建设费用, double 投资年回收系数)
                CyclicnumAlgor c = new CyclicnumAlgor(oilA, oilB, pip,
                    double.Parse(A油品费用.Text),
                     double.Parse(B油品费用.Text),
                     double.Parse(混入B油罐中A体积量.Text),
                     double.Parse(混入A油罐中B体积量.Text),
                     double.Parse(流速.Text),
                     double.Parse(年工作时间.Text),
                    double.Parse(otherdata.Rows[0]["cost_operating"]+""), double.Parse(otherdata.Rows[0]["cost_build"] + ""), double.Parse(otherdata.Rows[0]["n_recovery"] + ""));
                double 最优循环次数 = c.getResult();
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("最优循环次数:{0}", 最优循环次数);
                list.Items.Clear();
                ListViewItem viewitem1 = new ListViewItem();
                viewitem1.Content = "混油A:  "+oilA.Name;
                list.Items.Add(viewitem1);
                ListViewItem viewitem2 = new ListViewItem();
                viewitem2.Content = "混油B:  " + oilB.Name;
                list.Items.Add(viewitem2);
                res.Content = 最优循环次数 + "次";

                resa.Visibility = Visibility.Visible;
            }


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
            //for (int i = 0; i < oilsdata.Rows.Count; i++)
            //{
            //    ComboBoxItem item = new ComboBoxItem();
            //    item.Content = oilsdata.Rows[i]["oils_name"];
            //    item.Tag = oilsdata.Rows[i]["oils_id"] + " ";
            //    if (i == oilsdata.Rows.Count - 1)
            //    {
            //        item.IsSelected = true;
            //    }
            //    oils.Items.Add(item);
            //}
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
        //private void look_oils(object sender, RoutedEventArgs e)
        //{
        //    ComboBoxItem item = (ComboBoxItem)oils.SelectedItem;
        //    Console.WriteLine(item.Tag);
        //    Phy_Edit phy_Edit = new Phy_Edit(int.Parse(item.Tag + " "));
        //    phy_Edit.ShowDialog();
        //}
        //private void look_soil(object sender, RoutedEventArgs e)
        //{
        //    ComboBoxItem item = (ComboBoxItem)soil.SelectedItem;
        //    Console.WriteLine(item.Tag);
        //    Soil_Edit soil_Edit = new Soil_Edit(int.Parse(item.Tag + " "));
        //    soil_Edit.ShowDialog();
        //}
        //private void look_pump(object sender, RoutedEventArgs e)
        //{
        //    ComboBoxItem item = (ComboBoxItem)pump.SelectedItem;
        //    Console.WriteLine(item.Tag);
        //    Pum_Edit pum_Edit = new Pum_Edit(int.Parse(item.Tag + " "));
        //    pum_Edit.ShowDialog();

        //}
        private void look_other(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)other.SelectedItem;
            Console.WriteLine(item.Tag);
            Oth_Edit oth_Edit = new Oth_Edit(int.Parse(item.Tag + " "));
            oth_Edit.ShowDialog();
        }

    }
}
