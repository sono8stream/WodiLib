using NUnit.Framework;
using WodiLib.Database;
using WodiLib.Test.Tools;

namespace WodiLib.Test.Database
{
    [TestFixture]
    public class DBTypeSettingListTest
    {
        [Test]
        public static void GetMaxCapacityTest()
        {
            var instance = new DBTypeSettingList();
            var maxCapacity = instance.GetMaxCapacity();

            // 取得した値が容量最大値と一致すること
            Assert.AreEqual(maxCapacity, DBTypeSettingList.MaxCapacity);
        }

        [Test]
        public static void GetMinCapacityTest()
        {
            var instance = new DBTypeSettingList();
            var maxCapacity = instance.GetMinCapacity();

            // 取得した値が容量最大値と一致すること
            Assert.AreEqual(maxCapacity, DBTypeSettingList.MinCapacity);
        }

        [Test]
        public static void SerializeTest()
        {
            var target = new DBTypeSettingList();
            target.AdjustLength(3);
            var clone = DeepCloner.DeepClone(target);
            Assert.IsTrue(clone.Equals(target));
        }
    }
}