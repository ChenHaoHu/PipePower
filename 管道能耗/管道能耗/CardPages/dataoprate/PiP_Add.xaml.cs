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
    /// PiP_Add.xaml 的交互逻辑
    /// </summary>
    public partial class PiP_Add : Window
    {
        int proid = 0;
        int pipeid = 0;
        public PiP_Add()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ProDAL proDal = new ProDAL();
            PipeDAL pipeDal = new PipeDAL();
            proid = proDal.getNowPro();
            pipeid = pipeDal.getMaxPipeId(proid) + 1;
            pipe_id.Text = pipeid+" ";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
         
            Pipe pipe = new Pipe(proid, pipeid, start_spot.Text, end_spot.Text, pipe_name.Text, pipe_length.Text, pipe_outer_diameter.Text, wall_thickness.Text, pipe_depth.Text, transport_medium.Text, insulation_materials.Text, tank_type_a.Text, tank_type_b.Text, tank_capacity_a.Text, tank_capacity_b.Text, maximum_pressure.Text);
            IsNumber Isnumber = new IsNumber();
            if (Isnumber.isNumber(pipe_length.Text.Trim()) == false || Isnumber.isNumber(pipe_outer_diameter.Text.Trim()) == false || Isnumber.isNumber(wall_thickness.Text.Trim()) == false || Isnumber.isNumber(pipe_depth.Text.Trim()) == false  ||Isnumber.isNumber(tank_capacity_a.Text.Trim()) == false || Isnumber.isNumber(tank_capacity_b.Text.Trim()) == false || Isnumber.isNumber(maximum_pressure.Text.Trim()) == false)
            {
                MessageBox.Show("输格式入有误");

            }
            else
            {
                PipeDAL pipeDAL = new PipeDAL();
                pipeDAL.addPipeData(pipe);
                MessageBox.Show("添加成功");
                Close();
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
