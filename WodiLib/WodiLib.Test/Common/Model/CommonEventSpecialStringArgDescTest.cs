using System;
using NUnit.Framework;
using WodiLib.Common;
using WodiLib.Sys.Cmn;
using WodiLib.Test.Tools;

namespace WodiLib.Test.Common
{
    [TestFixture]
    public class CommonEventSpecialStringArgDescTest
    {
        private static WodiLibLogger logger;

        [SetUp]
        public static void Setup()
        {
            LoggerInitializer.SetupWodiLibLoggerForDebug();
            logger = WodiLibLogger.GetInstance();
        }

        [Test]
        public static void ArgNameTest()
        {
            var instance = new CommonEventSpecialStringArgDesc();
            var argName = (CommonEventArgName) "test";

            var errorOccured = false;
            try
            {
                instance.ArgName = argName;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.IsFalse(errorOccured);

            var setValue = instance.ArgName;

            // セットした値と取得した値が一致すること
            Assert.IsTrue(setValue.Equals(argName));
        }

        [Test]
        public static void ArgTypeTest()
        {
            var instance = new CommonEventSpecialStringArgDesc();

            var errorOccured = false;
            try
            {
                var _ = instance.ArgType;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);

            var setValue = instance.ArgType;

            // 意図した値が取得できること
            Assert.AreEqual(setValue, CommonEventArgType.Normal);
        }

        [Test]
        public static void InitValueTest()
        {
            var instance = new CommonEventSpecialStringArgDesc();

            var errorOccured = false;
            try
            {
                var _ = instance.InitValue;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生すること
            Assert.IsTrue(errorOccured);
        }

        [Test]
        public static void GetSpecialCaseTest()
        {
            var instance = new CommonEventSpecialStringArgDesc();

            var errorOccured = false;
            try
            {
                instance.GetAllSpecialCase();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);

            // 選択肢数が意図した値と一致すること
            var caseLength = instance.GetAllSpecialCase().Count;
            Assert.AreEqual(caseLength, 0);
        }

        [Test]
        public static void GetAllSpecialCaseNumberTest()
        {
            var instance = new CommonEventSpecialStringArgDesc();

            var errorOccured = false;
            try
            {
                instance.GetAllSpecialCaseNumber();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);

            // 選択肢数が意図した値と一致すること
            var caseLength = instance.GetAllSpecialCase().Count;
            Assert.AreEqual(caseLength, 0);
        }

        [Test]
        public static void GetAllSpecialCaseDescriptionTest()
        {
            var instance = new CommonEventSpecialStringArgDesc();

            var errorOccured = false;
            try
            {
                instance.GetAllSpecialCaseNumber();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);

            // 選択肢数が意図した値と一致すること
            var caseLength = instance.GetAllSpecialCase().Count;
            Assert.AreEqual(caseLength, 0);
        }

        [Test]
        public static void SerializeTest()
        {
            var target = new CommonEventSpecialStringArgDesc
            {
                ArgName = "ArgName"
            };
            var clone = DeepCloner.DeepClone(target);
            Assert.IsTrue(clone.Equals(target));
        }
    }
}