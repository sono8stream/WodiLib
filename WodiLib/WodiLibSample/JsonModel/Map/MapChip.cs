using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace WodiLibSample.JsonModel.Map
{
    [DataContract]
    class MapChip
    {
        [DataMember(Name = "isAutoTile")]
        public bool IsAutoTile { get; set; }

        [DataMember(Name = "value")]
        public int Value { get; set; }

        [DataMember(Name = "leftUpAutoTileCode")]
        public int  LeftUpAutoTileCode { get; set; }

        [DataMember(Name = "rightUpAutoTileCode")]
        public int RightUpAutoTileCode { get; set; }

        [DataMember(Name = "leftDownAutoTileCode")]
        public int LeftDownAutoTileCode { get; set; }

        [DataMember(Name = "rightDownAutoTileCode")]
        public int RightDownAutoTileCode { get; set; }

        public MapChip(WodiLib.Map.MapChip mapChip)
        {
            IsAutoTile = mapChip.IsAutoTile;
            Value = mapChip.ToInt();
            if (IsAutoTile)
            {
                LeftUpAutoTileCode = mapChip.LeftUpAutoTile.Code;
                RightUpAutoTileCode = mapChip.RightUpAutoTile.Code;
                LeftDownAutoTileCode = mapChip.LeftDownAutoTile.Code;
                RightDownAutoTileCode = mapChip.RightDownAutoTile.Code;
            }
        }
    }
}
