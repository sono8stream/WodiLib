// ========================================
// Project Name : WodiLib
// File Name    : PictureShowSeWindowVariable.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using System;
using WodiLib.Project;

namespace WodiLib.Event.EventCommand
{
    /// <inheritdoc />
    /// <summary>
    /// イベントコマンド・ピクチャ（表示） お手軽ウィンドウ（変数指定）
    /// </summary>
    [Serializable]
    public class PictureShowSimpleWindowVariable : PictureShowBase
    {
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Private Constant
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        private const string EventCommandSentenceFormat
            = "ｳｨﾝﾄﾞｳ({0}) ｻｲｽﾞ[{1},{2}]";

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Property
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>読み込みファイル指定文字列変数</summary>
        public int LoadFireStringVar
        {
            get => _LoadFireStringVar;
            set
            {
                _LoadFireStringVar = value;
                NotifyPropertyChanged();
            }
        }

        /// <inheritdoc />
        /// <summary>読み込みファイル指定文字列変数</summary>
        protected override int _LoadFireStringVar { get; set; }

        /// <summary>分割数横</summary>
        public int DivisionWidth
        {
            get => _DivisionWidth;
            set
            {
                _DivisionWidth = value;
                NotifyPropertyChanged();
            }
        }

        /// <inheritdoc />
        /// <summary>分割数横</summary>
        protected override int _DivisionWidth { get; set; }

        /// <summary>分割数縦</summary>
        public int DivisionHeight
        {
            get => _DivisionHeight;
            set
            {
                _DivisionHeight = value;
                NotifyPropertyChanged();
            }
        }

        /// <inheritdoc />
        /// <summary>分割数縦</summary>
        protected override int _DivisionHeight { get; set; }

        /// <inheritdoc />
        /// <summary>読み込みファイル名または表示文字列</summary>
        protected override string LoadFileNameOrDrawString
        {
            get => "";
            set { }
        }

        /// <inheritdoc />
        /// <summary>表示タイプコード</summary>
        protected override byte DisplayTypeCode
        {
            get => EventCommandConstant.PictureShow.ShowTypeCode.SimpleWindowVariable;
            set { }
        }

        /// <inheritdoc />
        /// <summary>文字列使用フラグ</summary>
        protected override bool IsUseString => false;

        /// <inheritdoc />
        /// <summary>文字列変数指定フラグフラグ</summary>
        protected override bool IsLoadForVariableAddress => true;

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Protected Override Method
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <inheritdoc />
        protected override string MakeEventCommandDrawItemSentence(
            EventCommandSentenceResolver resolver, EventCommandSentenceType type,
            EventCommandSentenceResolveDesc desc)
        {
            var loadFileStr = resolver.GetVariableAddressStringIfVariableAddress(
                LoadFireStringVar, type, desc);
            var sizeWidthStr = resolver.GetNumericVariableAddressStringIfVariableAddress(DivisionWidth, type, desc);
            var sizeHeightStr = resolver.GetNumericVariableAddressStringIfVariableAddress(DivisionHeight, type, desc);
            return string.Format(EventCommandSentenceFormat,
                loadFileStr, sizeWidthStr, sizeHeightStr);
        }
    }
}