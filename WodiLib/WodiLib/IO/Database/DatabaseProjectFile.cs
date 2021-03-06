// ========================================
// Project Name : WodiLib
// File Name    : DatabaseProjectFile.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using System;
using System.Threading.Tasks;
using WodiLib.Database;
using WodiLib.Sys;

namespace WodiLib.IO
{
    /// <summary>
    /// XXXDatabase.projectファイルクラス
    /// </summary>
    public class DatabaseProjectFile
    {
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Public Property
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// ファイル名
        /// </summary>
        public DatabaseProjectFilePath FileName { get; }

        /// <summary>
        /// [Nullable] 読み取り/書き出しDBプロジェクトデータ
        /// </summary>
        public DatabaseProject Data { get; private set; }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Private Static Method
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// ファイル書き出しクラスを生成する。
        /// </summary>
        /// <param name="fileName">[NotNull] 書き出しファイル名</param>
        /// <param name="data">[NotNull] 書き出しDBプロジェクトデータ</param>
        /// <returns>ライターインスタンス</returns>
        /// <exception cref="ArgumentNullException">filePath, data がnullの場合</exception>
        private static DatabaseProjectFileWriter BuildFileWriter(DatabaseProjectFilePath fileName, DatabaseProject data)
        {
            if (fileName == null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(fileName)));

            if (data == null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(data)));

            var writer = new DatabaseProjectFileWriter(data, fileName);
            return writer;
        }

        /// <summary>
        /// ファイル読み込みクラスを生成する。
        /// </summary>
        /// <param name="fileName">[NotNull] 読み込みファイル名</param>
        /// <returns>リーダーインスタンス</returns>
        /// <exception cref="ArgumentNullException">fileNameがnullの場合</exception>
        private static DatabaseProjectFileReader BuildFileReader(DatabaseProjectFilePath fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(fileName)));

            var reader = new DatabaseProjectFileReader(fileName, fileName.DBKind);
            return reader;
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Constructor
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fileName">[NotNull] ファイル名</param>
        /// <exception cref="ArgumentNullException">fileNameがnullの場合</exception>
        public DatabaseProjectFile(DatabaseProjectFilePath fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(fileName)));

            FileName = fileName;
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Public Method
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// ファイルを同期的に書き出す。
        /// </summary>
        /// <param name="data">[NotNull] 書き出しデータ</param>
        /// <exception cref="ArgumentNullException">data がnullの場合</exception>
        public void WriteSync(DatabaseProject data)
        {
            if (data == null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(data)));

            Data = data;

            var writer = BuildFileWriter(FileName, Data);
            writer.WriteSync();
        }

        /// <summary>
        /// ファイルを非同期的に書き出す。
        /// </summary>
        /// <param name="data">[NotNull] 書き出しデータ</param>
        /// <returns>非同期処理タスク</returns>
        /// <exception cref="ArgumentNullException">data がnullの場合</exception>
        public async Task WriteAsync(DatabaseProject data)
        {
            if (data == null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(data)));

            // 出力データのDB種別が指定されている場合のみ、ファイル名との整合性チェック
            if (data.DBKind != null)
            {
                if (FileName.DBKind != data.DBKind)
                    throw new ArgumentException(
                        "ファイル名のDB種別と出力データのDB種別が異なります、");
            }

            Data = data;

            var writer = BuildFileWriter(FileName, Data);
            await writer.WriteAsync();
        }

        /// <summary>
        /// ファイルを同期的に読み込む。
        /// </summary>
        /// <returns>読み込みデータ</returns>
        public DatabaseProject ReadSync()
        {
            var reader = BuildFileReader(FileName);
            Data = reader.ReadSync();
            return Data;
        }

        /// <summary>
        /// ファイルを非同期的に読み込む。
        /// </summary>
        /// <returns>読み込みデータを返すタスク</returns>
        public async Task<DatabaseProject> ReadAsync()
        {
            var reader = BuildFileReader(FileName);
            await reader.ReadAsync();
            Data = reader.Data;
            return Data;
        }
    }
}