// ========================================
// Project Name : WodiLib
// File Name    : MpsFilePath.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using System;
using System.IO;
using System.Text.RegularExpressions;
using WodiLib.Cmn;
using WodiLib.Sys;
using WodiLib.Sys.Cmn;

namespace WodiLib.IO
{
    /// <summary>
    /// XXX.mps ファイルパス
    /// </summary>
    [Serializable]
    public class MpsFilePath : FilePath, IEquatable<MpsFilePath>
    {
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Public Constant
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>ファイルパスフォーマット</summary>
        public static readonly Regex FilePathRegex = new Regex(@"^(.*)\.mps$", RegexOptions.IgnoreCase);

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Protected Override Constant
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>空文字許可フラグ</summary>
        protected override bool IsAllowEmptyString { get; } = false;

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Constructor
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>
        ///     ファイル名が "XXX.mps" ではない場合、警告ログを出力する。
        /// </remarks>
        /// <param name="value">[NotNull][NotNewLine] ファイルパス</param>
        /// <exception cref="ArgumentNullException">valueがnullの場合</exception>
        /// <exception cref="ArgumentNewLineException">
        ///     valueに改行が含まれる場合、
        ///     または255byteを超える場合
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     valueがファイルパスとして不適切な場合
        /// </exception>
        public MpsFilePath(string value) : base(value)
        {
            if (value is null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(value)));

            var fileName = Path.GetFileName(value);
            if (!FilePathRegex.IsMatch(fileName))
            {
                WodiLibLogger.GetInstance().Warning(
                    WarningMessage.UnsuitableFileName(value, FilePathRegex));
            }
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Public Override Method
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// string に変換する。
        /// </summary>
        /// <returns>string値</returns>
        public override string ToString() => Value;

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((MpsFilePath) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Public Method
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// 値を比較する。
        /// </summary>
        /// <param name="other">比較対象</param>
        /// <returns>一致する場合、true</returns>
        public bool Equals(MpsFilePath other)
        {
            if (other is null) return false;
            return Value.Equals(other.Value);
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Implicit
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// string -> MpsFilePath への暗黙的な型変換
        /// </summary>
        /// <param name="src">変換元</param>
        /// <returns>変換したインスタンス</returns>
        public static implicit operator MpsFilePath(string src)
        {
            if (src is null) return null;
            var result = new MpsFilePath(src);
            return result;
        }

        /// <summary>
        /// MpsFilePath -> string への暗黙的な型変換
        /// </summary>
        /// <param name="src">変換元</param>
        /// <returns>変換したインスタンス</returns>
        public static implicit operator string(MpsFilePath src)
        {
            return src?.Value;
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Operator
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// ==
        /// </summary>
        /// <param name="left">左辺</param>
        /// <param name="right">右辺</param>
        /// <returns>左辺==右辺の場合true</returns>
        public static bool operator ==(MpsFilePath left, MpsFilePath right)
        {
            if (ReferenceEquals(left, right)) return true;

            if (left is null || right is null) return false;

            return left.Equals(right);
        }

        /// <summary>
        /// !=
        /// </summary>
        /// <param name="left">左辺</param>
        /// <param name="right">右辺</param>
        /// <returns>左辺!=右辺の場合true</returns>
        public static bool operator !=(MpsFilePath left, MpsFilePath right)
        {
            return !(left == right);
        }
    }
}