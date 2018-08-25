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
using System.Windows.Shapes;

namespace 管道能耗
{
    /// <summary>
    /// EditFile.xaml 的交互逻辑
    /// </summary>
    public partial class EditFile : Window
    {
        int id;
        ProDAL dal = new ProDAL();

        public EditFile()
        {
            InitializeComponent();
            id = dal.getNowPro();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.recoveryData(id);

        }
       

        private void Button_build(object sender, RoutedEventArgs e)
        {
            Project pro = new Project(id,project_number.Text,project_name.Text,responsible.Text,(new Time()).getNowTime());
           
                dal.delProData(id);
                dal.addPoData(pro);
                MessageBox.Show("修改成功");
                Close();
            
        }

        private void Button_cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void recoveryData(int id)
        {
          
            DataTable data = dal.getSingleProData(id);
            project_name.Text = data.Rows[0]["project_name"] + " ";
            project_number.Text = data.Rows[0]["project_number"] + " ";
            responsible.Text = data.Rows[0]["responsible"] + " ";
        }
    }
}
