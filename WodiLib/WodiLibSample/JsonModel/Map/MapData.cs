using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace WodiLibSample.JsonModel.Map
{
    [DataContract]
    class MapData
    {
        [DataMember(Name = "tileSetID")]
        public int TileSetID { get; set; }

        [DataMember(Name = "width")]
        public int Width { get; set; }

        [DataMember(Name = "height")]
        public int Height { get; set; }

        [DataMember(Name = "layer1")]
        public Layer Layer1 { get; set; }

        [DataMember(Name = "layer2")]
        public Layer Layer2 { get; set; }

        [DataMember(Name = "layer3")]
        public Layer Layer3 { get; set; }

        [DataMember(Name = "mapEvents")]
        //[IgnoreDataMember]
        public MapEventList MapEvents { get; set; }

        public MapData(WodiLib.Map.MapData rawData)
        {
            TileSetID = rawData.TileSetId;
            Width = rawData.MapSizeWidth;
            Height = rawData.MapSizeHeight;
            Layer1 = new Layer(rawData.Layer1);
            Layer2 = new Layer(rawData.Layer2);
            Layer3 = new Layer(rawData.Layer3);
            MapEvents = new MapEventList(rawData.MapEvents);
        }

        public string ToJsonString()
        {
            string json;
            using (var ms = new MemoryStream())
            using (var sr = new StreamReader(ms))
            {
                var serializer = new DataContractJsonSerializer(typeof(MapData));
                serializer.WriteObject(ms, this);
                ms.Position = 0;
                json = sr.ReadToEnd();
            }
            return json;
        }
    }
}
