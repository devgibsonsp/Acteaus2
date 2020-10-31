using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine;
using ObjectData.ItemData.Models;

namespace ObjectData.ItemData.Utilities
{
    public static class ItemLookup
    {
        public static readonly string path =  Application.dataPath + "/Scripts/ObjectData/ItemData/Static";
        //public static readonly string file = "/ItemData.json";

        public static readonly string AmuletData = "/AmuletData.json";
        public static readonly string ArmorData = "/ArmorData.json";
        public static readonly string AxeData = "/AxeData.json";
        public static readonly string BluntData = "/BluntData.json";
        public static readonly string BootData = "/BootData.json";
        public static readonly string GloveData = "/GloveData.json";
        public static readonly string HelmData = "/HelmData.json";
        public static readonly string KeyData ="/KeyData.json";
        public static readonly string OtherData = "/OtherData.json";
        public static readonly string PotionData = "/PotionData.json";
        public static readonly string RingData = "/RingData.json";
        public static readonly string ScrollData = "/ScrollData.json";
        public static readonly string ShieldData = "/ShieldData.json";
        public static readonly string SwordData = "/SwordData.json";

        // This will need to be replaced by database entries in the future
        public static ItemModel MasterList { get; set;}

        public static bool IsInitialized { get; set; } = false;

        public static void InitializeItemData()
        {
            //JObject data = JSONUtilities.LoadJSON(path + file);

            // Load Item Data
            MasterList = LoadJSON(path + AmuletData).ToObject<ItemModel>();
            MasterList.Items.AddRange(LoadJSON(path + ArmorData).ToObject<ItemModel>().Items);
            MasterList.Items.AddRange(LoadJSON(path + AxeData).ToObject<ItemModel>().Items);
            MasterList.Items.AddRange(LoadJSON(path + BluntData).ToObject<ItemModel>().Items);
            MasterList.Items.AddRange(LoadJSON(path + BootData).ToObject<ItemModel>().Items);
            MasterList.Items.AddRange(LoadJSON(path + GloveData).ToObject<ItemModel>().Items);
            MasterList.Items.AddRange(LoadJSON(path + HelmData).ToObject<ItemModel>().Items);
            MasterList.Items.AddRange(LoadJSON(path + KeyData).ToObject<ItemModel>().Items);
            MasterList.Items.AddRange(LoadJSON(path + OtherData).ToObject<ItemModel>().Items);
            MasterList.Items.AddRange(LoadJSON(path + PotionData).ToObject<ItemModel>().Items);
            MasterList.Items.AddRange(LoadJSON(path + RingData).ToObject<ItemModel>().Items);
            MasterList.Items.AddRange(LoadJSON(path + ScrollData).ToObject<ItemModel>().Items);
            MasterList.Items.AddRange(LoadJSON(path + ShieldData).ToObject<ItemModel>().Items);
            MasterList.Items.AddRange(LoadJSON(path + SwordData).ToObject<ItemModel>().Items);

            IsInitialized = true;
        }

        private static JObject LoadJSON(string path)
        {
            return JSONUtilities.LoadJSON(path);
        }

        public static Item FindItem(string name)
        {
            
            return MasterList.Items.Find(i => i.Name == name);
        }
    }
}