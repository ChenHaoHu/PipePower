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
    /// Pum_Edit.xaml 的交互逻辑
    /// </summary>
    public partial class Pum_Edit : Window
    {
        int id;
        public Pum_Edit()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }
        public Pum_Edit(int id)
        {
            this.id = id;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.recoveryData(id);
        }

        private void Button_build(object sender, RoutedEventArgs e)
        {
            IsNumber Isnumber = new IsNumber();
            Pump pump = new Pump(name.Text, id, head.Text, displacement.Text, power.Text, suctionPressure.Text, runTime.Text, inPressure.Text, outPressure.Text);
           if (Isnumber.isNumber(head.Text.Trim()) == false || Isnumber.isNumber(displacement.Text.Trim()) == false || Isnumber.isNumber(power.Text.Trim()) == false || Isnumber.isNumber(suctionPressure.Text.Trim()) == false || Isnumber.isNumber(runTime.Text.Trim()) == false || Isnumber.isNumber(inPressure.Text.Trim()) == false || Isnumber.isNumber(outPressure.Text.Trim()) == false)
            {
                MessageBox.Show("输入格式有误");

            }
            else
            {
                PumpDAL dal = new PumpDAL();
                dal.delPumData(id);
                dal.addPumpData(pump);
                MessageBox.Show("添加成功");
                Close();
            }
          
        }

        private void Button_cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void recoveryData(int id)
        {
            PumpDAL dal = new PumpDAL();
            DataTable data = dal.getSinglePumpData(id);
            pumpId.Text = data.Rows[0]["pump_id"] + " ";
            name.Text = data.Rows[0]["name"] + " ";
            head.Text = data.Rows[0]["head"] + " ";
            power.Text = data.Rows[0]["displacement"] + " ";
            displacement.Text = data.Rows[0]["power"] + " ";
            suctionPressure.Text = data.Rows[0]["suction_pressure"] + " ";
            runTime.Text = data.Rows[0]["running_time"] + " ";
            inPressure.Text = data.Rows[0]["in_pressure"] + " ";
            outPressure.Text = data.Rows[0]["out_pressure"] + " ";
        }
    }
}
