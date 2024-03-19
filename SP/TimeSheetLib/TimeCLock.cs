using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheetLib
{
    public class TimeClock
    {
        public IPerson Person { get; set; }
        public ClockInOut ClockInOut { get; set; }
        public DateTime Time { get; set; }
    }
    public enum ClockInOut
    {
        In,
        Out
    }
}
