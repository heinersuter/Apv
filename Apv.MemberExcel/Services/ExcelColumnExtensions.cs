using System;
using System.Collections.Generic;
using System.Linq;

namespace Apv.MemberExcel.Services
{
    public static class ExcelColumnExtensions
    {
        public static int Index(this ExcelColumn column)
        {
            return (int)column;
        }

        public static IEnumerable<ExcelColumn> All()
        {
            return Enum.GetValues(typeof(ExcelColumn)).Cast<ExcelColumn>();
        }
    }
}
