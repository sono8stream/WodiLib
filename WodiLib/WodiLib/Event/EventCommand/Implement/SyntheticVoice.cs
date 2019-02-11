// ========================================
// Project Name : WodiLib
// File Name    : SyntheticVoice.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using System;
using System.ComponentModel;
using WodiLib.Sys;

namespace WodiLib.Event.EventCommand
{
    /// <inheritdoc />
    /// <summary>
    /// イベントコマンド・合成音声
    /// </summary>
    public class SyntheticVoice : EventCommandBase
    {
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     OverrideMethod
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <inheritdoc />
        public override int EventCommandCode => EventCommand.EventCommandCode.SyntheticVoice;

        /// <inheritdoc />
        public override byte NumberVariableCount => 0x05;

        /// <inheritdoc />
        public override byte StringVariableCount => 0x01;

        /// <inheritdoc />
        /// <summary>
        /// インデックスを指定して数値変数を取得する。
        /// </summary>
        /// <param name="index">[Range(0, 4)] インデックス</param>
        /// <returns>インデックスに対応した値</returns>
        /// <exception cref="ArgumentOutOfRangeException">indexが指定範囲以外</exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override int GetNumberVariable(int index)
        {
            switch (index)
            {
                case 0:
                    return EventCommandCode;

                case 1:
                    return PlaybackSpeed;

                case 2:
                    return Volume;

                case 3:
                    return VoiceTone;

                case 4:
                    return Delay;

                default:
                    throw new ArgumentOutOfRangeException(
                        ErrorMessage.OutOfRange(nameof(index), 0, 4, index));
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// 数値変数を設定する。
        /// </summary>
        /// <param name="index">[Range(1, 4)] インデックス</param>
        /// <param name="value">設定値</param>
        /// <exception cref="ArgumentOutOfRangeException">indexが指定範囲以外</exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override void SetNumberVariable(int index, int value)
        {
            switch (index)
            {
                case 1:
                    PlaybackSpeed = value;
                    return;

                case 2:
                    Volume = value;
                    return;

                case 3:
                    VoiceTone = value;
                    return;

                case 4:
                    Delay = value;
                    return;

                default:
                    throw new ArgumentOutOfRangeException(
                        ErrorMessage.OutOfRange(nameof(index), 1, 4, index));
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// インデックスを指定して文字列変数を取得する。
        /// </summary>
        /// <param name="index">[Range(0, 0)] インデックス</param>
        /// <returns>インデックスに対応した値</returns>
        /// <exception cref="ArgumentOutOfRangeException">indexが指定範囲以外</exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override string GetStringVariable(int index)
        {
            if (index == 0) return PlaybackText;
            throw new ArgumentOutOfRangeException(
                ErrorMessage.OutOfRange(nameof(index), 0, 0, index));
        }

        /// <inheritdoc />
        /// <summary>
        /// 文字列変数を設定する。
        /// </summary>
        /// <param name="index">[Range(0, 0)] インデックス</param>
        /// <param name="value">[NotNull] 設定値</param>
        /// <exception cref="ArgumentOutOfRangeException">indexが指定範囲以外</exception>
        /// <exception cref="ArgumentNullException">valueがnull</exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override void SetStringVariable(int index, string value)
        {
            if (value == null) throw new ArgumentNullException(ErrorMessage.NotNull(nameof(value)));
            if (index == 0)
            {
                PlaybackText = value;
                return;
            }

            throw new ArgumentOutOfRangeException(
                ErrorMessage.OutOfRange(nameof(index), 0, 0, index));
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Property
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>読む速さ</summary>
        public int PlaybackSpeed { get; set; }

        /// <summary>音量</summary>
        public int Volume { get; set; }

        /// <summary>声の高さ</summary>
        public int VoiceTone { get; set; }

        /// <summary>再生遅延</summary>
        public int Delay { get; set; }

        private string playbackText = "";

        /// <summary>[NotNull] 再生文章</summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        public string PlaybackText
        {
            get => playbackText;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(PlaybackText)));
                playbackText = value;
            }
        }
    }
}