// ========================================
// Project Name : WodiLib
// File Name    : GameIniFileWriter.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WodiLib.Ini;
using WodiLib.Sys;
using WodiLib.Sys.Cmn;

namespace WodiLib.IO
{
    /// <summary>
    /// Game.ini書き出しクラス
    /// </summary>
    internal class GameIniFileWriter
    {
        /// <summary>書き出しファイルパス</summary>
        public GameIniFilePath FilePath { get; }

        /// <summary>書き出すデータ</summary>
        public GameIniData Data { get; }

        /// <summary>ロガー</summary>
        private WodiLibLogger Logger { get; } = WodiLibLogger.GetInstance();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="outputData">[NotNull] 書き出しデータ</param>
        /// <param name="filePath">[NotNullOrEmpty] 書き出しファイルパス</param>
        /// <exception cref="ArgumentNullException">outputData, filePathがnullの場合</exception>
        public GameIniFileWriter(GameIniData outputData, GameIniFilePath filePath)
        {
            if (outputData is null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(outputData)));
            if (filePath is null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(filePath)));

            Data = outputData;
            FilePath = filePath;
        }

        /// <summary>
        /// データを同期的に書き出す。
        /// </summary>
        /// <exception cref="ArgumentException">
        ///     ファイル名が正しくない場合、
        ///     またはpathが非ファイルデバイスを参照している場合
        /// </exception>
        public void WriteSync()
        {
            Logger.Info(FileIOMessage.StartFileWrite(GetType()));

            var innerWriter = new NonSectionIniFileWriter<GameIniRootData>(
                FilePath, new List<GameIniRootData> {Data.ToIniRootData()});
            innerWriter.WriteSync();

            Logger.Info(FileIOMessage.EndFileWrite(GetType()));
        }


        /// <summary>
        /// データを非同期的に書き出す。
        /// </summary>
        /// <returns>書き出しTask</returns>
        /// <exception cref="ArgumentException">
        ///     ファイル名が正しくない場合、
        ///     またはpathが非ファイルデバイスを参照している場合
        /// </exception>
        public async Task WriteAsync()
        {
            await Task.Run(WriteSync);
        }
    }
}