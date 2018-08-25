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
    public class OtherDAL
    {

        //获取所有的其他资料
        public DataTable getOtherData()
        {
            string sql = "select * from tb_other";
            DataTable dt = SqlHelper.ExecuteDataTable(sql, null);
            return dt;
        }

        //获取单个的其他资料
        public DataTable getSingleOtherData(int id)
        {
            string sql = "select * from tb_other where other_id ="+ id;
            DataTable dt = SqlHelper.ExecuteDataTable(sql, null);
            return dt;
        }


        //删除某个其他资料
        public bool delOtherData(int otherid)
        {
            bool IsSuccess = false;
            StringBuilder sb = new StringBuilder();
            //sb.Append("BEGIN ");
            sb.Append("delete from tb_other where other_id=" + otherid);
            //sb.Append(" END;");
            if (SqlHelper.ExecuteNonQuery(sb.ToString()) > 0)
            {
                IsSuccess = true;
            }
            return IsSuccess;
        }

        //添加其他资料项
        public bool addOtherData(Other data)
        {
            bool IsSuccess = false;
            string sql = "insert into tb_other values(@a,@b,@c,@d,@e,@f,@g)";
            SqlParameter[] spm =
          {
                new SqlParameter("@a",SqlDbType.Int),
                new SqlParameter("@b",SqlDbType.VarChar),
                new SqlParameter("@c",SqlDbType.VarChar),
                new SqlParameter("@d",SqlDbType.VarChar),
                new SqlParameter("@e",SqlDbType.VarChar),
                new SqlParameter("@f",SqlDbType.VarChar),
                new SqlParameter("@g",SqlDbType.VarChar),

            };
            spm[0].Value = data.OtherId;
            spm[1].Value = data.Name;
            spm[2].Value = data.BuildCost;
            spm[3].Value = data.RecoveryN;
            spm[4].Value = data.OperatingCost;
            spm[5].Value = data.OilCost;
            spm[6].Value = data.ElectricityCost;

            if (SqlHelper.ExecuteNonQuery(sql, spm) > 0)
            {
                IsSuccess = true;
            }
            return IsSuccess;
        }



        //获取最大数
        public int getMaxOtherId()
        {
            string sql = "select  max(other_id) from tb_other";
            int max = 1000; ;
            if (SqlHelper.ExecuteScalar(sql, null) != System.DBNull.Value)
            {
                max = (int)SqlHelper.ExecuteScalar(sql, null);
            }
            return max;
        }
    }
}
