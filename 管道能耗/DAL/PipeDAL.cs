using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PipeDAL
    {

        //获取这个项目单个的管道资料
        public DataTable getSinglePipeData(int proId,int pipeid)
        {
            string sql = "select * from tb_pipe where project_id = @proId and pipe_id ="+pipeid;
            SqlParameter pms = new SqlParameter("@proId", proId);
            DataTable dt = SqlHelper.ExecuteDataTable(sql, pms);
            return dt;
        }


        //获取这个项目所有的管道资料
        public DataTable getPipeBaisc(int proId)
        {
            string sql = "select * from tb_pipe where project_id = @proId ";
            SqlParameter pms = new SqlParameter("@proId", proId);
            DataTable dt = SqlHelper.ExecuteDataTable(sql, pms);
            return dt;
        }


        //删除某个管道资料
        public bool delPipeData(int proId ,  int pipeid)
        {
            bool IsSuccess = false;
            StringBuilder sb = new StringBuilder();
            //sb.Append("BEGIN ");
            sb.Append("delete from tb_pipe where pipe_id=" + pipeid+"and project_id = "+proId);
            //sb.Append(" END;");
            if (SqlHelper.ExecuteNonQuery(sb.ToString()) > 0)
            {
                IsSuccess = true;
            }
            return IsSuccess;
        }


        public bool addPipeData(Pipe data)
        {
            bool IsSuccess = false;
            string sql = "insert into tb_pipe values(@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l,@m,@n,@o,@p)";
            SqlParameter[] spm =
          {
                new SqlParameter("@a",SqlDbType.Int),
                new SqlParameter("@b",SqlDbType.Int),
                new SqlParameter("@c",SqlDbType.VarChar),
                new SqlParameter("@d",SqlDbType.VarChar),
                new SqlParameter("@e",SqlDbType.VarChar),
                new SqlParameter("@f",SqlDbType.VarChar),
                new SqlParameter("@g",SqlDbType.VarChar),
                new SqlParameter("@h",SqlDbType.VarChar),
                new SqlParameter("@i",SqlDbType.VarChar),
                new SqlParameter("@j",SqlDbType.VarChar),
                new SqlParameter("@k",SqlDbType.VarChar),
                new SqlParameter("@l",SqlDbType.VarChar),
                new SqlParameter("@m",SqlDbType.VarChar),
                new SqlParameter("@n",SqlDbType.VarChar),
                new SqlParameter("@o",SqlDbType.VarChar),
                new SqlParameter("@p",SqlDbType.VarChar),
       

            };
            spm[0].Value = data.ProjectId;
            spm[1].Value = data.PipeId;
            spm[2].Value = data.Name;
            spm[3].Value = data.Start;
            spm[4].Value = data.End;
            spm[5].Value = data.Length;
            spm[6].Value = data.OuterDiameter;
            spm[7].Value = data.WallThickness;
            spm[8].Value = data.Depth;
            spm[9].Value = data.Trans;
            spm[10].Value = data.Insulation;
            spm[11].Value = data.TankTypeA;
            spm[12].Value = data.TankTypeB;
            spm[13].Value = data.TankCapacityA;
            spm[14].Value = data.TankCapacityB;
            spm[15].Value = data.MaximumPressure;

            if (SqlHelper.ExecuteNonQuery(sql, spm) > 0)
            {
                IsSuccess = true;
            }
            return IsSuccess;
        }

        //获取某个项目下管道编号的最大数
        public int getMaxPipeId(int proid)
        {
            string sql = "select  max(pipe_id) from tb_pipe where project_id = " + proid;
            int max = 1000; ;
            if (SqlHelper.ExecuteScalar(sql, null) != System.DBNull.Value)
            {
                max = (int)SqlHelper.ExecuteScalar(sql, null);
            }
            return max;
        }

    }
}
