// ========================================
// Project Name : WodiLib
// File Name    : PartyGraphicInsert.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using System;
using System.ComponentModel;
using WodiLib.Project;
using WodiLib.Sys;

namespace WodiLib.Event.EventCommand
{
    /// <inheritdoc />
    /// <summary>
    /// イベントコマンド・パーティ画像（挿入）
    /// </summary>
    [Serializable]
    public class PartyGraphicInsert : PartyGraphicBase
    {
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Private Constant
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        private const string EventCommandSentenceFormat
            = "{0}人目の前に[{1}]を挿入";

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     OverrideMethod
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <inheritdoc />
        public override byte NumberVariableCount => (byte) (IsTargetingValue ? 0x04 : 0x03);

        /// <inheritdoc />
        public override byte StringVariableCount => (byte) (IsTargetingValue ? 0x00 : 0x01);

        /// <inheritdoc />
        /// <summary>数値変数最小個数</summary>
        public override byte NumberVariableCountMin => 0x03;

        /// <inheritdoc />
        /// <summary>文字列変数最小個数</summary>
        public override byte StringVariableCountMin => 0x00;

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Property
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        private int memberId;

        /// <summary>X人目</summary>
        public int MemberId
        {
            get => memberId;
            set
            {
                memberId = value;
                NotifyPropertyChanged();
            }
        }

        private bool isTargetingValue;

        /// <summary>処理対象数値変数指定フラグ（対象文字列指定のときfalse）</summary>
        public override bool IsTargetingValue
        {
            get => isTargetingValue;
            set
            {
                isTargetingValue = value;
                NotifyPropertyChanged();
            }
        }

        private readonly IntOrStr loadGraphic = (0, "");

        /// <summary>[NotNull] 読み込み画像ファイル名または変数</summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        public IntOrStr LoadGraphic
        {
            get => loadGraphic;
            set
            {
                if (value is null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(LoadGraphic)));
                loadGraphic.Merge(value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>処理内容</summary>
        protected override byte ExecCode
        {
            get => EventCommandConstant.PartyGraphic.ExecCode.Insert;
            set { }
        }

        /// <summary>操作対象序列</summary>
        protected override int TargetIndex
        {
            get => MemberId;
            set => MemberId = value;
        }

        /// <summary>[NotNull] 対象指定変数または対象ファイル名</summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        protected override IntOrStr Target
        {
            get => LoadGraphic;
            set
            {
                if (value is null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(Target)));
                LoadGraphic = value;
            }
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Protected Override Method
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// イベントコマンド文字列の処理内容部分を生成する。
        /// </summary>
        /// <param name="resolver">[NotNull] 名前解決クラスインスタンス</param>
        /// <param name="type">[NotNull] イベント種別</param>
        /// <param name="desc">[Nullable] 付加情報</param>
        /// <returns>イベントコマンド文字列の処理内容部分</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override string MakeEventCommandExecSentence(
            EventCommandSentenceResolver resolver, EventCommandSentenceType type,
            EventCommandSentenceResolveDesc desc)
        {
            var memberStr = resolver.GetNumericVariableAddressStringIfVariableAddress(MemberId, type, desc);
            var insertStr = IsTargetingValue
                ? resolver.GetVariableAddressStringIfVariableAddress(LoadGraphic.ToInt(), type, desc)
                : LoadGraphic.ToStr();

            return string.Format(EventCommandSentenceFormat,
                memberStr, insertStr);
        }
    }
}