using NUnit.Framework;
using WodiLib.Ini;
using WodiLib.Test.Tools;

namespace WodiLib.Test.Ini.Model
{
    [TestFixture]
    public class EventCommandPositionListTest
    {
        [Test]
        public static void GetMaxCapacityTest()
        {
            var instance = new ShortCutPositionList();
            var maxCapacity = instance.GetMaxCapacity();

            // 取得した値が容量最大値と一致すること
            Assert.AreEqual(maxCapacity, ShortCutPositionList.MaxCapacity);
        }

        [Test]
        public static void GetMinCapacityTest()
        {
            var instance = new ShortCutPositionList();
            var maxCapacity = instance.GetMinCapacity();

            // 取得した値が容量最大値と一致すること
            Assert.AreEqual(maxCapacity, ShortCutPositionList.MinCapacity);
        }

        [Test]
        public static void SerializeTest()
        {
            var target = new ShortCutPositionList
            {
                [2] = new ShortCutPosition(2)
            };
            var clone = DeepCloner.DeepClone(target);
            Assert.IsTrue(clone.Equals(target));
        }
    }
}