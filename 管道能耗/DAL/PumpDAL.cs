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
    public class PumpDAL
    {
   
        //获取编号最大数
        public int getMaxPumpId()
        {
            string sql = "select  max(pump_id) from tb_pump";
            int max = 1000; ;
            if (SqlHelper.ExecuteScalar(sql, null) != System.DBNull.Value)
            {
                max = (int)SqlHelper.ExecuteScalar(sql, null);
            }
            return max;
        }


        //获取当个泵资料
        public DataTable getSinglePumpData(int id)
        {
            string sql = "select * from tb_pump where pump_id=" + id;
        
            DataTable dt = SqlHelper.ExecuteDataTable(sql, null);
            return dt;
        }





        //获取所有泵资料
        public DataTable getPumpData()
        {
            string sql = "select * from tb_pump";
            DataTable dt = SqlHelper.ExecuteDataTable(sql, null);
            return dt;
        }


        //删除某个泵资料
        public bool delPumData(int pumpId)
        {
            bool IsSuccess = false;
            StringBuilder sb = new StringBuilder();
            //sb.Append("BEGIN ");
            sb.Append("delete from tb_pump where pump_id=" + pumpId);
            //sb.Append(" END;");
            if (SqlHelper.ExecuteNonQuery(sb.ToString()) > 0)
            {
                IsSuccess = true;
            }
            return IsSuccess;
        }

        //添加泵资料项
        public bool addPumpData(Pump data)
        {
            bool IsSuccess = false;
            string sql = "insert into tb_pump values(@a,@b,@c,@d,@e,@f,@g,@h,@i)";
            SqlParameter[] spm =
          {
                new SqlParameter("@a",SqlDbType.Int),
                new SqlParameter("@b",SqlDbType.VarChar),
                new SqlParameter("@c",SqlDbType.VarChar),
                new SqlParameter("@d",SqlDbType.VarChar),
                new SqlParameter("@e",SqlDbType.VarChar),
                new SqlParameter("@f",SqlDbType.VarChar),
                new SqlParameter("@g",SqlDbType.VarChar),
                new SqlParameter("@h",SqlDbType.VarChar),
                new SqlParameter("@i",SqlDbType.VarChar),

            };
            spm[0].Value = data.PumpId;
            spm[1].Value = data.Name;
            spm[2].Value = data.Head;
            spm[3].Value = data.Displacement;
            spm[4].Value = data.Power;
            spm[5].Value = data.SuctionPressure;
            spm[6].Value = data.RunTime;
            spm[7].Value = data.InPressure;
            spm[8].Value = data.OutPressure;
      

            if (SqlHelper.ExecuteNonQuery(sql, spm) > 0)
            {
                IsSuccess = true;
            }
            return IsSuccess;
        }

    }

}
