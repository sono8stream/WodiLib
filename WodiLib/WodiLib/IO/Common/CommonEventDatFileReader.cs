// ========================================
// Project Name : WodiLib
// File Name    : CommonEventDatFileReader.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using System;
using System.Threading.Tasks;
using WodiLib.Common;
using WodiLib.Sys;
using WodiLib.Sys.Cmn;

namespace WodiLib.IO
{
    /// <summary>
    /// コモンイベントデータファイル読み込みクラス
    /// </summary>
    internal class CommonEventDatFileReader
    {
        /// <summary>読み込みファイルパス</summary>
        // テストを考慮してstring型で管理する
        public string FilePath { get; }

        /// <summary>[Nullable] 読み込んだコモンイベントデータ</summary>
        public CommonEventData CommonEventData { get; private set; }

        private FileReadStatus ReadStatus { get; set; }

        /// <summary>ロガー</summary>
        private static WodiLibLogger Logger { get; } = WodiLibLogger.GetInstance();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="filePath">[NotNull] 読み込みファイルパス</param>
        /// <exception cref="ArgumentNullException">filePathがnullの場合</exception>
        public CommonEventDatFileReader(string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(filePath)));

            FilePath = filePath;
        }

        /// <summary>
        /// ファイルを同期的に読み込む
        /// </summary>
        /// <returns>読み込んだデータ</returns>
        /// <exception cref="InvalidOperationException">
        ///     すでにファイルを読み込んでいる場合、
        ///     またはファイルが正しく読み込めなかった場合
        /// </exception>
        public CommonEventData ReadSync()
        {
            if (CommonEventData != null)
                throw new InvalidOperationException(
                    $"すでに読み込み完了しています。");

            Logger.Info(FileIOMessage.StartFileRead(GetType()));

            ReadStatus = new FileReadStatus(FilePath);
            CommonEventData = new CommonEventData();

            // ヘッダチェック
            ReadHeader(ReadStatus);

            // コモンイベント
            ReadCommonEvent(ReadStatus, CommonEventData);

            // フッタチェック
            ReadFooter(ReadStatus);

            Logger.Info(FileIOMessage.EndFileRead(GetType()));

            return CommonEventData;
        }

        /// <summary>
        /// ファイルを非同期的に読み込む
        /// </summary>
        /// <returns>読み込み成否</returns>
        /// <exception cref="InvalidOperationException">
        ///     すでにファイルを読み込んでいる場合、
        ///     またはファイルが正しく読み込めなかった場合
        /// </exception>
        public async Task ReadAsync()
        {
            await Task.Run(ReadSync);
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     ReadMethod
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// ヘッダ
        /// </summary>
        /// <param name="status">読み込み経過状態</param>
        /// <exception cref="InvalidOperationException">ファイルヘッダが仕様と異なる場合</exception>
        private static void ReadHeader(FileReadStatus status)
        {
            foreach (var b in CommonEventData.Header)
            {
                if (status.ReadByte() != b)
                {
                    throw new InvalidOperationException(
                        $"ファイルヘッダがファイル仕様と異なります（offset:{status.Offset}）");
                }

                status.IncreaseByteOffset();
            }

            Logger.Debug($"{nameof(CommonEventDatFileReader)} ヘッダチェック完了");
        }

        /// <summary>
        /// コモンイベントリスト
        /// </summary>
        /// <param name="status">読み込み経過状態</param>
        /// <param name="data">結果格納インスタンス</param>
        /// <exception cref="InvalidOperationException">ファイルが仕様と異なる場合</exception>
        private static void ReadCommonEvent(FileReadStatus status, CommonEventData data)
        {
            // コモンイベント数
            var length = ReadCommonEventLength(status);

            // コモンイベントリスト
            ReadCommonEventList(status, length, data);
        }


        /// <summary>
        /// コモンイベント数
        /// </summary>
        /// <param name="status">読み込み経過状態</param>
        /// <returns>コモンイベント数</returns>
        private static int ReadCommonEventLength(FileReadStatus status)
        {
            var length = status.ReadInt();
            status.IncreaseIntOffset();

            Logger.Debug($"{nameof(CommonEventDatFileReader)} コモンイベント数読み込み完了 コモンイベント数：{length}");

            return length;
        }

        /// <summary>
        /// コモンイベントリスト
        /// </summary>
        /// <param name="status">読み込み経過状態</param>
        /// <param name="length">コモンイベント数</param>
        /// <param name="data">結果格納インスタンス</param>
        /// <exception cref="InvalidOperationException">ファイルが仕様と異なる場合</exception>
        private static void ReadCommonEventList(FileReadStatus status, int length, CommonEventData data)
        {
            var reader = new CommonEventReader(status, length);

            var commonEventList = reader.Read();
            data.CommonEventList = new CommonEventList(commonEventList);
        }

        /// <summary>
        /// フッタ
        /// </summary>
        /// <param name="status">読み込み経過状態</param>
        /// <exception cref="InvalidOperationException">ファイルヘッダが仕様と異なる場合</exception>
        private static void ReadFooter(FileReadStatus status)
        {
            foreach (var b in CommonEventData.Footer)
            {
                if (status.ReadByte() != b)
                {
                    throw new InvalidOperationException(
                        $"ファイルフッタがファイル仕様と異なります（offset:{status.Offset}）");
                }

                status.IncreaseByteOffset();
            }

            Logger.Debug($"{nameof(CommonEventDatFileReader)} フッタチェック完了");
        }
    }
}