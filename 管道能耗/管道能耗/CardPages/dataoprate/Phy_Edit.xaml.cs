using DAL;
using Model;
using BLL;
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
using System.Windows.Shapes;

namespace 管道能耗.CardPages.dataoprate
{
    /// <summary>
    /// Phy_Edit.xaml 的交互逻辑
    /// </summary>
    public partial class Phy_Edit : Window
    {
        int id;
        public Phy_Edit()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        public Phy_Edit(int id)
        {
            this.id = id;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.recoveryData(id);
        }
       private void Button_build(object sender, RoutedEventArgs e)
        {
            IsNumber Isnumber = new IsNumber();
            Oils oils = new Oils(id, name.Text, density.Text, viscosity.Text, masFlow.Text, outputByYear.Text, volume_concentration.Text);
            if (Isnumber.isNumber(density.Text.Trim()) == false || Isnumber.isNumber(viscosity.Text.Trim()) == false || Isnumber.isNumber(masFlow.Text.Trim()) == false || Isnumber.isNumber(outputByYear.Text.Trim()) == false || Isnumber.isNumber(volume_concentration.Text.Trim()) == false)
            {
                MessageBox.Show("输入格式有误");

            }
            else
            {
                OilsDAL dal = new OilsDAL();
                dal.delOilsData(id);
                dal.addOilsData(oils);
                MessageBox.Show("修改成功");
                Close(); 
            }
           
        }

        private void Button_cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void recoveryData(int id)
        {
            OilsDAL dal = new OilsDAL();
            DataTable data = dal.getSingleOilosData(id);
            oilsId.Text = data.Rows[0]["oils_id"] + " ";
            name.Text = data.Rows[0]["oils_name"] + " ";
            density.Text = data.Rows[0]["oils_density"] + " ";
            viscosity.Text = data.Rows[0]["oils_viscosity"] + " ";
            masFlow.Text = data.Rows[0]["mass_flow"] + " ";
            outputByYear.Text = data.Rows[0]["output_year"] + " ";
            volume_concentration.Text = data.Rows[0]["volume_concentration"] + " ";
        }
    }
}
