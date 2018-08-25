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
    /// Pum_Add.xaml 的交互逻辑
    /// </summary>
    public partial class Pum_Add : Window
    {
        int id = 0;
        public Pum_Add()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            PumpDAL pumpdal = new PumpDAL();
            id = pumpdal.getMaxPumpId() + 1;
            pumpId.Text = id + " ";
        }
        private void Button_build(object sender, RoutedEventArgs e)
        {
            IsNumber Isnumber = new IsNumber();
            Pump pump = new Pump(name.Text,id,head.Text,displacement.Text,power.Text,suctionPressure.Text,runTime.Text,inPressure.Text,outPressure.Text);
            if (Isnumber.isNumber(head.Text.Trim()) == false || Isnumber.isNumber(displacement.Text.Trim()) == false || Isnumber.isNumber(power.Text.Trim()) == false || Isnumber.isNumber(suctionPressure.Text.Trim()) == false || Isnumber.isNumber(runTime.Text.Trim()) == false || Isnumber.isNumber(inPressure.Text.Trim()) == false || Isnumber.isNumber(outPressure.Text.Trim()) == false )
            {
                MessageBox.Show("输入格式有误");

            }
            else
            {
                PumpDAL pumpdal = new PumpDAL();
                pumpdal.addPumpData(pump);
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
