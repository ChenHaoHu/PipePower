using DAL;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Media.Imaging;


namespace 管道能耗
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ProDAL proDAL = new ProDAL();

        public MainWindow()
        {
            InitializeComponent();
        }

        //菜单打开操作
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog opf = new OpenFileDialog();
            //opf.Filter = "Excel文件|*.xlsx|97-03Excel|*.xls";
            //opf.ShowDialog();
            OpenFile openFile = new OpenFile();
            openFile.ShowDialog();
        }
        //另存为操作
        private void Osave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog osfd = new SaveFileDialog();
            osfd.Filter = "Excel文件|*.xlsx|97-03Excel|*.xls";
            osfd.ShowDialog();
        }
        //退出操作
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        //左边导航显示在右边
        private void Treeview_Selected(object sender, RoutedEventArgs e)
        {

            TreeViewItem treeViewItem = (TreeViewItem)sender;
            //管道基础参数按钮
            if ((string)treeViewItem.Header == "管道基础参数")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/data/PipelineBasic.xaml"));

            }

            // 油品物性参数按钮
            if ((string)treeViewItem.Header == "油品物性参数")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/data/PhysicalProperty.xaml"));

            }

            // 土壤环境温度
            if ((string)treeViewItem.Header == "土壤环境参数")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/data/SoilAmbient.xaml"));

            }

            // 泵站参数
            if ((string)treeViewItem.Header == "泵站参数")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/data/PumpParameters.xaml"));

            }
            //其他参数
            if ((string)treeViewItem.Header == "其他参数")
            {
                frame.Navigate(new Uri("pack://application:,,,/CardPages/data/OthersData.xaml"));
            }

            // 水力特性分析
            if ((string)treeViewItem.Header == "水力特性分析")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/operation/Wateranalyze.xaml"));


            }

            // 管道工作特性
            if ((string)treeViewItem.Header == "管道工作特性")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/operation/Wipeanalyze.xaml"));

            }

            // 最优循环次数
            if ((string)treeViewItem.Header == "最优循环次数")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/programme/Cyclicnum.xaml"));

            }

            // 混油量
            if ((string)treeViewItem.Header == "混油量")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/programme/MixedOilNum.xaml"));

            }

            // 运行能耗计算
            if ((string)treeViewItem.Header == "运行能耗计算")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/programme/Energycalcu.xaml"));

            }
            //混油切割方案
            if ((string)treeViewItem.Header == "混油切割方案")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/programme/MixedOilCutting.xaml"));

            }
            //泵站布置方案
            if ((string)treeViewItem.Header == "泵站布置方案")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/programme/PumpSet.xaml"));

            }
            //首站开泵方案
            if ((string)treeViewItem.Header == "首站开泵方案")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/programme/PumpOpen.xaml"));

            }

            // 帮助文档
            if ((string)treeViewItem.Header == "帮助文档")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/help/HelpFile.xaml"));
            }

            //// 关于我们
            //if ((string)treeViewItem.Header == "关于我们")
            //{

            //    frame.Navigate(new Uri("pack://application:,,,/CardPages/help/Aboutus.xaml"));

            //}

        }
        //上方导航显示在右边
        private void Menuitem_Selected(object sender, RoutedEventArgs e)
        {
            MenuItem menuitem = (MenuItem)sender;

            //管道基础参数按钮
            if ((string)menuitem.Header == "管道基础参数")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/data/PipelineBasic.xaml"));

            }

            // 油品物性参数按钮
            if ((string)menuitem.Header == "油品物性参数")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/data/PhysicalProperty.xaml"));

            }

            // 土壤环境温度
            if ((string)menuitem.Header == "土壤环境温度")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/data/SoilAmbient.xaml"));

            }

            // 泵站参数
            if ((string)menuitem.Header == "泵站参数")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/data/PumpParameters.xaml"));

            }
            //其他参数
            if ((string)menuitem.Header == "其他参数")
            {
                frame.Navigate(new Uri("pack://application:,,,/CardPages/data/OthersData.xaml"));
            }

            // 水力特性分析
            if ((string)menuitem.Header == "水力特性分析")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/operation/Wateranalyze.xaml"));

            }

            // 管道工作特性
            if ((string)menuitem.Header == "管道工作特性")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/operation/Wipeanalyze.xaml"));
            }

            // 最优循环次数
            if ((string)menuitem.Header == "最优循环次数")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/programme/Cyclicnum.xaml"));

            }

            // 混油量
            if ((string)menuitem.Header == "混油量")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/programme/MixedOilNum.xaml"));

            }

            // 运行能耗计算
            if ((string)menuitem.Header == "运行能耗计算")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/programme/Energycalcu.xaml"));

            }
            //混油切割方案
            if ((string)menuitem.Header == "混油切割方案")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/programme/MixedOilCutting.xaml"));

            }
            //泵站布置方案
            if ((string)menuitem.Header == "泵站布置方案")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/programme/PumpSet.xaml"));

            }
            //首站开泵方案
            if ((string)menuitem.Header == "首站开泵方案")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/programme/PumpOpen.xaml"));

            }

            // 帮助文档
            if ((string)menuitem.Header == "帮助文档")
            {

                frame.Navigate(new Uri("pack://application:,,,/CardPages/help/HelpFile.xaml"));

            }

            //// 关于我们
            //if ((string)menuitem.Header == "关于我们")
            //{

            //    frame.Navigate(new Uri("pack://application:,,,/CardPages/help/Aboutus.xaml"));

            //}
        }
        //新建操作
        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            AddFile addFile = new AddFile();
            addFile.Title = "新建项目";
            addFile.ShowDialog();
        }

        private void treeview_aboutour_Selected(object sender, RoutedEventArgs e)
        {
            AboutUs about = new AboutUs();
            about.Title = "关于";
            about.ShowDialog();


        }

        private void treeview_helpfile_Selected(object sender, RoutedEventArgs e)
        {
            HelpFile help = new HelpFile();
            help.Title = "帮助文档";
            help.ShowDialog();
        }


        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditFile edit = new EditFile();
            edit.Title = "编辑项目";
            edit.ShowDialog();
        }

    }
}
