using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DebugClocksListView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = @"D:\All Code\..SP2024\SP\SP\bin\Debug\TimeSheetData\WriteText.json";
        public MainWindow()
        {
            InitializeComponent();


            string jsonString = System.IO.File.ReadAllText(path);
            var clockData = JsonConvert.DeserializeObject<ClockDataRoot>(jsonString);
            ClockDataList.ItemsSource = clockData.Clocks["4293"];
        }

        public class ClockDataRoot
        {
            public Dictionary<string, List<ClockData>> Clocks { get; set; }
        }

        public class ClockData
        {
            public Person Person { get; set; }
            public int ClockInOut { get; set; }
            public string Time { get; set; }

            // Converting the properties from the nested object for the display
            public string ID => Person.ID;
            public string FirstName => Person.FirstName;
            public string LastName => Person.LastName;

            public string ClockStatus => ClockInOut == 0 ? "In" : "Out";
        }

        public class Person
        {
            [JsonProperty("$type")]
            public string Type { get; set; }
            public string ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public double HourlyRate { get; set; }
            public string Password { get; set; }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            string jsonString = System.IO.File.ReadAllText(path);
            var clockData = JsonConvert.DeserializeObject<ClockDataRoot>(jsonString);
            ClockDataList.ItemsSource = clockData.Clocks["4293"];
        }
    }
}
