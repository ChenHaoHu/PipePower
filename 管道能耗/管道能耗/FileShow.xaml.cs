using DAL;
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
    /// FileShow.xaml 的交互逻辑
    /// </summary>
    public partial class FileShow : Window
    {
        public FileShow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true;
        }
        public FileShow(int id)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true;
            ProDAL proDAL = new ProDAL();
            DataTable data = proDAL.getSingleProData(id);
            number.Text = data.Rows[0]["project_number"] +" ";
            name.Text = data.Rows[0]["project_name"] + " ";
            responsible.Text = data.Rows[0]["responsible"] + " ";
            date.Text = data.Rows[0]["build_date"] + " ";

        }
    }
}
