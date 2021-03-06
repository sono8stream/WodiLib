// ========================================
// Project Name : WodiLib
// File Name    : MapTreeDataFile.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using System;
using System.Threading.Tasks;
using WodiLib.Map;
using WodiLib.Sys;

namespace WodiLib.IO
{
    /// <summary>
    /// マップツリーデータファイルクラス
    /// </summary>
    public class MapTreeDataFile
    {
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Public Property
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// ファイル名
        /// </summary>
        public MapTreeDataFilePath FilePath { get; }

        /// <summary>
        /// [Nullable] 読み取り/書き出しマップデータ
        /// </summary>
        public MapTreeData MapTreeData { get; private set; }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Private Static Method
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// ファイル書き出しクラスを生成する。
        /// </summary>
        /// <param name="filePath">[NotNullOrEmpty] 書き出しファイル名</param>
        /// <param name="mapTreeData">[NotNull] 書き出しマップデータ</param>
        /// <returns>ライターインスタンス</returns>
        /// <exception cref="ArgumentNullException">filePath, mapTreeData がnullの場合</exception>
        /// <exception cref="ArgumentException">filePathが空文字の場合</exception>
        private static MapTreeDataFileWriter BuildMapTreeDataFileWriter(string filePath, MapTreeData mapTreeData)
        {
            if (mapTreeData == null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(mapTreeData)));
            if (filePath == null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(filePath)));
            if (filePath.IsEmpty())
                throw new ArgumentException(
                    ErrorMessage.NotEmpty(nameof(filePath)));

            var writer = new MapTreeDataFileWriter(mapTreeData, filePath);
            return writer;
        }

        /// <summary>
        /// ファイル読み込みクラスを生成する。
        /// </summary>
        /// <param name="filePath">[NotNullOrEmpty] 読み込みファイル名</param>
        /// <returns>リーダーインスタンス</returns>
        /// <exception cref="ArgumentNullException">filePathがnullの場合</exception>
        /// <exception cref="ArgumentException">filePathが空文字の場合</exception>
        private static MapTreeDataFileReader BuildMapTreeDataFileReader(string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(filePath)));
            if (filePath.IsEmpty())
                throw new ArgumentException(
                    ErrorMessage.NotEmpty(nameof(filePath)));

            var reader = new MapTreeDataFileReader(filePath);
            return reader;
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Constructor
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="filePath">[NotNull] ファイル名</param>
        /// <exception cref="ArgumentNullException">filePathがnullの場合</exception>
        public MapTreeDataFile(MapTreeDataFilePath filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(filePath)));

            FilePath = filePath;
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Public Method
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// ファイルを同期的に書き出す。
        /// </summary>
        /// <param name="mapTreeData">[NotNull] 書き出しデータ</param>
        /// <exception cref="ArgumentNullException">mapTreeData がnullの場合</exception>
        public void WriteSync(MapTreeData mapTreeData)
        {
            if (mapTreeData == null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(mapTreeData)));

            MapTreeData = mapTreeData;

            var writer = BuildMapTreeDataFileWriter(FilePath, MapTreeData);
            writer.WriteSync();
        }

        /// <summary>
        /// ファイルを非同期的に書き出す。
        /// </summary>
        /// <param name="mapTreeData">[NotNull] 書き出しデータ</param>
        /// <returns>非同期処理タスク</returns>
        /// <exception cref="ArgumentNullException">mapTreeData がnullの場合</exception>
        public async Task WriteAsync(MapTreeData mapTreeData)
        {
            if (mapTreeData == null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(mapTreeData)));

            MapTreeData = mapTreeData;

            var writer = BuildMapTreeDataFileWriter(FilePath, MapTreeData);
            await writer.WriteAsync();
        }

        /// <summary>
        /// ファイルを同期的に読み込む。
        /// </summary>
        /// <returns>読み込みデータ</returns>
        public MapTreeData ReadSync()
        {
            var reader = BuildMapTreeDataFileReader(FilePath);
            MapTreeData = reader.ReadSync();
            return MapTreeData;
        }

        /// <summary>
        /// ファイルを非同期的に読み込む。
        /// </summary>
        /// <returns>読み込みデータを返すタスク</returns>
        public async Task<MapTreeData> ReadAsync()
        {
            var reader = BuildMapTreeDataFileReader(FilePath);
            await reader.ReadAsync();
            MapTreeData = reader.Data;
            return MapTreeData;
        }
    }
}