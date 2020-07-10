using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine;
using ObjectData.ItemData.Models;

namespace ObjectData.ItemData.Utilities
{
    public static class ItemLookup
    {
        public static readonly string path =  Application.dataPath + "/Scripts/ObjectData/ItemData/Static";
        public static readonly string file = "/ItemData.json";

        // This will need to be replaced by database entries in the future
        public static ItemModel MasterList { get; set;}

        public static void InitializeItemData()
        {
            JObject data = JSONUtilities.LoadJSON(path + file);
            MasterList = data.ToObject<ItemModel>();
        }

        public static Item FindItem(string name)
        {
            
            return MasterList.Items.Find(i => i.Name == name);
        }
    }
}