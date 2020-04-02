using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace WodiLibSample.JsonModel.Map
{
    [DataContract]
    class TileSetData
    {
        [DataMember(Name = "tileSetSettings")]
        public List<TileSetSetting> TileSetSettings { get; set; }

        public TileSetData(WodiLib.Map.TileSetData rawData)
        {
            TileSetSettings = new List<TileSetSetting>();
            TileSetSettings.AddRange(rawData.TileSetSettingList.Select(item =>
            {
                return new TileSetSetting(item);
            }));
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
