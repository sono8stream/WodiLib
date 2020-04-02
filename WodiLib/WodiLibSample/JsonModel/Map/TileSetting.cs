using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace WodiLibSample.JsonModel.Map
{
    [DataContract]
    class TileSetting
    {
        [DataMember(Name = "permission")]
        public int Permission { get; set; }

        [DataMember(Name = "impassableArea")]
        public int ImpassableArea { get; set; }

        [DataMember(Name = "impassableDirection")]
        public int ImpassableDirection { get; set; }

        [DataMember(Name = "drawOption")]
        public int DrawOption { get; set; }

        [DataMember(Name = "isCounter")]
        public bool IsCounter { get; set; }

        [DataMember(Name="tagNo")]
        public int TagNo { get; set; }

        public TileSetting(WodiLib.Map.TilePathSetting rawSetting, byte tagNo)
        {
            Permission = rawSetting.PathPermission.Code;
            if (rawSetting.PathPermission == WodiLib.Map.TilePathPermission.PartialDeny)
            {
                ImpassableArea = rawSetting.ImpassableFlags.ToCode();
            }
            else
            {
                ImpassableArea = 0;
            }
            if (rawSetting.PathPermission == WodiLib.Map.TilePathPermission.Allow ||
                rawSetting.PathPermission == WodiLib.Map.TilePathPermission.Dependent)
            {
                ImpassableDirection = rawSetting.CannotPassingFlags.ToCode();
            }
            else
            {
                ImpassableDirection = 0;
            }
            DrawOption = rawSetting.PathOption.Code;
            IsCounter = rawSetting.IsCounter;
            TagNo = tagNo;
        }
    }
}
