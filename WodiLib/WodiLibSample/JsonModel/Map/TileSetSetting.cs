using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace WodiLibSample.JsonModel.Map
{
    [DataContract]
    class TileSetSetting
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "baseTilePath")]
        public string BaseTilePath { get; set; }

        [DataMember(Name = "autoTilePathList")]
        public List<string> AutoTilePathList { get; set; }

        [DataMember(Name = "tileSettingList")]
        public List<TileSetting> TileSettingList { get; set; }

        public TileSetSetting(WodiLib.Map.TileSetSetting rawSetting)
        {
            Name = rawSetting.Name;
            BaseTilePath = rawSetting.BaseTileSetFileName;
            AutoTilePathList = rawSetting.AutoTileFileNameList.Select(
                item => item.ToString()).ToList();
            TileSettingList = new List<TileSetting>();
            for(int i = 0; i < rawSetting.TilePathSettingList.Count; i++)
            {
                var tileSetting = new TileSetting(rawSetting.TilePathSettingList[i],
                    rawSetting.TileTagNumberList[i]);
                TileSettingList.Add(tileSetting);
            }
        }

        public string ToJsonString()
        {
            string json;
            using (var ms = new MemoryStream())
            using (var sr = new StreamReader(ms))
            {
                var serializer = new DataContractJsonSerializer(typeof(TileSetSetting));
                serializer.WriteObject(ms, this);
                ms.Position = 0;
                json = sr.ReadToEnd();
            }
            return json;
        }
    }
}
