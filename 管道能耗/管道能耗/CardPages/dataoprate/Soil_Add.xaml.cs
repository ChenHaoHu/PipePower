using DAL;
using Model;
using BLL;
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

namespace 管道能耗.CardPages.dataoprate
{
    /// <summary>
    /// Soil_Add.xaml 的交互逻辑
    /// </summary>
    public partial class Soil_Add : Window
    {
        int id = 0;
        public Soil_Add()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SoilDAL dal = new SoilDAL();
            id = dal.getMaxSoilId() + 1;
            soidId.Text = id + " ";
        }
        private void Button_build(object sender, RoutedEventArgs e)
        {
            IsNumber Isnumber = new IsNumber();

            Soil soil = new Soil(id,soilName.Text,soilTemp.Text,soilDiff.Text);
            if (Isnumber.isNumber(soilTemp.Text.Trim()) == false || Isnumber.isNumber(soilDiff.Text.Trim()) == false )
            {
                MessageBox.Show("输入格式有误");

            }
            else
            {
                SoilDAL dal = new SoilDAL();
                dal.addSoilData(soil);
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
