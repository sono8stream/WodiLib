// ========================================
// Project Name : WodiLib
// File Name    : MapEventPageBootInfo.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using WodiLib.Sys;

namespace WodiLib.Map
{
    /// <summary>
    /// マップイベントページ起動情報クラス
    /// </summary>
    [Serializable]
    public class MapEventPageBootInfo : IEquatable<MapEventPageBootInfo>, ISerializable
    {
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Public Property
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        private MapEventBootType mapEventBootType = MapEventBootType.PushOKKey;

        /// <summary>イベント起動タイプ</summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        public MapEventBootType MapEventBootType
        {
            get => mapEventBootType;
            set
            {
                if (value == null) throw new PropertyNullException(ErrorMessage.NotNull(nameof(MapEventBootType)));
                mapEventBootType = value;
            }
        }

        private MapEventBootCondition mapEventBootCondition1 = new MapEventBootCondition();
        private MapEventBootCondition mapEventBootCondition2 = new MapEventBootCondition();
        private MapEventBootCondition mapEventBootCondition3 = new MapEventBootCondition();
        private MapEventBootCondition mapEventBootCondition4 = new MapEventBootCondition();

        /// <summary>起動条件1設定フラグ</summary>
        public bool HasEventBootCondition1
        {
            get => mapEventBootCondition1.UseCondition;
            set => mapEventBootCondition1.UseCondition = value;
        }

        /// <summary>起動条件2設定フラグ</summary>
        public bool HasEventBootCondition2
        {
            get => mapEventBootCondition2.UseCondition;
            set => mapEventBootCondition2.UseCondition = value;
        }

        /// <summary>起動条件3設定フラグ</summary>
        public bool HasEventBootCondition3
        {
            get => mapEventBootCondition3.UseCondition;
            set => mapEventBootCondition3.UseCondition = value;
        }

        /// <summary>起動条件4設定フラグ</summary>
        public bool HasEventBootCondition4
        {
            get => mapEventBootCondition4.UseCondition;
            set => mapEventBootCondition4.UseCondition = value;
        }

        /// <summary>
        /// 起動条件設定フラグを設定する。
        /// </summary>
        /// <param name="index">[Range(0, 3)] 条件インデックス</param>
        /// <param name="flag">条件設定フラグ</param>
        /// <exception cref="ArgumentOutOfRangeException">indexが 0～3 以外</exception>
        public void SetHasEventBootCondition(int index, bool flag)
        {
            switch (index)
            {
                case 0:
                    HasEventBootCondition1 = flag;
                    return;
                case 1:
                    HasEventBootCondition2 = flag;
                    return;
                case 2:
                    HasEventBootCondition3 = flag;
                    return;
                case 3:
                    HasEventBootCondition4 = flag;
                    return;
                default:
                    throw new ArgumentOutOfRangeException(
                        ErrorMessage.OutOfRange(nameof(index), 0, 3, index));
            }
        }

        /// <summary>[NotNull] イベント起動条件1</summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        public MapEventBootCondition MapEventBootCondition1
        {
            get => mapEventBootCondition1;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(MapEventBootCondition1)));
                mapEventBootCondition1 = value;
            }
        }

        /// <summary>[NotNull] イベント起動条件2</summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        public MapEventBootCondition MapEventBootCondition2
        {
            get => mapEventBootCondition2;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(MapEventBootCondition2)));
                mapEventBootCondition2 = value;
            }
        }

        /// <summary>[NotNull] イベント起動条件3</summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        public MapEventBootCondition MapEventBootCondition3
        {
            get => mapEventBootCondition3;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(MapEventBootCondition3)));
                mapEventBootCondition3 = value;
            }
        }

        /// <summary>[NotNull] イベント起動条件4</summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        public MapEventBootCondition MapEventBootCondition4
        {
            get => mapEventBootCondition4;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(MapEventBootCondition4)));
                mapEventBootCondition4 = value;
            }
        }

        /// <summary>
        /// 起動条件を設定する。
        /// </summary>
        /// <param name="index">[Range(0, 3)] 条件インデックス</param>
        /// <param name="condition">[NotNull] 条件</param>
        /// <exception cref="ArgumentOutOfRangeException">indexが 0～3 以外</exception>
        /// <exception cref="ArgumentNullException">conditionがnull</exception>
        public void SetEventBootCondition(int index, MapEventBootCondition condition)
        {
            if (condition == null)
            {
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(condition)));
            }

            switch (index)
            {
                case 0:
                    MapEventBootCondition1 = condition;
                    return;
                case 1:
                    MapEventBootCondition2 = condition;
                    return;
                case 2:
                    MapEventBootCondition3 = condition;
                    return;
                case 3:
                    MapEventBootCondition4 = condition;
                    return;
                default:
                    throw new ArgumentOutOfRangeException(
                        ErrorMessage.OutOfRange(nameof(index), 0, 3, index));
            }
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Constructor
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MapEventPageBootInfo()
        {
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Public Method
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// 値を比較する。
        /// </summary>
        /// <param name="other">比較対象</param>
        /// <returns>一致する場合、true</returns>
        public bool Equals(MapEventPageBootInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return mapEventBootType.Equals(other.mapEventBootType)
                   && mapEventBootCondition1.Equals(other.mapEventBootCondition1)
                   && mapEventBootCondition2.Equals(other.mapEventBootCondition2)
                   && mapEventBootCondition3.Equals(other.mapEventBootCondition3)
                   && mapEventBootCondition4.Equals(other.mapEventBootCondition4);
        }

        /// <summary>
        /// バイナリ変換する。
        /// </summary>
        /// <returns>バイナリデータ</returns>
        public byte[] ToBinary()
        {
            var result = new List<byte>();

            // 起動条件
            result.Add(MapEventBootType.Code);
            // 条件1演算子 & 使用フラグ
            result.Add(MapEventBootCondition1.MakeEventBootConditionByte());
            // 条件2演算子 & 使用フラグ
            result.Add(MapEventBootCondition2.MakeEventBootConditionByte());
            // 条件3演算子 & 使用フラグ
            result.Add(MapEventBootCondition3.MakeEventBootConditionByte());
            // 条件4演算子 & 使用フラグ
            result.Add(MapEventBootCondition4.MakeEventBootConditionByte());
            // 条件1左辺
            result.AddRange(MapEventBootCondition1.LeftSide.ToBytes(Endian.Woditor));
            // 条件2左辺
            result.AddRange(MapEventBootCondition2.LeftSide.ToBytes(Endian.Woditor));
            // 条件3左辺
            result.AddRange(MapEventBootCondition3.LeftSide.ToBytes(Endian.Woditor));
            // 条件4左辺
            result.AddRange(MapEventBootCondition4.LeftSide.ToBytes(Endian.Woditor));
            // 条件1右辺
            result.AddRange(MapEventBootCondition1.RightSide.ToBytes(Endian.Woditor));
            // 条件2右辺
            result.AddRange(MapEventBootCondition2.RightSide.ToBytes(Endian.Woditor));
            // 条件3右辺
            result.AddRange(MapEventBootCondition3.RightSide.ToBytes(Endian.Woditor));
            // 条件4右辺
            result.AddRange(MapEventBootCondition4.RightSide.ToBytes(Endian.Woditor));

            return result.ToArray();
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Serializable
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// オブジェクトをシリアル化するために必要なデータを設定する。
        /// </summary>
        /// <param name="info">デシリアライズ情報</param>
        /// <param name="context">コンテキスト</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(mapEventBootType), mapEventBootType.Code);
            info.AddValue(nameof(mapEventBootCondition1), mapEventBootCondition1);
            info.AddValue(nameof(mapEventBootCondition2), mapEventBootCondition2);
            info.AddValue(nameof(mapEventBootCondition3), mapEventBootCondition3);
            info.AddValue(nameof(mapEventBootCondition4), mapEventBootCondition4);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="info">デシリアライズ情報</param>
        /// <param name="context">コンテキスト</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected MapEventPageBootInfo(SerializationInfo info, StreamingContext context)
        {
            mapEventBootType = MapEventBootType.FromByte(info.GetByte(nameof(mapEventBootType)));
            mapEventBootCondition1 = info.GetValue<MapEventBootCondition>(nameof(mapEventBootCondition1));
            mapEventBootCondition2 = info.GetValue<MapEventBootCondition>(nameof(mapEventBootCondition2));
            mapEventBootCondition3 = info.GetValue<MapEventBootCondition>(nameof(mapEventBootCondition3));
            mapEventBootCondition4 = info.GetValue<MapEventBootCondition>(nameof(mapEventBootCondition4));
        }
    }
}