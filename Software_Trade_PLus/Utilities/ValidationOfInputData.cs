using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_Trade_PLus.Utilities
{
    public static class ValidationOfInputData
    {
        public static bool StringMatchCheck(string str)
        {
            //Здесь можно добавить дополнительные ограничения
            return String.IsNullOrWhiteSpace(str);
        }
    }
}
