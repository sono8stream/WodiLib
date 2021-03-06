using System;
using System.Collections.Generic;
using WodiLib.Map;
using NUnit.Framework;
using WodiLib.Event.EventCommand;
using WodiLib.Sys.Cmn;
using WodiLib.Test.Tools;

namespace WodiLib.Test.Map
{
    [TestFixture]
    public class MapEventOnePageTest
    {
        private static WodiLibLogger logger;

        [SetUp]
        public static void Setup()
        {
            LoggerInitializer.SetupWodiLibLoggerForDebug();
            logger = WodiLibLogger.GetInstance();
        }

        [TestCase(false, false)]
        [TestCase(true, true)]
        public static void GraphicInfoSetTest(bool isNull, bool isError)
        {
            var instance = new MapEventPage();
            var errorOccured = false;
            try
            {
                instance.GraphicInfo = isNull ? null : new MapEventPageGraphicInfo();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);
        }

        [TestCase(false, false)]
        [TestCase(true, true)]
        public static void BootInfoSetTest(bool isNull, bool isError)
        {
            var instance = new MapEventPage();
            var errorOccured = false;
            try
            {
                instance.BootInfo = isNull ? null : new MapEventPageBootInfo();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);
        }

        [TestCase(false, false)]
        [TestCase(true, true)]
        public static void MoveRouteInfoSetTest(bool isNull, bool isError)
        {
            var instance = new MapEventPage();
            var errorOccured = false;
            try
            {
                instance.MoveRouteInfo = isNull ? null : new MapEventPageMoveRouteInfo();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);
        }

        [TestCase(false, false)]
        [TestCase(true, true)]
        public static void SetOptionFlagTest(bool isNull, bool isError)
        {
            var instance = new MapEventPage();
            var errorOccured = false;
            try
            {
                instance.Option = isNull ? null : new MapEventPageOption();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);
        }

        private static readonly object[] EventCommandsSetTestCaseSource =
        {
            new object[] {new List<IEventCommand>(), false},
            new object[] {new List<IEventCommand> {new Blank()}, false},
            new object[] {null, true},
        };

        [TestCaseSource(nameof(EventCommandsSetTestCaseSource))]
        public static void EventCommandsSetTest(IReadOnlyList<IEventCommand> commands, bool isError)
        {
            var instance = new MapEventPage();
            var errorOccured = false;
            try
            {
                instance.EventCommands = new EventCommandList(commands);
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
        public static void SerializeTest()
        {
            var target = new MapEventPage
            {
                ShadowGraphicId = 4
            };
            var clone = DeepCloner.DeepClone(target);
            Assert.IsTrue(clone.Equals(target));
        }
    }
}