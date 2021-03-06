// ========================================
// Project Name : WodiLib
// File Name    : ClearItemHandler.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using System;
using WodiLib.Sys;

namespace WodiLib.Database.DatabaseTypeDescHandler.ItemDescList.ItemValues
{
    /// <summary>
    /// DBItemValuesList.ClearItemのイベントハンドラ
    /// </summary>
    internal class ClearItemHandler : OnClearItemHandler<DBItemValue>
    {
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Public Constant
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// リストイベントハンドラにつけるタグ
        /// </summary>
        public static readonly string HandlerTag = ItemSetting.SetItemHandler.HandlerTag;

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Constructor
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        public ClearItemHandler(DatabaseTypeDesc outer)
            : base(MakeHandler(outer), HandlerTag, false, canChangeEnabled: false)
        {
        }

        /// <summary>
        /// DatabaseItemDescList.ClearItemのイベントを生成する。
        /// </summary>
        /// <param name="outer">連係外部クラスインスタンス</param>
        /// <returns>ClearItemイベント</returns>
        private static Action MakeHandler(DatabaseTypeDesc outer)
        {
            return () =>
            {
                outer.ItemDescList.Clear();
                outer.WritableItemSettingList.Clear();
            };
        }
    }
}