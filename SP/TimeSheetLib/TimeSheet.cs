using PersonLib.SaveData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimeSheetLib
{
    // create a new TimeSheet class that has a dictionary of employee ids as keys and each value is a list of clocks
    public class TimeSheet
    {
        public Dictionary<string, List<TimeClock>> Clocks { get; set; } = new Dictionary<string, List<TimeClock>>();
        public TimeSheet()
        {
        }
        public bool IsClockedIn(string EmployeeID)
        {
            if (!Clocks.ContainsKey(EmployeeID))
            {
                Clocks[EmployeeID] = new List<TimeClock>();
                return false;
            }
            try
            {
                return Clocks[EmployeeID].Last().ClockInOut == ClockInOut.In;
            }
            catch(Exception e) { MessageBox.Show("Error", e.Message, MessageBoxButton.OK, MessageBoxImage.Error); }
            return false;
        }
        private void AddClock(TimeClock clock)
        {
            if (Clocks.ContainsKey(clock.Person.ID))
            {
                Clocks[clock.Person.ID].Add(clock);
            }
            else
            {
                Clocks.Add(clock.Person.ID, new List<TimeClock>() { clock });
            }
        }
        public void AddClock(IPerson person, ClockInOut clockInOut, DateTime time)
        {
            TimeClock clock = new TimeClock();
            clock.Person = person;
            clock.ClockInOut = clockInOut;
            clock.Time = time;
            AddClock(clock);
        }
        private bool RemoveClock(TimeClock clock)
        {
            if (Clocks.ContainsKey(clock.Person.ID))
            {
                try
                {
                    int index = Clocks[clock.Person.ID].FindIndex(c => c.Time == clock.Time);
                    Clocks[clock.Person.ID].RemoveAt(index);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool RemoveClock(IPerson person, ClockInOut clockInOut, DateTime time)
        {
            TimeClock clock = new TimeClock();
            clock.Person = person;
            clock.ClockInOut = clockInOut;
            clock.Time = time;
            return RemoveClock(clock);
        }
        public List<TimeClock> GetClocksByDate(DateTime date)
        {
            List<TimeClock> clocks = new List<TimeClock>();
            foreach (KeyValuePair<string, List<TimeClock>> kvp in Clocks)
            {
                clocks.AddRange(kvp.Value.Where(c => c.Time.Date == date.Date).ToList());
            }
            return clocks;
        }
        public List<TimeClock> GetClocksByPerson(IPerson person)
        {
            if (Clocks.ContainsKey(person.ID))
            {
                return Clocks[person.ID];
            }
            else
            {
                return new List<TimeClock>();
            }
        }
        public List<TimeClock> GetClocksByPersonAndDate(IPerson person, DateTime date)
        {
            if (Clocks.ContainsKey(person.ID))
            {
                return Clocks[person.ID].Where(c => c.Time.Date == date.Date).ToList();
            }
            else
            {
                return new List<TimeClock>();
            }
        }
    }
}
