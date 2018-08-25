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
using BLL;

namespace 管道能耗.CardPages.dataoprate
{
    /// <summary>
    /// PiP_Edit.xaml 的交互逻辑
    /// </summary>
    public partial class PiP_Edit : Window
    {
        int proid;
        int pipeid;
        public PiP_Edit()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        public PiP_Edit(int proid, int pipeid)
        {
            InitializeComponent();
            this.proid = proid;
            this.pipeid = pipeid;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.recoveryData(proid, pipeid);
        }
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //save
            IsNumber Isnumber = new IsNumber();
            Pipe pipe = new Pipe(proid,pipeid,start_spot.Text,end_spot.Text,pipe_name.Text,pipe_length.Text,pipe_outer_diameter.Text,wall_thickness.Text,pipe_depth.Text, transport_medium.Text,insulation_materials.Text,tank_type_a.Text,tank_type_b.Text,tank_capacity_a.Text,tank_capacity_b.Text,maximum_pressure.Text);
            if (Isnumber.isNumber(pipe_length.Text.Trim()) == false || Isnumber.isNumber(pipe_outer_diameter.Text.Trim()) == false || Isnumber.isNumber(wall_thickness.Text.Trim()) == false || Isnumber.isNumber(pipe_depth.Text.Trim()) == false || Isnumber.isNumber(tank_capacity_a.Text.Trim()) == false || Isnumber.isNumber(tank_capacity_b.Text.Trim()) == false || Isnumber.isNumber(maximum_pressure.Text.Trim()) == false)
            {
                MessageBox.Show("输入格式有误");

            }
            else
            {
                PipeDAL pipeDAL = new PipeDAL();
                pipeDAL.delPipeData(proid, pipeid);
                pipeDAL.addPipeData(pipe);
                MessageBox.Show("修改成功");
                Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //cancel
            Close();
        }
        public void recoveryData(int proid, int pipeid)
        {
            PipeDAL dal = new PipeDAL();
            DataTable data =  dal.getSinglePipeData(proid, pipeid);
            Console.WriteLine(data.Rows[0]["pipe_id"]);
            pipe_id.Text = data.Rows[0]["pipe_id"]+" ";
            start_spot.Text = data.Rows[0]["start_spot"] + " ";
            end_spot.Text = data.Rows[0]["end_spot"] + " ";
            pipe_name.Text = data.Rows[0]["pipe_name"] + " ";
            pipe_length.Text = data.Rows[0]["pipe_length"] + " ";
            pipe_outer_diameter.Text = data.Rows[0]["pipe_outer_diameter"] + " ";
            wall_thickness.Text = data.Rows[0]["wall_thickness"] + " ";
            pipe_depth.Text = data.Rows[0]["pipe_depth"] + " ";
            transport_medium.Text = data.Rows[0]["transport_medium"] + " ";
            insulation_materials.Text = data.Rows[0]["insulation_materials"] + " ";
            tank_type_a.Text = data.Rows[0]["tank_type_a"] + " ";
            tank_capacity_a.Text = data.Rows[0]["tank_capacity_a"] + " ";
            tank_type_b.Text = data.Rows[0]["tank_type_b"] + " ";
            tank_capacity_b.Text = data.Rows[0]["tank_capacity_b"] + " ";
            maximum_pressure.Text = data.Rows[0]["maximum_pressure"] + " ";
       
        }
        
    }
}
