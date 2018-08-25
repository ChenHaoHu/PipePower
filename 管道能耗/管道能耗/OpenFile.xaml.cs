using BLL;
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
    /// OpenFile.xaml 的交互逻辑
    /// </summary>
    public partial class OpenFile : Window
    {
        DataTable data;
       public OpenFile()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true;
            ProDAL proDAL = new ProDAL();
             data = proDAL.getProData();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Height = 50;
                Color color =(Color)ColorConverter.ConvertFromString("white");
                SolidColorBrush brush = new SolidColorBrush(color);
                item.Background = brush;
                item.Content = data.Rows[i]["project_name"]+"      ("+ data.Rows[i]["project_number"] + ")";
                filelist.Items.Add(item);
            }
        
        }

        private void build_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ProDAL dal = new ProDAL();
            int id1 = dal.getNowPro();
            AddFile addFile = new AddFile();
            addFile.Title = "新建项目";
            addFile.ShowDialog();
            int id2 = dal.getNowPro();
            if (id1 != id2)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.ShowDialog();
                this.Close();
            }
            this.Show();
        }
        private void look_Click(object sender, RoutedEventArgs e)
        {
            if (filelist.SelectedIndex >= 0)
            {
                Console.WriteLine(data.Rows[filelist.SelectedIndex]["project_id"] + " ");
                FileShow fileShow = new FileShow(int.Parse(data.Rows[filelist.SelectedIndex]["project_id"] + " "));
                fileShow.ShowDialog();
            }
         
        }
        private void dele_Click(object sender, RoutedEventArgs e)
        {
            if (filelist.SelectedIndex >= 0)
            {
                new ProDAL().delProData(int.Parse(data.Rows[filelist.SelectedIndex]["project_id"] + " "));
                filelist.Items.Clear();
                ProDAL proDAL = new ProDAL();
                data = proDAL.getProData();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.Height = 50;
                    Color color = (Color)ColorConverter.ConvertFromString("white");
                    SolidColorBrush brush = new SolidColorBrush(color);
                    item.Background = brush;
                    item.Content = data.Rows[i]["project_name"] + "      (" + data.Rows[i]["project_number"] + ")";
                    filelist.Items.Add(item);
                }
            }
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            if (filelist.SelectedIndex >= 0)
            {
                int id = int.Parse(data.Rows[filelist.SelectedIndex]["project_id"] + " ");
                new ProDAL().changeNowPro(id);
                this.Hide();
                MainWindow mainWindow = new MainWindow();
                mainWindow.ShowDialog();
                this.Close();
            }
        }
    }
}
