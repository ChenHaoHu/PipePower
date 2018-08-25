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
   public class SoilDAL
    {
        //获取油编号最大数
        public int getMaxSoilId()
        {
            string sql = "select  max(soil_id) from tb_soil";
            int max = 1000; ;
            if (SqlHelper.ExecuteScalar(sql, null) != System.DBNull.Value)
            {
                max = (int)SqlHelper.ExecuteScalar(sql, null);
            }
            return max;
        }


        //获取单个土壤资料
        public DataTable getSingelSoilData(int soilId)
        {
            string sql = "select * from tb_soil where soil_id = @id ";
            SqlParameter pms = new SqlParameter("@id", soilId);
            DataTable dt = SqlHelper.ExecuteDataTable(sql, pms);
            return dt;
        }
   
        //获取所有土壤资料
        public DataTable getSoilData()
        {
            string sql = "select * from tb_soil";
            
            DataTable dt = SqlHelper.ExecuteDataTable(sql, null);
            return dt;
        }


        //删除某个泵资料
        public bool delSoilData(int soilId)
        {
            bool IsSuccess = false;
            StringBuilder sb = new StringBuilder();
            //sb.Append("BEGIN ");
            sb.Append("delete from tb_soil where soil_id=" + soilId);
            //sb.Append(" END;");
            if (SqlHelper.ExecuteNonQuery(sb.ToString()) > 0)
            {
                IsSuccess = true;
            }
            return IsSuccess;
        }

        //添加泵资料项
        public bool addSoilData(Soil data)
        {
            bool IsSuccess = false;
            string sql = "insert into tb_soil values(@a,@b,@c,@d)";
            SqlParameter[] spm =
          {
                new SqlParameter("@a",SqlDbType.Int),
                new SqlParameter("@b",SqlDbType.VarChar),
                new SqlParameter("@c",SqlDbType.VarChar),
                new SqlParameter("@d",SqlDbType.VarChar),
            };
            spm[0].Value = data.SoidId;
            spm[1].Value = data.SoilName;
            spm[2].Value = data.SoilTemp;
            spm[3].Value = data.SoilDiff;



            if (SqlHelper.ExecuteNonQuery(sql, spm) > 0)
            {
                IsSuccess = true;
            }
            return IsSuccess;
        }

    }
}
