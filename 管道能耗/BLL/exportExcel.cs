using MSExcel = Microsoft.Office.Interop.Excel;

using System.IO;
using System.Reflection;
using System;
using Microsoft.CSharp;
using System.Collections.Generic;
using System.Data;
using Microsoft.Office.Interop.Excel;

namespace BLL
{
   public class exportExcel
    {


        public void export(System.Data.DataTable data,string path,string type)

        {
            MSExcel.Application excel;              //Excel应用程序变量
            MSExcel.Workbook excelDoc;                     //Excel文档变量
            excel = new MSExcel.ApplicationClass();    //初始化


       int rowIndex = 0;
        int colIndex = 0;


            //如果已存在，则删除

            if (File.Exists((string)path))

            {
                File.Delete((string)path);

            }

            //由于使用的是COM库，因此有许多变量需要用Nothing代替

            Object Nothing = Missing.Value;

            excelDoc = excel.Workbooks.Add(Nothing);

            //使用第一个工作表作为插入数据的工作表

            MSExcel.Worksheet xSt = (MSExcel.Worksheet)excelDoc.Sheets[1];

       

            int row = 0;
            int co = 0;
            if (type.Equals("Other"))
            {
                List<string> title = new List<string>();
                title.Add("编号");
                title.Add("方案名");
                title.Add("油罐建设费用");
                title.Add("投资年回收系数");
                title.Add("油罐经营费用");
                title.Add("油品差价");
                title.Add("电费");

                rowIndex = data.Rows.Count + 1;
                colIndex = title.Count + 1;
                foreach (string temp in title)
                {
                    co++;
                    combineTransverse(1, co, co, temp, excel, xSt);
                }

                for (int i= 0; i< data.Rows.Count; i++)
                {
                    combineTransverse(2 + row, 1, 1,data.Rows[i]["other_id"] +" ", excel, xSt);
                    combineTransverse(2 + row, 2, 2, data.Rows[i]["other_name"] + " ", excel, xSt);
                    combineTransverse(2 + row, 3, 3, data.Rows[i]["cost_build"] + " ", excel, xSt);
                    combineTransverse(2 + row, 4, 4, data.Rows[i]["n_recovery"] + " ", excel, xSt);
                    combineTransverse(2 + row, 5, 5, data.Rows[i]["cost_operating"] + " ", excel, xSt);
                    combineTransverse(2 + row, 6, 6, data.Rows[i]["cost_oil"] + " ", excel, xSt);
                    combineTransverse(2 + row, 7, 7, data.Rows[i]["cost_electricity"] + " ", excel, xSt);

                    //怎么行数 20180715
                    row++;
                }
              
            }
            if (type.Equals("Oils"))
            {
       
                List<string> title = new List<string>();
                title.Add("编号");
                title.Add("油品名称");
                title.Add("密度");
                title.Add("运动粘度");
                title.Add("质量流量");
                title.Add("年输量");
                title.Add("体积浓度");


                rowIndex = data.Rows.Count + 1;
                colIndex = title.Count + 1;
                foreach (string temp in title)
                {
                    co++;
                    combineTransverse(1, co, co, temp, excel, xSt);
                }
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    combineTransverse(2 + row, 1, 1, data.Rows[i]["oils_id"] + " ", excel, xSt);
                    combineTransverse(2 + row, 2, 2, data.Rows[i]["oils_name"] + " ", excel, xSt);
                    combineTransverse(2 + row, 3, 3, data.Rows[i]["oils_density"] + " ", excel, xSt);
                    combineTransverse(2 + row, 4, 4, data.Rows[i]["oils_viscosity"] + " ", excel, xSt);
                    combineTransverse(2 + row, 5, 5, data.Rows[i]["mass_flow"] + " ", excel, xSt);
                    combineTransverse(2 + row, 6, 6, data.Rows[i]["output_year"] + " ", excel, xSt);
                    combineTransverse(2 + row, 7, 7, data.Rows[i]["volume_concentration"] + " ", excel, xSt);

                    //怎么行数 20180715
                    row++;
                }

            }
            if (type.Equals("Soil"))
            {
                List<string> title = new List<string>();
                title.Add("编号");
                title.Add("类型名");
                title.Add("埋深温度");
                title.Add("高层差");

                rowIndex = data.Rows.Count + 1;
                colIndex = title.Count + 1;
                foreach (string temp in title)
                {
                    co++;
                    combineTransverse(1, co, co, temp, excel, xSt);
                }
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    combineTransverse(2 + row, 1, 1, data.Rows[i]["soil_id"] + " ", excel, xSt);
                    combineTransverse(2 + row, 2, 2, data.Rows[i]["soil_name"] + " ", excel, xSt);
                    combineTransverse(2 + row, 3, 3, data.Rows[i]["soil_temp"] + " ", excel, xSt);
                    combineTransverse(2 + row, 4, 4, data.Rows[i]["soil_difference"] + " ", excel, xSt);

                    //怎么行数 20180715
                    row++;
                }

            }
     
            if (type.Equals("Pump"))
            {
                List<string> title = new List<string>();
                title.Add("编号");
                title.Add("泵类型");
                title.Add("扬程(m)");
                title.Add("质量(万吨/年)");
                title.Add("泵排量(m3/s)");
                title.Add("泵吸入压力(MPa)");
                title.Add("泵运行时间(s)");
                title.Add("进站压力(Pa)");
                title.Add("出站压力(Pa)");

                rowIndex = data.Rows.Count + 1;
                colIndex = title.Count + 1;
                foreach (string temp in title)
                {
                    co++;
                    combineTransverse(1, co, co, temp, excel, xSt);
                }
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    combineTransverse(2 + row, 1, 1, data.Rows[i]["pump_id"] + " ", excel, xSt);
                    combineTransverse(2 + row, 2, 2, data.Rows[i]["name"] + " ", excel, xSt);
                    combineTransverse(2 + row, 3, 3, data.Rows[i]["head"] + " ", excel, xSt);
                    combineTransverse(2 + row, 4, 4, data.Rows[i]["displacement"] + " ", excel, xSt);
                    combineTransverse(2 + row, 5, 5, data.Rows[i]["power"] + " ", excel, xSt);
                    combineTransverse(2 + row, 6, 6, data.Rows[i]["suction_pressure"] + " ", excel, xSt);
                    combineTransverse(2 + row, 7, 7, data.Rows[i]["running_time"] + " ", excel, xSt);
                    combineTransverse(2 + row, 8, 8, data.Rows[i]["in_pressure"] + " ", excel, xSt);
                    combineTransverse(2 + row, 9, 9, data.Rows[i]["out_pressure"] + " ", excel, xSt);

                    //怎么行数 20180715
                    row++;
                }

            }
            if (type.Equals("Pipe"))
            {
                List<string> title = new List<string>();
                title.Add("编号");
                title.Add("起点名称");
                title.Add("终点名称");
                title.Add("管道名称");
                title.Add("管道长度(km)");
                title.Add("管道外径(mm)");
                title.Add("管道壁厚(mm)");
                title.Add("管道埋深(m)");
                title.Add("输送介质");
                title.Add("保温材质");
                title.Add("A油罐类型");
                title.Add("A油罐容量(m3)");
                title.Add("B油罐类型");
                title.Add("B油罐容量(m3)");
                title.Add("管道最大承压(Pa)");
          

                rowIndex = data.Rows.Count + 1;
                colIndex = title.Count + 1;
                foreach (string temp in title)
                {
                    co++;
                    combineTransverse(1, co, co, temp, excel, xSt);
                }

                for (int i = 0; i < data.Rows.Count; i++)
                {
                    combineTransverse(2 + row, 1, 1, data.Rows[i]["pipe_id"] + " ", excel, xSt);
                    combineTransverse(2 + row, 2, 2, data.Rows[i]["pipe_name"] + " ", excel, xSt);
                    combineTransverse(2 + row, 3, 3, data.Rows[i]["start_spot"] + " ", excel, xSt);
                    combineTransverse(2 + row, 4, 4, data.Rows[i]["end_spot"] + " ", excel, xSt);
                    combineTransverse(2 + row, 5, 5, data.Rows[i]["pipe_length"] + " ", excel, xSt);
                    combineTransverse(2 + row, 6, 6, data.Rows[i]["pipe_outer_diameter"] + " ", excel, xSt);
                    combineTransverse(2 + row, 7, 7, data.Rows[i]["wall_thickness"] + " ", excel, xSt);
                    combineTransverse(2 + row, 8, 8, data.Rows[i]["pipe_depth"] + " ", excel, xSt);
                    combineTransverse(2 + row, 9, 9, data.Rows[i]["transport_medium"] + " ", excel, xSt);
                    combineTransverse(2 + row, 10, 10, data.Rows[i]["insulation_materials"] + " ", excel, xSt);
                    combineTransverse(2 + row, 11, 11, data.Rows[i]["tank_type_a"] + " ", excel, xSt);
                    combineTransverse(2 + row, 12, 12, data.Rows[i]["tank_type_b"] + " ", excel, xSt);
                    combineTransverse(2 + row, 13, 13, data.Rows[i]["tank_capacity_a"] + " ", excel, xSt);
                    combineTransverse(2 + row, 14, 14, data.Rows[i]["tank_capacity_b"] + " ", excel, xSt);
                    combineTransverse(2 + row, 15, 15, data.Rows[i]["maximum_pressure"] + " ", excel, xSt);

                    //怎么行数 20180715
                    row++;
                }

            }
            if (type.Equals("Wateranalyze"))
            {
                List<string> title = new List<string>();
                title.Add("油品名称");
                title.Add("前站出站压力");
                title.Add("末站进站压力");
                title.Add("沿程摩阻");
                title.Add("管道总压降");
                title.Add("斜率");


                rowIndex = data.Rows.Count + 1;
                colIndex = title.Count + 1;
                foreach (string temp in title)
                {
                    co++;
                    combineTransverse(1, co, co, temp, excel, xSt);
                }

                for (int i = 0; i < data.Rows.Count; i++)
                {
                    combineTransverse(2 + row, 1, 1, data.Rows[i]["油品名称"] + " ", excel, xSt);
                    combineTransverse(2 + row, 2, 2, data.Rows[i]["前站出站压力"] + " ", excel, xSt);
                    combineTransverse(2 + row, 3, 3, data.Rows[i]["末站进站压力"] + " ", excel, xSt);
                    combineTransverse(2 + row, 4, 4, data.Rows[i]["沿程摩阻"] + " ", excel, xSt);
                    combineTransverse(2 + row, 5, 5, data.Rows[i]["管道总压降"] + " ", excel, xSt);
                    combineTransverse(2 + row, 6, 6, data.Rows[i]["斜率"] + " ", excel, xSt);

                    //怎么行数 20180715
                    row++;
                }

            }
            if (type.Equals("Wipeanalyze"))
            {
                List<string> title = new List<string>();
                title.Add("管道名");
                title.Add("扬程");
                title.Add("工作流量");


                rowIndex = data.Rows.Count + 1;
                colIndex = title.Count + 1;
                foreach (string temp in title)
                {
                    co++;
                    combineTransverse(1, co, co, temp, excel, xSt);
                }

                for (int i = 0; i < data.Rows.Count; i++)
                {
                    combineTransverse(2 + row, 1, 1, data.Rows[i]["管道名"] + " ", excel, xSt);
                    combineTransverse(2 + row, 2, 2, data.Rows[i]["扬程"] + " ", excel, xSt);
                    combineTransverse(2 + row, 3, 3, data.Rows[i]["工作流量"] + " ", excel, xSt);

                    //怎么行数 20180715
                    row++;
                }

            }
            // 
            //绘制边框 
            // 
            xSt.get_Range(excel.Cells[1, 1], excel.Cells[rowIndex, colIndex - 1]).Borders.LineStyle = 1;
            xSt.get_Range(excel.Cells[1, 1], excel.Cells[rowIndex, colIndex - 1]).Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick;//设置左边线加粗 
            xSt.get_Range(excel.Cells[1, 1], excel.Cells[rowIndex, colIndex - 1]).Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick;//设置上边线加粗 
            xSt.get_Range(excel.Cells[1, 1], excel.Cells[rowIndex, colIndex - 1]).Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick;//设置右边线加粗 
            xSt.get_Range(excel.Cells[1, 1], excel.Cells[rowIndex, colIndex - 1]).Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick;//设置下边线加粗


            //设置报表表格为最适应宽度 
            // 
            xSt.get_Range(excel.Cells[1, 1], excel.Cells[rowIndex, colIndex]).Select();
            xSt.get_Range(excel.Cells[1, 1], excel.Cells[rowIndex, colIndex]).Columns.AutoFit();

            //WdSaveFormat为Excel文档的保存格式

            object format = MSExcel.XlFileFormat.xlWorkbookDefault;

            //将excelDoc文档对象的内容保存为XLSX文档

            excelDoc.SaveAs(path, format, Nothing, Nothing, Nothing, Nothing, MSExcel.XlSaveAsAccessMode.xlExclusive, Nothing, Nothing, Nothing, Nothing, Nothing);

            //关闭excelDoc文档对象

            excelDoc.Close(Nothing, Nothing, Nothing);

            //关闭excelApp组件对象

            excel.Quit();

            Console.WriteLine(path + " 创建完毕！");

          
        }


        //横向联合
        public static void combineTransverse(int row, int begin, int end, string text, MSExcel.Application excel, MSExcel.Worksheet xSt)
        {
            excel.Cells[row, begin] = text;
            //设置整个报表的标题为跨列居中 
            // 
            xSt.get_Range(excel.Cells[row, begin], excel.Cells[row, end]).Select();
            xSt.get_Range(excel.Cells[row, begin], excel.Cells[row, end]).HorizontalAlignment = XlHAlign.xlHAlignCenterAcrossSelection;

        }

        //多向联合
        public static void combineTransverseVertical(int begin1, int end1, int begin2, int end2, string text, MSExcel.Application excel, MSExcel.Worksheet xSt)
        {
            excel.Cells[begin1, end1] = text;
            //设置整个报表的标题为跨列居中 
            // XlHAlign.xlHAlignCenterAcrossSelection;
            // xSt.get_Range(excel.Cells[begin1, end1], excel.Cells[begin1, end2]).Select();
            //xSt.get_Range(excel.Cells[begin1, end1], excel.Cells[begin1, end2]).VerticalAlignment = XlHAlign.xlHAlignCenterAcrossSelection;
            xSt.get_Range(excel.Cells[begin1, end1], excel.Cells[begin2, end1]).MergeCells = true;
            xSt.get_Range(excel.Cells[begin1, end1], excel.Cells[begin2, end2]).MergeCells = true;
            xSt.get_Range(excel.Cells[begin1, end1], excel.Cells[begin2, end2]).WrapText = true;//  

        }
    }
}
