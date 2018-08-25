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
  public   class ProDAL
    {
        //增删改查

        public DataTable getSingleProData(int id)
        {
            string sql = "select * from tb_project where project_id=" + id;

            DataTable dt = SqlHelper.ExecuteDataTable(sql, null);
            return dt;
        }


        public DataTable getProData()
        {
            string sql = "select * from tb_project";
            DataTable dt = SqlHelper.ExecuteDataTable(sql, null);
            return dt;
        }

        public bool delProData(int id)
        {
            bool IsSuccess = false;
            StringBuilder sb = new StringBuilder();
            //sb.Append("BEGIN ");

            sb.Append("delete from tb_project where project_id=" + id);

            //sb.Append(" END;");
            if (SqlHelper.ExecuteNonQuery(sb.ToString()) > 0)
            {
                IsSuccess = true;
            }
            return IsSuccess;
        }

        public bool addPoData(Project data)
        {
            bool IsSuccess = false;
            string sql = "insert into tb_project values(@a,@b,@c,@d,@e)";
            SqlParameter[] spm =
          {
                new SqlParameter("@a",SqlDbType.Int),
                new SqlParameter("@b",SqlDbType.VarChar),
                new SqlParameter("@c",SqlDbType.VarChar),
                new SqlParameter("@d",SqlDbType.VarChar),
                new SqlParameter("@e",SqlDbType.VarChar),
            };
            spm[0].Value = data.Proid;
            spm[1].Value = data.Name;
            spm[2].Value = data.Number;
            spm[3].Value = data.Responsible;
            spm[4].Value = data.BuildDate;

            if (SqlHelper.ExecuteNonQuery(sql, spm) > 0)
            {
                IsSuccess = true;
            }
            return IsSuccess;
        }

        public bool changeNowPro(int proid)
        {
            bool IsSuccess = false;
            string sql = "update tb_data set pro_id = " + proid + "where id = '1'";


            if (SqlHelper.ExecuteNonQuery(sql, null) > 0)
            {
                IsSuccess = true;
            }
            return IsSuccess;
        }

        public int getNowPro()
        {
            string sql = "select pro_id from tb_data where id = '1'";
            int max = 0;
            if (SqlHelper.ExecuteScalar(sql, null) != System.DBNull.Value)
            {
                max = (int)SqlHelper.ExecuteScalar(sql, null);
            }
            return max;
        }




        public int getBigProId()
        {
            string sql = "select  max(project_id) from tb_project";
            int max = 1000; ;
            if ( SqlHelper.ExecuteScalar(sql, null)!= System.DBNull.Value)
            {
                max = (int)SqlHelper.ExecuteScalar(sql, null);
            }
        
            return  max;


        }

    }

    
}
