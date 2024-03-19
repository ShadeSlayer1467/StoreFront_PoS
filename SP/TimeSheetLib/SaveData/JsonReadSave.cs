using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonLib.SaveData
{
    public class JsonReadSave : ReadSaveTimeSheetData
    {
        public void SaveTimeSheet(TimeSheetLib.TimeSheet timeSheet)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            };

            string json = JsonConvert.SerializeObject(timeSheet, settings);

            // get full path to current executable
            string fullPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            // get directory
            string directory = System.IO.Path.GetDirectoryName(fullPath);

            // make a folder named "TimeSheetData" in the directory if it doesn't exist then save the file there
            System.IO.Directory.CreateDirectory(directory + @"\TimeSheetData");
            System.IO.File.WriteAllText(directory + @"\TimeSheetData\WriteText.json", json);
        }
        public TimeSheetLib.TimeSheet LoadTimeSheet()
        {
            // get full path to current executable
            string fullPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            // get directory
            string directory = System.IO.Path.GetDirectoryName(fullPath);

            // make a folder named "TimeSheetData" in the directory if it doesn't exist then save the file there
            System.IO.Directory.CreateDirectory(directory + @"\TimeSheetData");

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            };

            try
            {
                string json = System.IO.File.ReadAllText(directory + @"\TimeSheetData\WriteText.json");
                return JsonConvert.DeserializeObject<TimeSheetLib.TimeSheet>(json, settings);
            }
            catch
            {
                return new TimeSheetLib.TimeSheet();
            }
        }
    }
}