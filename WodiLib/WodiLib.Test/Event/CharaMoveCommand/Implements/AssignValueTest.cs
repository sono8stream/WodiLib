using System;
using NUnit.Framework;
using WodiLib.Event.CharaMoveCommand;
using WodiLib.Sys.Cmn;
using WodiLib.Test.Tools;

namespace WodiLib.Test.Event.CharaMoveCommand
{
    [TestFixture]
    public class AssignValueTest
    {
        private static WodiLibLogger logger;

        [SetUp]
        public static void Setup()
        {
            LoggerInitializer.SetupWodiLibLoggerForDebug();
            logger = WodiLibLogger.GetInstance();
        }

        [TestCase(1000000, "m", true, 0)]
        [TestCase(1000000, "c", true, 0)]
        [TestCase(1099999, "m", true, 0)]
        [TestCase(1099999, "c", true, 0)]
        [TestCase(1100000, "m", false, 1100000)]
        [TestCase(1100000, "c", false, 1600000)]
        [TestCase(1100009, "m", false, 1100009)]
        [TestCase(1100009, "c", false, 1600009)]
        [TestCase(1100010, "m", true, 0)]
        [TestCase(1100010, "c", true, 0)]
        [TestCase(1600000, "m", false, 1100000)]
        [TestCase(1600000, "c", false, 1600000)]
        [TestCase(1600009, "m", false, 1100009)]
        [TestCase(1600009, "c", false, 1600009)]
        [TestCase(1600010, "m", true, 0)]
        [TestCase(1600010, "c", true, 0)]
        [TestCase(1999999, "m", true, 0)]
        [TestCase(1999999, "c", true, 0)]
        [TestCase(2000000, "m", false, 2000000)]
        [TestCase(2000000, "c", false, 2000000)]
        [TestCase(2099999, "m", false, 2099999)]
        [TestCase(2099999, "c", false, 2099999)]
        [TestCase(2100000, "m", true, 2100000)]
        [TestCase(2100000, "c", true, 2100000)]
        public static void TargetAddressTest(int initValue, string targetOwnerId,
            bool isError, int answerValue)
        {
            var instance = new AssignValue
            {
                Owner = targetOwnerId.Equals("m")
                    ? TargetAddressOwner.MapEvent
                    : TargetAddressOwner.CommonEvent
            };

            var errorOccured = false;
            try
            {
                instance.TargetAddress = initValue;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);

            if (errorOccured) return;

            var resultValue = (int) instance.TargetAddress;

            // 取得した値外とした値と一致すること
            Assert.AreEqual(resultValue, answerValue);
        }

        [Test]
        public static void SerializeTest()
        {
            var target = new AssignValue
            {
                Value = 21,
                Owner = TargetAddressOwner.CommonEvent
            };
            var clone = DeepCloner.DeepClone(target);
            Assert.IsTrue(clone.Equals(target));
        }
    }
}