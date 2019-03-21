// ========================================
// Project Name : WodiLib
// File Name    : StringExtension.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

namespace WodiLib.Sys
{
    /// <summary>
    /// string拡張クラス
    /// </summary>
    internal static class StringExtension
    {
        /// <summary>
        /// 空文字かどうかを返す。
        /// </summary>
        /// <param name="src">対象</param>
        /// <returns>空文字の場合、true</returns>
        public static bool IsEmpty(this string src)
        {
            return src.Equals(string.Empty);
        }

        /// <summary>
        /// 改行を含むかどうかを返す。
        /// </summary>
        /// <param name="src">対象</param>
        /// <returns>改行を含む場合、true</returns>
        public static bool HasNewLine(this string src)
        {
            return src.Contains("\n") || src.Contains("\r\n");
        }
    }
}