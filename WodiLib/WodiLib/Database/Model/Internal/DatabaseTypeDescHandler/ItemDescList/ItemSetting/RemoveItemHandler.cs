// ========================================
// Project Name : WodiLib
// File Name    : RemoveItemHandler.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using System;
using WodiLib.Sys;

namespace WodiLib.Database.DatabaseTypeDescHandler.ItemDescList.ItemSetting
{
    /// <summary>
    /// DBItemSettingList.RemoveItemのイベントハンドラ
    /// </summary>
    internal class RemoveItemHandler : OnRemoveItemHandler<DBItemSetting>
    {
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Public Constant
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// リストイベントハンドラにつけるタグ
        /// </summary>
        public static readonly string HandlerTag = SetItemHandler.HandlerTag;

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Constructor
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        public RemoveItemHandler(DatabaseTypeDesc outer)
            : base(MakeHandler(outer), HandlerTag, false, canChangeEnabled: false)
        {
        }

        /// <summary>
        /// DatabaseItemDescList.RemoveItemのイベントを生成する。
        /// </summary>
        /// <param name="outer">連係外部クラスインスタンス</param>
        /// <returns>RemoveItemイベント</returns>
        private static Action<int> MakeHandler(DatabaseTypeDesc outer)
        {
            return i =>
            {
                outer.ItemDescList.RemoveAt(i);
                outer.WritableItemValuesList.RemoveFieldAt(i);
            };
        }
    }
}