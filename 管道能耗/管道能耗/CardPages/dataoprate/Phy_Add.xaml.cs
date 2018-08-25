using DAL;
using Model;
using System;
using System.Collections.Generic;
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
using BLL;

namespace 管道能耗.CardPages.dataoprate
{
    /// <summary>
    /// Phy_Add.xaml 的交互逻辑
    /// </summary>
    public partial class Phy_Add : Window
    {
        int id = 0;
        public Phy_Add()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            OilsDAL oilsdal = new OilsDAL();
            id = oilsdal.getMaxOilsId() + 1;
            oilsId.Text = id + " ";
        }
        
        private void Button_build(object sender, RoutedEventArgs e)
        {
            Oils oils = new Oils(id, name.Text, density.Text, viscosity.Text, masFlow.Text, outputByYear.Text, volume_concentration.Text);
            IsNumber Isnumber = new IsNumber();
            if (Isnumber.isNumber(density.Text.Trim()) == false || Isnumber.isNumber(viscosity.Text.Trim()) == false || Isnumber.isNumber(masFlow.Text.Trim()) == false || Isnumber.isNumber(outputByYear.Text.Trim()) == false || Isnumber.isNumber(volume_concentration.Text.Trim()) == false)
            {
                MessageBox.Show("输入格式有误");

            }
            else
            {
                OilsDAL oilsdal = new OilsDAL();
                oilsdal.addOilsData(oils);
                MessageBox.Show("添加成功");
                Close();
            }
        }

        private void Button_cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
