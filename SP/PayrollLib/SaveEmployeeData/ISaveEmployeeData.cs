using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollLib.SaveEmployeeData
{
    public interface ISaveEmployeeData
    {
        void SaveEmployeeData(List<IEmployee> employee);
        List<IEmployee> LoadEmployeeData();
    }
}
