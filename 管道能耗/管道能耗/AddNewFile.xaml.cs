using BLL;
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

namespace 管道能耗
{
    /// <summary>
    /// AddFile.xaml 的交互逻辑
    /// </summary>
    public partial class AddFile : Window
    {
        public AddFile()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true;
        }

        private void Button_build(object sender, RoutedEventArgs e)
        {
            //获取输入框信息
            string number  = this.number.Text;
            string name = this.name.Text;
            string responsible = this.responsible.Text;
            Console.WriteLine(number + " " + name + " " + responsible);
            //获取时间信息
            Time time = new Time();
            string now = time.getNowTime();
            if (number.Length != 0&& name.Length!=0&&responsible.Length!=0)
            {
                //插入数据并标识现在的项目的id是多少
                ProDAL prodal = new ProDAL();
                int proid = prodal.getBigProId();
                prodal.addPoData(new Project(proid + 1, number, name, responsible, now));
                //改变缓存的值
                prodal.changeNowPro(proid + 1);
                MessageBox.Show("成功创建" + name + "项目");
                this.Close();
                return;
            }
            MessageBox.Show("请确保没有空白项");
            
        }

        private void Button_cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
