using BLL;
using DAL;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using 管道能耗.CardPages.dataoprate;

namespace 管道能耗
{
    /// <summary>
    /// Page2.xaml 的交互逻辑
    /// </summary>
    public partial class Page2 : Page
    {
        DataTable data;
        public Page2()
        {
            InitializeComponent();
            initTable();

            
        }

        public void initTable()
        {
            table.SelectedIndex = 0;
            table.IsReadOnly = true;
            table.ItemsSource = null;
            data = new OilsDAL().getOilosData();
            table.ItemsSource = data.DefaultView;
            initTools();
            table.SelectedIndex = -1;
            this.SelectRow.Text = "0";

            if (data.Rows.Count == 0)
            {
                SelectRow.Text = "0 ";
            }
        }

        //初始化tool
        public void initTools()
        {
            total.Text = "/" + data.Rows.Count;
        }

        public void button_add(object sender, RoutedEventArgs e)
        {
            Phy_Add phy_Add = new Phy_Add();
            phy_Add.ShowDialog();
            initTable();
        }
        public void button_edit(object sender, RoutedEventArgs e)
        {

            if (table.SelectedIndex >= 0)
            {
                Phy_Edit phy_Edit = new Phy_Edit(int.Parse(data.Rows[table.SelectedIndex]["oils_id"] + " "));
                phy_Edit.ShowDialog();
                initTable();
            }
            else
            {
                MessageBox.Show("没有选中项,无法编辑");
            }
        }
        public void button_dele(object sender, RoutedEventArgs e)
        {
            if (table.SelectedIndex >= 0)
            {
                int oilsid = int.Parse(data.Rows[table.SelectedIndex]["oils_id"] + " ");
                OilsDAL dal = new OilsDAL();
                dal.delOilsData(oilsid);
                initTable();
            }
            else
            {
                MessageBox.Show("没有选中项,无法删除");
            }
        }
        public void start(object sender, RoutedEventArgs e)
        {
            table.SelectedIndex = 0;
            this.SelectRow.Text = " " + (table.SelectedIndex + 1);
        }
        public void end(object sender, RoutedEventArgs e)
        {
            table.SelectedIndex = data.Rows.Count - 1;
            this.SelectRow.Text = " " + (table.SelectedIndex + 1);
        }
        public void up(object sender, RoutedEventArgs e)
        {

            if (table.SelectedIndex > 0)
            {
                table.SelectedIndex--;
                this.SelectRow.Text = " " + (table.SelectedIndex + 1);
                this.SelectRow.Text = " " + (table.SelectedIndex + 1);
            }


        }
        public void down(object sender, RoutedEventArgs e)
        {
            if (table.SelectedIndex < data.Rows.Count - 1)
            {
                table.SelectedIndex++;
                this.SelectRow.Text = " " + (table.SelectedIndex + 1);
                this.SelectRow.Text = " " + (table.SelectedIndex + 1);
            }

        }

        private void SelectRow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                if (IsOnlyNumber(SelectRow.Text))
                {
                    int select = int.Parse(SelectRow.Text);
                    if (select <= data.Rows.Count)
                    {
                        table.SelectedIndex = select - 1;
                    }
                    else
                    {
                        MessageBox.Show("没有该行");
                    }
                }
                else
                {
                    MessageBox.Show("输入字符无效");
                    this.SelectRow.Text = " " + (table.SelectedIndex + 1);
                }

            }
        }

        private static bool IsOnlyNumber(string value)
        {
            Regex r1 = new Regex(@"^[0-9]+$");
            Regex r2 = new Regex(@"^\s[0-9]+$");

            if (!r1.Match(value).Success)
            {
                return r2.Match(value).Success;
            }

            return r1.Match(value).Success;
        }

        public void button_export(object sender, RoutedEventArgs e)
        {
            string path = string.Empty;
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "Files (*.xlsx*)|*.xls*"//如果需要筛选txt文件（"Files (*.txt)|*.txt"）
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                path = openFileDialog.FileName;
            }
            if (path.Length > 0)
            {
                OilsDAL dAL = new OilsDAL();
                Console.WriteLine(path);
                exportExcel exp = new exportExcel();
                exp.export(dAL.getOilosData(), path, "Oils");
                MessageBox.Show("导出完成");
            }
        }

        private void table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
