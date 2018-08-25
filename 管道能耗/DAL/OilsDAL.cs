using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class OilsDAL
    {


        //获取油品编号最大数
        public int getMaxOilsId()
        {
            string sql = "select  max(oils_id) from tb_oils";
            int max = 1000; ;
            if (SqlHelper.ExecuteScalar(sql, null) != System.DBNull.Value)
            {
                max = (int)SqlHelper.ExecuteScalar(sql, null);
            }
            return max;
        }

        //获取所有的油品资料
        public DataTable getOilosData()
        {
            string sql = "select * from tb_oils";
            DataTable dt = SqlHelper.ExecuteDataTable(sql, null);
            return dt;
        }

        //获取单个的油品资料
        public DataTable getSingleOilosData(int id)
        {
            string sql = "select * from tb_oils where oils_id =" + id;
            DataTable dt = SqlHelper.ExecuteDataTable(sql, null);
            return dt;
        }

        public bool delOilsData(int id)
        {
            bool IsSuccess = false;
            StringBuilder sb = new StringBuilder();
            //sb.Append("BEGIN ");

            sb.Append("delete from tb_oils where oils_id=" + id);

            //sb.Append(" END;");
            if (SqlHelper.ExecuteNonQuery(sb.ToString()) > 0)
            {
                IsSuccess = true;
            }
            return IsSuccess;
        }

        public bool addOilsData(Oils data)
        {
            bool IsSuccess = false;
            string sql = "insert into tb_oils values(@a,@b,@c,@d,@e,@f,@g)";
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
            spm[0].Value = data.OilsId;
            spm[1].Value = data.Name;
            spm[2].Value = data.Density;
            spm[3].Value = data.Viscosity;
            spm[4].Value = data.MasFlow;
            spm[5].Value = data.OutputByYear;
            spm[6].Value = data.Volume_concentration;
 
            if (SqlHelper.ExecuteNonQuery(sql, spm) > 0)
            {
                IsSuccess = true;
            }
            return IsSuccess;
        }


    }


   
}
