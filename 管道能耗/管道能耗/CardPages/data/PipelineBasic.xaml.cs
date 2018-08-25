using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : Page
    {
        DataTable data;

        public Page1()
        {
            InitializeComponent();
            PipeGrid();
            initTable();

        }
        //grid初始
        public void PipeGrid()
        {
            pipe_grid.IsReadOnly =true ;
        }
        //初始化表格
        public void initTable()
        {
            pipe_grid.SelectedIndex = 0;
            pipe_grid.ItemsSource = null;
            data = new PipeDAL().getPipeBaisc(new ProDAL().getNowPro());
            pipe_grid.ItemsSource =data.DefaultView;
            initTools();
            pipe_grid.SelectedIndex = -1;
            this.SelectRow.Text = "0";

            if (data.Rows.Count == 0)
            {
                SelectRow.Text = "0 ";
            }
        }

        //初始化tool
        public void initTools()
        {
            total.Text = "/"+data.Rows.Count;
        }
        //表格选择处理
        private void selectRow(object sender, SelectedCellsChangedEventArgs e)
        {
            this.SelectRow.Text = " "+(pipe_grid.SelectedIndex + 1);
        }

        public void pipbutton_add(object sender, RoutedEventArgs e)
        {
            PiP_Add pipadd = new PiP_Add();
            pipadd.ShowDialog();
            initTable();
        }
        public void pipbutton_edit(object sender, RoutedEventArgs e)
        {
            if (pipe_grid.SelectedIndex >= 0)
            {
                int projectid = int.Parse(data.Rows[pipe_grid.SelectedIndex]["project_id"] + " ");
                int pipeid = int.Parse(data.Rows[pipe_grid.SelectedIndex]["pipe_id"] + " ");
                PiP_Edit pipedit = new PiP_Edit(projectid, pipeid);
                pipedit.ShowDialog();
                initTable();
            }
            else
            {
                MessageBox.Show("没有选中项,无法编辑");
            }
        }

        public void pipbutton_dele(object sender, RoutedEventArgs e)
        {
            if (pipe_grid.SelectedIndex >= 0)
            {
                int projectid = int.Parse(data.Rows[pipe_grid.SelectedIndex]["project_id"] + " ");
                int pipeid = int.Parse(data.Rows[pipe_grid.SelectedIndex]["pipe_id"] + " ");
                PipeDAL pipeDAL = new PipeDAL();
                pipeDAL.delPipeData(projectid, pipeid);
                initTable();
            }
            else
            {
                MessageBox.Show("没有选中项,无法删除");
            }

        }

        public void start(object sender, RoutedEventArgs e)
        {
            pipe_grid.SelectedIndex = 0;
            this.SelectRow.Text = " " + (pipe_grid.SelectedIndex + 1);
        }
        public void end(object sender, RoutedEventArgs e)
        {
            pipe_grid.SelectedIndex = data.Rows.Count - 1;
            this.SelectRow.Text = " " + (pipe_grid.SelectedIndex + 1);
        }
        public void up(object sender, RoutedEventArgs e)
        {

            if (pipe_grid.SelectedIndex > 0)
            {
                pipe_grid.SelectedIndex--;
                this.SelectRow.Text = " " + (pipe_grid.SelectedIndex + 1);
                this.SelectRow.Text = " " + (pipe_grid.SelectedIndex + 1);
            }


        }
        public void down(object sender, RoutedEventArgs e)
        {
            if (pipe_grid.SelectedIndex < data.Rows.Count - 1)
            {
                pipe_grid.SelectedIndex++;
                this.SelectRow.Text = " " + (pipe_grid.SelectedIndex + 1);
                this.SelectRow.Text = " " + (pipe_grid.SelectedIndex + 1);
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
                        pipe_grid.SelectedIndex = select - 1;
                    }
                    else
                    {
                        MessageBox.Show("没有该行");
                    }
                }
                else
                {
                    MessageBox.Show("输入字符无效");
                    this.SelectRow.Text = " " + (pipe_grid.SelectedIndex + 1);
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
                PipeDAL dAL = new PipeDAL();
                Console.WriteLine(path);
                exportExcel exp = new exportExcel();
                exp.export(dAL.getPipeBaisc(new ProDAL().getNowPro()), path, "Pipe");
                MessageBox.Show("导出完成");
            }
        }

    }
}
