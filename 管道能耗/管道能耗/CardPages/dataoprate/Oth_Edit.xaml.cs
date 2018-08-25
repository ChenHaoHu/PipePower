using DAL;
using BLL;
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
using System.Windows.Shapes;

namespace 管道能耗.CardPages.dataoprate
{
    /// <summary>
    /// Oth_Edit.xaml 的交互逻辑
    /// </summary>
    public partial class Oth_Edit : Window
    {
        int id;
        public Oth_Edit()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
       
        }

        public Oth_Edit(int id)
        {
            this.id = id;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.recoveryData(id);
        }
   private void Button_build(object sender, RoutedEventArgs e)
        {
            IsNumber Isnumber = new IsNumber();
            if (name.Text.Length != 0 && buildCost.Text.Length != 0 && recoveryN.Text.Length != 0 && operatingCost.Text.Length != 0 && oilCost.Text.Length != 0 && electricityCost.Text.Length != 0)
            {
                if (Isnumber.isNumber(buildCost.Text.Trim()) == false || Isnumber.isNumber(recoveryN.Text.Trim())==false|| Isnumber.isNumber(operatingCost.Text.Trim())==false|| Isnumber.isNumber(oilCost.Text.Trim()) == false || Isnumber.isNumber(electricityCost.Text.Trim()) == false)
                {
                    MessageBox.Show("输入格式有误");

                }
                else
                {
                    OtherDAL dal = new OtherDAL();
                    dal.delOtherData(id);
                    Other other = new Other(id, name.Text, buildCost.Text, recoveryN.Text, operatingCost.Text, oilCost.Text, electricityCost.Text);
                    dal.addOtherData(other);
                    MessageBox.Show("修改成功");
                    Close();
                    return;
                }

            }
            else { MessageBox.Show("请确保没有空白项");
            }
            

            
        }

        private void Button_cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void recoveryData(int id)
        {
            OtherDAL dal = new OtherDAL();
            DataTable data = dal.getSingleOtherData(id);
            otherId.Text = data.Rows[0]["other_id"] + " ";
            name.Text = data.Rows[0]["other_name"] + " ";
            buildCost.Text = data.Rows[0]["cost_build"] + " ";
            recoveryN.Text = data.Rows[0]["n_recovery"] + " ";
            operatingCost.Text = data.Rows[0]["cost_operating"] + " ";
            oilCost.Text = data.Rows[0]["cost_oil"] + " ";
            electricityCost.Text = data.Rows[0]["cost_electricity"] + " ";
        }
    }
}
