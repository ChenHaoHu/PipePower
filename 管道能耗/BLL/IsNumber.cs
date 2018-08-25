using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public  class IsNumber
    {
        public  bool isNumber(string input)
        {
            Double d = 0;
            if (double.TryParse(input, out d)) // TryParse返回true说明是数值
                return true;
            else // 不是数值
                return false;
        }
    }
}
