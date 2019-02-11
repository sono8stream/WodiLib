// ========================================
// Project Name : WodiLib
// File Name    : PartyGraphicReplace.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using WodiLib.Sys;

namespace WodiLib.Event.EventCommand
{
    /// <inheritdoc />
    /// <summary>
    /// イベントコマンド・パーティ画像（入れ替え）
    /// </summary>
    public class PartyGraphicReplace : PartyGraphicBase
    {
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     OverrideMethod
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <inheritdoc />
        public override byte NumberVariableCount => (byte) (IsTargetingValue ? 0x04 : 0x03);

        /// <inheritdoc />
        public override byte StringVariableCount => (byte) (IsTargetingValue ? 0x00 : 0x01);

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Property
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>X人目</summary>
        public int MemberId { get; set; }

        /// <summary>処理対象数値変数指定フラグ（対象文字列指定のときfalse）</summary>
        public override bool IsTargetingValue { get; set; }

        private readonly IntOrStr loadGraphic = (0, "");

        /// <summary>[NotNull] 読み込み画像ファイル名または変数</summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        public IntOrStr LoadGraphic
        {
            get => loadGraphic;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(LoadGraphic)));
                loadGraphic.Merge(value);
            }
        }

        /// <summary>処理内容</summary>
        protected override byte ExecCode
        {
            get => EventCommandConstant.PartyGraphic.ExecCode.Replace;
            set { }
        }

        /// <summary>操作対象序列</summary>
        protected override int TargetIndex
        {
            get => MemberId;
            set => MemberId = value;
        }

        /// <summary>対象指定変数または対象ファイル名</summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        protected override IntOrStr Target
        {
            get => LoadGraphic;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(Target)));
                LoadGraphic = value;
            }
        }
    }
}