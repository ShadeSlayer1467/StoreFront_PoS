using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonLib.SaveData
{
    public interface ReadSaveTimeSheetData
    {
        void SaveTimeSheet(TimeSheetLib.TimeSheet timeSheet);
        TimeSheetLib.TimeSheet LoadTimeSheet();
    }
}
