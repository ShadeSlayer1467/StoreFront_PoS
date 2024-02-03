using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.UI.SaveData
{
    public class JsonDatabase : SaveInventory
    {
        public void SaveInventory(DataModel.InventoryManager inventory)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(inventory);
            System.IO.File.WriteAllText("inventory.json", json);
        }
        public DataModel.InventoryManager GetInventoryManager()
        {
            try
            {
                string json = System.IO.File.ReadAllText("inventory.json");
                DataModel.InventoryManager result = JsonConvert.DeserializeObject<DataModel.InventoryManager>(json);
                return result;
            }
            catch (Exception)
            {
                return new DataModel.InventoryManager();
            }
        }
    }
}
