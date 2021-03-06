using System;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using WodiLib.Map;
using WodiLib.Sys.Cmn;
using WodiLib.Test.IO;
using WodiLib.Test.Tools;

namespace WodiLib.Test.Map
{
    [TestFixture]
    public class MapDataTest
    {
        private static WodiLibLogger logger;

        [SetUp]
        public static void Setup()
        {
            LoggerInitializer.SetupWodiLibLoggerForDebug();
            logger = WodiLibLogger.GetInstance();
        }

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            // テスト用ファイル出力
            MapFileTestItemGenerator.OutputMapFile();
        }

        [Test]
        public static void SetMemoTest()
        {
            var instance = new MapData();
            var value = (MapDataMemo) "test";
            var errorOccured = false;
            try
            {
                instance.Memo = value;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);
        }

        [TestCase(-1, true)]
        [TestCase(0, false)]
        [TestCase(2, false)]
        [TestCase(3, true)]
        public static void GetLayerTest(int index, bool isError)
        {
            var layer1 = new Layer();
            var layer2 = new Layer();
            var layer3 = new Layer();

            var instance = new MapData
            {
                Layer1 = layer1,
                Layer2 = layer2,
                Layer3 = layer3,
            };

            var errorOccured = false;
            Layer getResult = null;
            try
            {
                getResult = instance.GetLayer(index);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);

            if (!errorOccured)
            {
                Layer setObject;
                switch (index)
                {
                    case 0:
                        setObject = layer1;
                        break;
                    case 1:
                        setObject = layer2;
                        break;
                    default:
                        setObject = layer3;
                        break;
                }

                // 取得したインスタンスが初期化内容と一致すること
                Assert.AreSame(getResult, setObject);
            }
        }

        [TestCase(-1, false, true)]
        [TestCase(0, false, false)]
        [TestCase(0, true, true)]
        [TestCase(2, false, false)]
        [TestCase(3, false, true)]
        public static void SetLayerTest(int index, bool isNull, bool isError)
        {
            var instance = new MapData();
            Layer layer = null;
            if (!isNull) layer = new Layer();
            var errorOccured = false;
            try
            {
                instance.SetLayer(index, layer);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);

            if (!errorOccured)
            {
                // 内容が正しくセットされていること
                Layer setObject;
                switch (index)
                {
                    case 0:
                        setObject = instance.Layer1;
                        break;
                    case 1:
                        setObject = instance.Layer2;
                        break;
                    default:
                        setObject = instance.Layer3;
                        break;
                }

                Assert.AreSame(setObject, layer);
            }
        }

        [TestCase(0, false, false)]
        [TestCase(0, true, true)]
        [TestCase(1, false, false)]
        [TestCase(1, true, true)]
        [TestCase(2, false, false)]
        [TestCase(2, true, true)]
        public static void SetLayerTest2(int index, bool isNull, bool isError)
        {
            var instance = new MapData();
            var errorOccured = false;
            try
            {
                switch (index)
                {
                    case 0:
                        instance.Layer1 = isNull ? null : new Layer();
                        break;
                    case 1:
                        instance.Layer2 = isNull ? null : new Layer();
                        break;
                    default:
                        instance.Layer3 = isNull ? null : new Layer();
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);
        }

        [Test]
        public static void SetLayer1Test()
        {
            var instance = new MapData();

            var width = (MapSizeWidth) 30;
            var height = (MapSizeHeight) 24;

            var setLayer = new Layer();
            setLayer.UpdateSize(width, height);

            var errorOccured = false;
            try
            {
                instance.Layer1 = setLayer;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);

            // マップサイズが変化していること
            Assert.AreEqual(instance.MapSizeWidth, width);
            Assert.AreEqual(instance.MapSizeHeight, height);

            // レイヤーのサイズも変化していること
            Assert.AreEqual(instance.Layer1.Width, width);
            Assert.AreEqual(instance.Layer1.Height, height);
            Assert.AreEqual(instance.Layer2.Width, width);
            Assert.AreEqual(instance.Layer2.Height, height);
            Assert.AreEqual(instance.Layer3.Width, width);
            Assert.AreEqual(instance.Layer3.Height, height);
        }

        [TestCase(true, true, false)]
        [TestCase(false, true, true)]
        [TestCase(true, false, true)]
        [TestCase(false, false, true)]
        public static void SetLayer2Test(bool isEqualWidth, bool isEqualHeight, bool isError)
        {
            var instance = new MapData();

            var width = (MapSizeWidth) 30;
            var height = (MapSizeHeight) 24;

            instance.UpdateMapSize(width, height);

            var layerWidth = isEqualWidth ? width : (MapSizeWidth) (width + 1);
            var layerHeight = isEqualHeight ? height : (MapSizeHeight) (height + 1);

            var setLayer = new Layer();
            setLayer.UpdateSize(layerWidth, layerHeight);

            var errorOccured = false;
            try
            {
                instance.Layer2 = setLayer;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);

            if (errorOccured) return;

            // マップサイズが変化していないこと
            Assert.AreEqual(instance.MapSizeWidth, width);
            Assert.AreEqual(instance.MapSizeHeight, height);

            // レイヤーのサイズも変化していないこと
            Assert.AreEqual(instance.Layer1.Width, width);
            Assert.AreEqual(instance.Layer1.Height, height);
            Assert.AreEqual(instance.Layer2.Width, width);
            Assert.AreEqual(instance.Layer2.Height, height);
            Assert.AreEqual(instance.Layer3.Width, width);
            Assert.AreEqual(instance.Layer3.Height, height);
        }

        [TestCase(true, true, false)]
        [TestCase(false, true, true)]
        [TestCase(true, false, true)]
        [TestCase(false, false, true)]
        public static void SetLayer3Test(bool isEqualWidth, bool isEqualHeight, bool isError)
        {
            var instance = new MapData();

            var width = (MapSizeWidth) 30;
            var height = (MapSizeHeight) 24;

            instance.UpdateMapSize(width, height);

            var layerWidth = isEqualWidth ? width : (MapSizeWidth) (width + 1);
            var layerHeight = isEqualHeight ? height : (MapSizeHeight) (height + 1);

            var setLayer = new Layer();
            setLayer.UpdateSize(layerWidth, layerHeight);

            var errorOccured = false;
            try
            {
                instance.Layer3 = setLayer;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);

            if (errorOccured) return;

            // マップサイズが変化していないこと
            Assert.AreEqual(instance.MapSizeWidth, width);
            Assert.AreEqual(instance.MapSizeHeight, height);

            // レイヤーのサイズも変化していないこと
            Assert.AreEqual(instance.Layer1.Width, width);
            Assert.AreEqual(instance.Layer1.Height, height);
            Assert.AreEqual(instance.Layer2.Width, width);
            Assert.AreEqual(instance.Layer2.Height, height);
            Assert.AreEqual(instance.Layer3.Width, width);
            Assert.AreEqual(instance.Layer3.Height, height);
        }

        [Test]
        public static void UpdateMapSizeWidthTest()
        {
            var instance = new MapData();
            var width = (MapSizeWidth) 30;

            var errorOccured = false;
            try
            {
                instance.UpdateMapSizeWidth(width);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);

            // マップサイズ横が設定した値であること
            Assert.AreEqual(instance.MapSizeWidth, width);

            // 各レイヤのマップサイズ横が設定した値であること
            Assert.AreEqual(instance.Layer1.Width, width);
            Assert.AreEqual(instance.Layer2.Width, width);
            Assert.AreEqual(instance.Layer3.Width, width);
        }

        [Test]
        public static void UpdateMapSizeHeightTest()
        {
            var instance = new MapData();
            var height = (MapSizeHeight) 30;

            var errorOccured = false;
            try
            {
                instance.UpdateMapSizeHeight(height);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);

            // マップサイズ縦が設定した値であること
            Assert.AreEqual(instance.MapSizeHeight, height);

            // 各レイヤのマップサイズ縦が設定した値であること
            Assert.AreEqual(instance.Layer1.Height, height);
            Assert.AreEqual(instance.Layer2.Height, height);
            Assert.AreEqual(instance.Layer3.Height, height);
        }

        [Test]
        public static void ToBinaryMap023Test()
        {
            MapFileTestItemGenerator.OutputMapFile();
            var map023Data = MapFileTestItemGenerator.GenerateMap023Data();
            var map023DataBuf = map023Data.ToBinary();

            using (var fs = new FileStream($@"{MapFileTestItemGenerator.TestWorkRootDir}\Map023.mps", FileMode.Open))
            {
                var length = (int) fs.Length;
                // ファイルサイズが規定でない場合誤作動防止の為テスト失敗にする
                Assert.AreEqual(length, 6615);

                var fileData = new byte[length];
                fs.Read(fileData, 0, length);

                // binデータ出力用
                var builder = new StringBuilder();
                foreach (var str in fileData.Select((s, index) => $"=\"[{index}] = {{byte}} {s}\""))
                {
                    builder.AppendLine(str);
                }

                var result = builder.ToString();
                Console.WriteLine(result);

                builder = new StringBuilder();
                foreach (var str in map023DataBuf.Select((s, index) => $"=\"[{index}] = {{byte}} {s}\""))
                {
                    builder.AppendLine(str);
                }

                result = builder.ToString();
                Console.WriteLine(result);

                for (var i = 0; i < map023DataBuf.Length; i++)
                {
                    if (i == fileData.Length)
                        Assert.Fail(
                            $"データ帳が異なります。（期待値：{fileData.Length}, 実際：{map023DataBuf.Length}）");

                    if (fileData[i] != map023DataBuf[i])
                        Assert.Fail(
                            $"offset: {i} のバイナリが異なります。（期待値：{fileData[i]}, 実際：{map023DataBuf[i]}）");
                }

                if (fileData.Length != map023DataBuf.Length)
                    Assert.Fail(
                        $"データ帳が異なります。（期待値：{fileData.Length}, 実際：{map023DataBuf.Length}）");
            }
        }

        [Test]
        public static void ToBinaryMap024Test()
        {
            MapFileTestItemGenerator.OutputMapFile();
            var map024Data = MapFileTestItemGenerator.GenerateMap024Data();
            var map024DataBuf = map024Data.ToBinary();

            using (var fs = new FileStream($@"{MapFileTestItemGenerator.TestWorkRootDir}\Map024.mps", FileMode.Open))
            {
                var length = (int) fs.Length;
                // ファイルサイズが規定でない場合誤作動防止の為テスト失敗にする
                Assert.AreEqual(length, 6080);

                var fileData = new byte[length];
                fs.Read(fileData, 0, length);

                // binデータ出力用
                var builder = new StringBuilder();
                foreach (var str in fileData.Select((s, index) => $"=\"[{index}] = {{byte}} {s}\""))
                {
                    builder.AppendLine(str);
                }

                var result = builder.ToString();
                Console.WriteLine(result);

                builder = new StringBuilder();
                foreach (var str in map024DataBuf.Select((s, index) => $"=\"[{index}] = {{byte}} {s}\""))
                {
                    builder.AppendLine(str);
                }

                result = builder.ToString();
                Console.WriteLine(result);

                for (var i = 0; i < map024DataBuf.Length; i++)
                {
                    if (i == fileData.Length)
                        Assert.Fail(
                            $"データ帳が異なります。（期待値：{fileData.Length}, 実際：{map024DataBuf.Length}）");

                    if (fileData[i] != map024DataBuf[i])
                        Assert.Fail(
                            $"offset: {i} のバイナリが異なります。（期待値：{fileData[i]}, 実際：{map024DataBuf[i]}）");
                }

                if (fileData.Length != map024DataBuf.Length)
                    Assert.Fail(
                        $"データ帳が異なります。（期待値：{fileData.Length}, 実際：{map024DataBuf.Length}）");
            }
        }

        [Test]
        public static void ToBinaryMap025Test()
        {
            MapFileTestItemGenerator.OutputMapFile();
            var map025Data = MapFileTestItemGenerator.GenerateMap025Data();
            var map025DataBuf = map025Data.ToBinary();

            using (var fs = new FileStream($@"{MapFileTestItemGenerator.TestWorkRootDir}\Map025.mps", FileMode.Open))
            {
                var length = (int) fs.Length;
                // ファイルサイズが規定でない場合誤作動防止の為テスト失敗にする
                Assert.AreEqual(length, 9211);

                var fileData = new byte[length];
                fs.Read(fileData, 0, length);

                // binデータ出力用
                var builder = new StringBuilder();
                foreach (var str in fileData.Select((s, index) => $"=\"[{index}] = {{byte}} {s}\""))
                {
                    builder.AppendLine(str);
                }

                var result = builder.ToString();
                Console.WriteLine(result);

                builder = new StringBuilder();
                foreach (var str in map025DataBuf.Select((s, index) => $"=\"[{index}] = {{byte}} {s}\""))
                {
                    builder.AppendLine(str);
                }

                result = builder.ToString();
                Console.WriteLine(result);

                for (var i = 0; i < map025DataBuf.Length; i++)
                {
                    if (i == fileData.Length)
                        Assert.Fail(
                            $"データ帳が異なります。（期待値：{fileData.Length}, 実際：{map025DataBuf.Length}）");

                    if (fileData[i] != map025DataBuf[i])
                        Assert.Fail(
                            $"offset: {i} のバイナリが異なります。（期待値：{fileData[i]}, 実際：{map025DataBuf[i]}）");
                }

                if (fileData.Length != map025DataBuf.Length)
                    Assert.Fail(
                        $"データ帳が異なります。（期待値：{fileData.Length}, 実際：{map025DataBuf.Length}）");
            }
        }

        [Test]
        public static void SerializeTest()
        {
            var target = new MapData
            {
                TileSetId = 2,
            };
            var clone = DeepCloner.DeepClone(target);
            Assert.IsTrue(clone.Equals(target));
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            // テスト用ファイル削除
            MapFileTestItemGenerator.DeleteMapFile();
        }
    }
}