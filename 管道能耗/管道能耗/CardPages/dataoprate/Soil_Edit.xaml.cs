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
    /// Soil_Edit.xaml 的交互逻辑
    /// </summary>
    public partial class Soil_Edit : Window
    {
        int id;
        public Soil_Edit()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        public Soil_Edit(int id)
        {
            this.id = id;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.recoveryData(id);
        }
        

        private void Button_build(object sender, RoutedEventArgs e)
        {
           
            Soil soil = new Soil(id, soilName.Text, soilTemp.Text, soilDiff.Text);
            IsNumber Isnumber = new IsNumber();
            if (Isnumber.isNumber(soilTemp.Text.Trim()) == false || Isnumber.isNumber(soilDiff.Text.Trim()) == false)
            {
                MessageBox.Show("输入格式有误");

            }
            else
            {
                SoilDAL dal = new SoilDAL();
                dal.delSoilData(id);
                dal.addSoilData(soil);
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
            SoilDAL dal = new SoilDAL();
            DataTable data = dal.getSingelSoilData(id);
            soidId.Text = data.Rows[0]["soil_id"] + " ";
            soilName.Text = data.Rows[0]["soil_name"] + " ";
            soilTemp.Text = data.Rows[0]["soil_temp"] + " ";
            soilDiff.Text = data.Rows[0]["soil_difference"] + " ";
        }
    }
}
