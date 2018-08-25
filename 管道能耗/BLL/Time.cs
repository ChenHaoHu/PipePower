using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Time
    {
    public  string  getNowTime()
        {
          string time =   DateTime.Now.ToString();            // 2008-9-4 20:02:10

            return time;
        }
    }
}
