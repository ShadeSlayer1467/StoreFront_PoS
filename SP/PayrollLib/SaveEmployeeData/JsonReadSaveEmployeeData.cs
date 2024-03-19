using Newtonsoft.Json;
using PayrollLib.SaveEmployeeData;
using PayrollLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheetLib;

public class JsonReadSaveEmployeeData : ISaveEmployeeData
{
    public List<IEmployee> LoadEmployeeData()
    {
        // get full path to current executable
        string fullPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

        // get directory
        string directory = System.IO.Path.GetDirectoryName(fullPath);

        try
        {
            string json = System.IO.File.ReadAllText(directory + @"\EmployeeData\WriteText.json");

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            };

            return JsonConvert.DeserializeObject<List<IEmployee>>(json, settings);
        }
        catch { return new List<IEmployee>(); }
    }

    public void SaveEmployeeData(List<IEmployee> employeeList)
    {
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            NullValueHandling = NullValueHandling.Ignore,
        };

        string json = JsonConvert.SerializeObject(employeeList, settings);

        // get full path to current executable
        string fullPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

        // get directory
        string directory = System.IO.Path.GetDirectoryName(fullPath);

        // make a folder named "TimeSheetData" in the directory if it doesn't exist then save the file there
        System.IO.Directory.CreateDirectory(directory + @"\EmployeeData");
        System.IO.File.WriteAllText(directory + @"\EmployeeData\WriteText.json", json);
    }
}

