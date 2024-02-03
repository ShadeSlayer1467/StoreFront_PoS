using PoS.UI.DataModel;

namespace PoS.UI.SaveData
{
    public interface SaveInventory
    {
        void SaveInventory(InventoryManager inventory);
        InventoryManager GetInventoryManager();
    }
}
