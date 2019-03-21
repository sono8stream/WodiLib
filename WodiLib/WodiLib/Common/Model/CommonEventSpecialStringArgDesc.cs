// ========================================
// Project Name : WodiLib
// File Name    : CommonEventSpecialStringArgDesc.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using WodiLib.Sys;

namespace WodiLib.Common
{
    /// <summary>
    /// コモンイベント文字列引数特殊指定情報クラス
    /// </summary>
    public class CommonEventSpecialStringArgDesc : ICommonEventSpecialArgDesc
    {
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Public Property
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        private string argName = "";

        /// <summary>
        /// [NotNull] 引数名
        /// </summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        public string ArgName
        {
            get => argName;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(ArgName)));
                argName = value;
            }
        }

        /// <summary>
        /// 引数特殊指定タイプ
        /// </summary>
        public CommonEventArgType ArgType => CommonEventArgType.Normal;

        /// <summary>
        /// 数値引数の初期値
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int InitValue => throw new InvalidOperationException(
            "文字列引数の数値初期値は取得できません。");

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Public Method
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// 引数種別によらずすべての選択肢を取得する。
        /// </summary>
        /// <returns>すべての選択肢リスト</returns>
        public List<CommonEventSpecialArgCase> GetAllSpecialCase()
        {
            // 文字列引数は選択肢なし
            return new List<CommonEventSpecialArgCase>();
        }

        /// <summary>
        /// すべての選択肢番号を取得する。
        /// </summary>
        /// <returns>すべての選択肢リスト</returns>
        public List<int> GetAllSpecialCaseNumber()
        {
            // 文字列引数は選択肢なし
            return new List<int>();
        }

        /// <summary>
        /// すべての選択肢文字列を取得する。
        /// </summary>
        /// <returns>すべての選択肢リスト</returns>
        public List<string> GetAllSpecialCaseDescription()
        {
            // 文字列引数は選択肢なし
            return new List<string>();
        }
    }
}