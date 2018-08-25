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
    /// Oth_Add.xaml 的交互逻辑
    /// </summary>
    public partial class Oth_Add : Window
    {
        
        int otherid = 0;
        public Oth_Add()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
            OtherDAL otherDAL = new OtherDAL();
            otherid = otherDAL.getMaxOtherId()+1;
            otherId.Text = otherid + " ";
        }
       
         /*   public  bool IsNumber(string input)
            {
                double d = 0;
                if (double.TryParse(input, out d)) // TryParse返回true说明是数值
                    return true;
                else // 不是数值
                    return false;
            }*/



            private void Button_build(object sender, RoutedEventArgs e)
        {

            if (name.Text.Length != 0 && buildCost.Text.Length != 0 && recoveryN.Text.Length != 0 && operatingCost.Text.Length != 0 && oilCost.Text.Length != 0 && electricityCost.Text.Length != 0)
            {
                IsNumber Isnumber = new IsNumber();
                if (Isnumber.isNumber(buildCost.Text.Trim())==false|| Isnumber.isNumber(recoveryN.Text.Trim())==false || Isnumber.isNumber(operatingCost.Text.Trim()) == false || Isnumber.isNumber(oilCost.Text.Trim()) == false || Isnumber.isNumber(electricityCost.Text.Trim()) == false)
                {
                    MessageBox.Show("输入格式有误");

                }
                else
                {
                    Other other = new Other(otherid, name.Text, buildCost.Text, recoveryN.Text, operatingCost.Text, oilCost.Text, electricityCost.Text);
                    OtherDAL otherDAL = new OtherDAL();
                    otherDAL.addOtherData(other);
                    MessageBox.Show("添加成功");
                    Close();
                    return;
                }

            }
            else
            { MessageBox.Show("请确保没有空白项");
            }

          
        }

        private void Button_cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
