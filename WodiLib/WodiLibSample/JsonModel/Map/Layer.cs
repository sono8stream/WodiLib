using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace WodiLibSample.JsonModel.Map
{
    [DataContract]
    class Layer
    {
        [DataMember(Name = "chips")]
        public MapChip[][] Chips { get; set; }

        public Layer(WodiLib.Map.Layer layer)
        {
            Chips = new MapChip[layer.Height][];

            for(int i = 0; i < layer.Height; i++)
            {
                Chips[i] = new MapChip[layer.Width];
                for(int j = 0; j < layer.Width; j++)
                {
                    Chips[i][j] = new MapChip(layer.Chips[j][i]);
                }
            }
        }
    }
}
