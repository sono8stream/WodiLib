using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using WodiLib.Sys;
using WodiLib.Sys.Cmn;
using WodiLib.Test.Tools;

namespace WodiLib.Test.Sys
{
    [TestFixture]
    public class RestrictedCapacityCollectionTest
    {
        private static WodiLibLogger logger;

        [SetUp]
        public static void Setup()
        {
            LoggerInitializer.SetupWodiLibLoggerForDebug();
            logger = WodiLibLogger.GetInstance();
        }

        [TestCase(TestClassType.Type1, false)]
        [TestCase(TestClassType.Type2, false)]
        [TestCase(TestClassType.Type3, false)]
        [TestCase(TestClassType.Type4, true)]
        [TestCase(TestClassType.Type5, true)]
        [TestCase(TestClassType.Type6, true)]
        public static void ConstructorTest1(TestClassType testType, bool isError)
        {
            var initLength = 0;

            var errorOccured = false;

            AbsCollectionTest instance = null;
            try
            {
                switch (testType)
                {
                    case TestClassType.Type1:
                        initLength = CollectionTest1.MinCapacity;
                        instance = new CollectionTest1();
                        break;
                    case TestClassType.Type2:
                        initLength = CollectionTest2.MinCapacity;
                        instance = new CollectionTest2();
                        break;
                    case TestClassType.Type3:
                        initLength = CollectionTest3.MinCapacity;
                        instance = new CollectionTest3();
                        break;
                    case TestClassType.Type4:
                        initLength = CollectionTest4.MinCapacity;
                        instance = new CollectionTest4();
                        break;
                    case TestClassType.Type5:
                        initLength = CollectionTest5.MinCapacity;
                        instance = new CollectionTest5();
                        break;
                    case TestClassType.Type6:
                        initLength = CollectionTest6.MinCapacity;
                        instance = new CollectionTest6();
                        break;
                    default:
                        Assert.Fail();
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

            if (errorOccured) return;

            // 初期要素が容量最小数と一致すること
            Assert.AreEqual(instance.Count, initLength);
        }

        [TestCase(TestClassType.Type1, -1, true)]
        [TestCase(TestClassType.Type1, 0, false)]
        [TestCase(TestClassType.Type1, 10, false)]
        [TestCase(TestClassType.Type1, 11, true)]
        [TestCase(TestClassType.Type2, -1, true)]
        [TestCase(TestClassType.Type2, 4, true)]
        [TestCase(TestClassType.Type2, 5, false)]
        [TestCase(TestClassType.Type2, 10, false)]
        [TestCase(TestClassType.Type2, 11, true)]
        [TestCase(TestClassType.Type3, -1, true)]
        [TestCase(TestClassType.Type3, 9, true)]
        [TestCase(TestClassType.Type3, 10, false)]
        [TestCase(TestClassType.Type3, 11, true)]
        [TestCase(TestClassType.Type4, -1, true)]
        [TestCase(TestClassType.Type4, 0, true)]
        [TestCase(TestClassType.Type4, 10, true)]
        [TestCase(TestClassType.Type4, 11, true)]
        [TestCase(TestClassType.Type5, -1, true)]
        [TestCase(TestClassType.Type5, 0, true)]
        [TestCase(TestClassType.Type5, 10, true)]
        [TestCase(TestClassType.Type5, 11, true)]
        [TestCase(TestClassType.Type6, -1, true)]
        [TestCase(TestClassType.Type6, 0, true)]
        [TestCase(TestClassType.Type6, 10, true)]
        [TestCase(TestClassType.Type6, 11, true)]
        public static void ConstructorTest2(TestClassType testType, int initLength, bool isError)
        {
            var errorOccured = false;

            var initList = MakeStringList(initLength);

            AbsCollectionTest instance = null;
            try
            {
                switch (testType)
                {
                    case TestClassType.Type1:
                        instance = new CollectionTest1(initList);
                        break;
                    case TestClassType.Type2:
                        instance = new CollectionTest2(initList);
                        break;
                    case TestClassType.Type3:
                        instance = new CollectionTest3(initList);
                        break;
                    case TestClassType.Type4:
                        instance = new CollectionTest4(initList);
                        break;
                    case TestClassType.Type5:
                        instance = new CollectionTest5(initList);
                        break;
                    case TestClassType.Type6:
                        instance = new CollectionTest6(initList);
                        break;
                    default:
                        Assert.Fail();
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

            if (errorOccured) return;

            // 要素数がセットした要素数と一致すること
            Assert.AreEqual(instance.Count, initLength);
        }

        [TestCase(-1, 7, true)]
        [TestCase(0, 7, false)]
        [TestCase(6, 7, false)]
        [TestCase(7, 7, true)]
        public static void IndexerGetTest(int index, int initLength, bool isError)
        {
            var instance = MakeCollectionForMethodTest(initLength, out var countDic);
            var errorOccured = false;
            try
            {
                var _ = instance[index];
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);

            // 各Virtualメソッドが一度も呼ばれていないこと
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnSetItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnInsertItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnRemoveItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnClearItemsCalled)], 0);
        }

        [TestCase(-1, "abc", 7, true)]
        [TestCase(0, "abc", 7, false)]
        [TestCase(6, "abc", 7, false)]
        [TestCase(7, "abc", 7, true)]
        [TestCase(-1, null, 7, true)]
        [TestCase(0, null, 7, true)]
        [TestCase(6, null, 7, true)]
        [TestCase(7, null, 7, true)]
        public static void IndexerSetTest(int index, string setItem, int initLength, bool isError)
        {
            var instance = MakeCollectionForMethodTest(initLength, out var countDic);

            var errorOccured = false;
            try
            {
                instance[index] = setItem;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);

            // 各Virtualメソッドが意図した回数呼ばれていること
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnSetItemCalled)], isError ? 0 : 1);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnInsertItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnRemoveItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnClearItemsCalled)], 0);

            if (errorOccured) return;

            for (var i = 0; i < initLength; i++)
            {
                if (i != index)
                {
                    // 初期値が変化していないこと
                    Assert.AreEqual(instance[i], i.ToString());
                }
                else
                {
                    // セットした値が反映されていること
                    Assert.AreEqual(instance[i], setItem);
                }
            }
        }

        // Count プロパティのテストは ConstructorTest1, ConstructorTest2 が兼ねる

        [TestCase(-1, -1, 7, true)]
        [TestCase(-1, 0, 7, true)]
        [TestCase(-1, 7, 7, true)]
        [TestCase(-1, 8, 7, true)]
        [TestCase(0, -1, 7, true)]
        [TestCase(0, 0, 7, false)]
        [TestCase(0, 7, 7, false)]
        [TestCase(0, 8, 7, true)]
        [TestCase(6, -1, 7, true)]
        [TestCase(6, 0, 7, false)]
        [TestCase(6, 1, 7, false)]
        [TestCase(6, 2, 7, true)]
        [TestCase(7, -1, 7, true)]
        [TestCase(7, 0, 7, true)]
        [TestCase(7, 1, 7, true)]
        [TestCase(7, 2, 7, true)]
        public static void GetRangeTest(int index, int count, int initLength, bool isError)
        {
            var instance = MakeCollectionForMethodTest(initLength, out var countDic);
            var errorOccured = false;
            IEnumerable<string> result = null;

            try
            {
                result = instance.GetRange(index, count);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);

            // 各Virtualメソッドが一度も呼ばれていないこと
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnSetItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnInsertItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnRemoveItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnClearItemsCalled)], 0);

            if (errorOccured || result == null) return;

            var resultArray = result as string[] ?? result.ToArray();

            // 取得した要素数が一致すること
            Assert.AreEqual(resultArray.Length, count);

            // 取得した要素が取得元と一致すること
            for (var i = 0; i < count; i++)
            {
                Assert.AreEqual(resultArray[i], instance[index + i]);
            }
        }

        [TestCase("", 0, false)]
        [TestCase("", 9, false)]
        [TestCase("", 10, true)]
        [TestCase(null, 0, true)]
        [TestCase(null, 9, true)]
        [TestCase(null, 10, true)]
        public static void AddTest(string item, int initLength, bool isError)
        {
            var instance = MakeCollectionForMethodTest(initLength, out var countDic);
            var errorOccured = false;

            try
            {
                instance.Add(item);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);

            // 各Virtualメソッドが意図した回数呼ばれていること
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnSetItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnInsertItemCalled)], isError ? 0 : 1);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnRemoveItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnClearItemsCalled)], 0);

            if (errorOccured) return;

            // 追加後の要素数が元の要素数+1であること
            Assert.AreEqual(instance.Count, initLength + 1);

            // 初期要素が変化していないこと
            for (var i = 0; i < initLength; i++)
            {
                Assert.AreEqual(instance[i], i.ToString());
            }

            // 末尾に意図した値が追加されていること
            Assert.AreEqual(instance[initLength], item);
        }

        [TestCase(0, -1, false, true)]
        [TestCase(0, 0, false, false)]
        [TestCase(0, 10, false, false)]
        [TestCase(0, 10, true, true)]
        [TestCase(0, 11, false, true)]
        [TestCase(0, 11, true, true)]
        [TestCase(7, -1, false, true)]
        [TestCase(7, 0, false, false)]
        [TestCase(7, 3, false, false)]
        [TestCase(7, 3, true, true)]
        [TestCase(7, 4, false, true)]
        [TestCase(7, 4, true, true)]
        public static void AddRangeTest(int initLength, int addLength, bool hasNullInAddLength, bool isError)
        {
            var instance = MakeCollectionForMethodTest(initLength, out var countDic);
            var addList = MakeStringList2(addLength, hasNullInAddLength);
            var errorOccured = false;

            try
            {
                instance.AddRange(addList);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);

            // 各Virtualメソッドが意図した回数呼ばれていること
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnSetItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnInsertItemCalled)], isError ? 0 : addLength);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnRemoveItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnClearItemsCalled)], 0);

            if (errorOccured) return;

            // 追加後の要素数が元の要素数+追加した要素数であること
            Assert.AreEqual(instance.Count, initLength + addLength);

            // 初期要素が変化していないこと
            for (var i = 0; i < initLength; i++)
            {
                Assert.AreEqual(instance[i], i.ToString());
            }

            // 末尾に意図した値が追加されていること
            for (var i = 0; i < addLength; i++)
            {
                Assert.AreEqual(instance[initLength + i], (i * 100).ToString());
            }
        }

        [TestCase(-1, "", 0, true)]
        [TestCase(-1, "", 9, true)]
        [TestCase(-1, "", 10, true)]
        [TestCase(-1, null, 0, true)]
        [TestCase(-1, null, 9, true)]
        [TestCase(-1, null, 10, true)]
        [TestCase(0, "", 0, false)]
        [TestCase(0, "", 9, false)]
        [TestCase(0, "", 10, true)]
        [TestCase(0, null, 0, true)]
        [TestCase(0, null, 9, true)]
        [TestCase(0, null, 10, true)]
        [TestCase(9, "", 0, true)]
        [TestCase(9, "", 9, false)]
        [TestCase(9, "", 10, true)]
        [TestCase(9, null, 0, true)]
        [TestCase(9, null, 9, true)]
        [TestCase(9, null, 10, true)]
        [TestCase(10, "", 0, true)]
        [TestCase(10, "", 9, true)]
        [TestCase(10, "", 10, true)]
        [TestCase(10, null, 0, true)]
        [TestCase(10, null, 9, true)]
        [TestCase(10, null, 10, true)]
        public static void InsertTest(int index, string item, int initLength, bool isError)
        {
            var instance = MakeCollectionForMethodTest(initLength, out var countDic);
            var errorOccured = false;

            try
            {
                instance.Insert(index, item);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);

            // 各Virtualメソッドが意図した回数呼ばれていること
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnSetItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnInsertItemCalled)], isError ? 0 : 1);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnRemoveItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnClearItemsCalled)], 0);

            if (errorOccured) return;

            // 追加後の要素数が元の要素数+1であること
            Assert.AreEqual(instance.Count, initLength + 1);

            // 初期要素（挿入位置より前）が変化していないこと
            for (var i = 0; i < index; i++)
            {
                Assert.AreEqual(instance[i], i.ToString());
            }

            // 挿入位置に意図した値が追加されていること
            Assert.AreEqual(instance[index], item);

            // 初期要素（挿入位置より後）が変化していないこと
            for (var i = index + 1; i < initLength + 1; i++)
            {
                Assert.AreEqual(instance[i], (i - 1).ToString());
            }
        }

        [TestCase(0, -1, -1, false, true)]
        [TestCase(0, -1, 0, false, true)]
        [TestCase(0, -1, 10, false, true)]
        [TestCase(0, -1, 10, true, true)]
        [TestCase(0, -1, 11, false, true)]
        [TestCase(0, -1, 11, true, true)]
        [TestCase(0, -1, -1, false, true)]
        [TestCase(0, 0, 0, false, false)]
        [TestCase(0, 0, 10, false, false)]
        [TestCase(0, 0, 10, true, true)]
        [TestCase(0, 0, 11, false, true)]
        [TestCase(0, 0, 11, true, true)]
        [TestCase(0, 1, 0, false, true)]
        [TestCase(0, 1, 10, false, true)]
        [TestCase(0, 1, 10, true, true)]
        [TestCase(0, 1, 11, false, true)]
        [TestCase(0, 1, 11, true, true)]
        [TestCase(7, -1, -1, false, true)]
        [TestCase(7, -1, 0, false, true)]
        [TestCase(7, -1, 3, false, true)]
        [TestCase(7, -1, 3, true, true)]
        [TestCase(7, -1, 4, false, true)]
        [TestCase(7, -1, 4, true, true)]
        [TestCase(7, 0, -1, false, true)]
        [TestCase(7, 0, 0, false, false)]
        [TestCase(7, 0, 3, false, false)]
        [TestCase(7, 0, 3, true, true)]
        [TestCase(7, 0, 4, false, true)]
        [TestCase(7, 0, 4, true, true)]
        [TestCase(7, 7, -1, false, true)]
        [TestCase(7, 7, 0, false, false)]
        [TestCase(7, 7, 3, false, false)]
        [TestCase(7, 7, 3, true, true)]
        [TestCase(7, 7, 4, false, true)]
        [TestCase(7, 7, 4, true, true)]
        [TestCase(7, 8, -1, false, true)]
        [TestCase(7, 8, 0, false, true)]
        [TestCase(7, 8, 3, false, true)]
        [TestCase(7, 8, 3, true, true)]
        [TestCase(7, 8, 4, false, true)]
        [TestCase(7, 8, 4, true, true)]
        public static void InsertRangeTest(int initLength, int index, int addLength, bool hasNullInAddLength, bool isError)
        {
            var instance = MakeCollectionForMethodTest(initLength, out var countDic);
            var addList = MakeStringList2(addLength, hasNullInAddLength);
            var errorOccured = false;

            try
            {
                instance.InsertRange(index, addList);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);

            // 各Virtualメソッドが意図した回数呼ばれていること
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnSetItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnInsertItemCalled)], isError ? 0 : addLength);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnRemoveItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnClearItemsCalled)], 0);

            if (errorOccured) return;

            // 追加後の要素数が元の要素数+追加した要素数であること
            Assert.AreEqual(instance.Count, initLength + addLength);

            // 初期要素（挿入位置より前）が変化していないこと
            for (var i = 0; i < index; i++)
            {
                Assert.AreEqual(instance[i], i.ToString());
            }

            // 挿入位置に意図した値が追加されていること
            for (var i = 0; i < addLength; i++)
            {
                Assert.AreEqual(instance[i + index], (i * 100).ToString());
            }

            // 初期要素（挿入位置より後）が変化していないこと
            for (var i = index + addLength; i < initLength + addLength; i++)
            {
                Assert.AreEqual(instance[i], (i - addLength).ToString());
            }
        }

        [TestCase(5, -1, false, false)]
        [TestCase(5, 0, true, false)]
        [TestCase(5, 4, true, false)]
        [TestCase(5, 5, false, false)]
        [TestCase(7, -1, false, false)]
        [TestCase(7, 0, false, true)]
        [TestCase(7, 6, false, true)]
        [TestCase(7, 7, false, false)]
        public static void RemoveTest(int initLength, int removeIndex, bool isError, bool removeResult)
        {
            // initLength == removeIndex のとき、remove対象は要素中に含まれないものになる

            var initStrLength = initLength > removeIndex + 1 ? initLength : removeIndex + 1;
            var initStrList = MakeStringList(initStrLength);

            var instance = MakeCollection2ForMethodTest(initStrList, initLength, out var countDic);

            var errorOccured = false;
            var result = false;
            try
            {
                var removeItem = removeIndex == -1 ? null : initStrList[removeIndex];
                result = instance.Remove(removeItem);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);

            // Remove() の結果が意図した値であること
            Assert.AreEqual(result, removeResult);

            // 各Virtualメソッドが意図した回数呼ばれていること
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnSetItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnInsertItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnRemoveItemCalled)], !isError && removeResult ? 1 : 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnClearItemsCalled)], 0);

            if (errorOccured) return;

            if (!removeResult)
            {
                // ---------- 削除に失敗している場合 ----------

                // 削除後の要素数が元の要素数と一致すること
                Assert.AreEqual(instance.Count, initLength);

                // 要素が変化していないこと
                for (var i = 0; i < initLength; i++)
                {
                    Assert.AreEqual(instance[i], i.ToString());
                }

                return;
            }

            // ---------- 削除に成功している場合 ----------

            // 削除後の要素数が元の要素数-1であること
            Assert.AreEqual(instance.Count, initLength - 1);

            // 初期要素（削除位置より前）が変化していないこと
            for (var i = 0; i < removeIndex; i++)
            {
                Assert.AreEqual(instance[i], i.ToString());
            }

            // 初期要素（削除位置より後）が変化していないこと
            for (var i = removeIndex + 1; i < initLength - 1; i++)
            {
                Assert.AreEqual(instance[i], (i + 1).ToString());
            }
        }

        [TestCase(5, -1, true)]
        [TestCase(5, 0, true)]
        [TestCase(5, 4, true)]
        [TestCase(5, 5, true)]
        [TestCase(7, -1, true)]
        [TestCase(7, 0, false)]
        [TestCase(7, 6, false)]
        [TestCase(7, 7, true)]
        public static void RemoveAtTest(int initLength, int index, bool isError)
        {
            var initStrList = MakeStringList(initLength);
            var instance = MakeCollection2ForMethodTest(initStrList, initLength, out var countDic);

            var errorOccured = false;
            try
            {
                instance.RemoveAt(index);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);

            // 各Virtualメソッドが意図した回数呼ばれていること
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnSetItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnInsertItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnRemoveItemCalled)], isError ? 0 : 1);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnClearItemsCalled)], 0);

            if (errorOccured) return;

            // 削除後の要素数が元の要素数-1であること
            Assert.AreEqual(instance.Count, initLength - 1);

            // 初期要素（削除位置より前）が変化していないこと
            for (var i = 0; i < index; i++)
            {
                Assert.AreEqual(instance[i], i.ToString());
            }

            // 初期要素（削除位置より後）が変化していないこと
            for (var i = index + 1; i < initLength - 1; i++)
            {
                Assert.AreEqual(instance[i], (i + 1).ToString());
            }
        }

        [TestCase(5, -1, -1, true)]
        [TestCase(5, 0, -1, true)]
        [TestCase(5, 4, -1, true)]
        [TestCase(5, 5, -1, true)]
        [TestCase(5, -1, 0, true)]
        [TestCase(5, 0, 0, false)]
        [TestCase(5, 4, 0, false)]
        [TestCase(5, 5, 0, true)]
        [TestCase(5, -1, 1, true)]
        [TestCase(5, 0, 1, true)]
        [TestCase(5, 4, 1, true)]
        [TestCase(5, 5, 1, true)]
        [TestCase(7, -1, -1, true)]
        [TestCase(7, 0, -1, true)]
        [TestCase(7, 6, -1, true)]
        [TestCase(7, 7, -1, true)]
        [TestCase(7, -1, 0, true)]
        [TestCase(7, 0, 0, false)]
        [TestCase(7, 6, 0, false)]
        [TestCase(7, 7, 0, true)]
        [TestCase(7, -1, 1, true)]
        [TestCase(7, 0, 1, false)]
        [TestCase(7, 6, 1, false)]
        [TestCase(7, 7, 1, true)]
        [TestCase(7, -1, 2, true)]
        [TestCase(7, 0, 2, false)]
        [TestCase(7, 5, 2, false)]
        [TestCase(7, 6, 2, true)]
        [TestCase(7, -1, 3, true)]
        [TestCase(7, 0, 3, true)]
        [TestCase(7, 4, 3, true)]
        [TestCase(7, 5, 3, true)]
        public static void RemoveRangeTest(int initLength, int index, int count,
            bool isError)
        {
            var initStrList = MakeStringList(initLength);
            var instance = MakeCollection2ForMethodTest(initStrList, initLength, out var countDic);

            var errorOccured = false;
            try
            {
                instance.RemoveRange(index, count);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);

            // 各Virtualメソッドが意図した回数呼ばれていること
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnSetItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnInsertItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnRemoveItemCalled)], isError ? 0 : count);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnClearItemsCalled)], 0);

            if (errorOccured) return;

            // 削除後の要素数が元の要素数-削除数であること
            Assert.AreEqual(instance.Count, initLength - count);

            // 初期要素（削除位置より前）が変化していないこと
            for (var i = 0; i < index; i++)
            {
                Assert.AreEqual(instance[i], i.ToString());
            }

            // 初期要素（削除位置より後）が変化していないこと
            for (var i = index + count; i < initLength - count; i++)
            {
                Assert.AreEqual(instance[i], (i + count).ToString());
            }
        }

        [TestCase(TestClassType.Type1, 0)]
        [TestCase(TestClassType.Type1, 10)]
        [TestCase(TestClassType.Type2, 5)]
        [TestCase(TestClassType.Type2, 10)]
        public static void ClearTest(TestClassType classType, int initLength)
        {
            var initList = MakeStringList(initLength);

            AbsCollectionTest instance = null;
            Dictionary<string, int> countDic = null;
            switch (classType)
            {
                case TestClassType.Type1:
                    instance = MakeCollectionForMethodTest(initLength, out countDic);
                    break;
                case TestClassType.Type2:
                    instance = MakeCollection2ForMethodTest(initList, initLength, out countDic);
                    break;
                default:
                    Assert.Fail();
                    break;
            }
            var minCapacity = instance.GetMinCapacity();

            var errorOccured = false;
            try
            {
                instance.Clear();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);

            // 各Virtualメソッドが意図した回数呼ばれていること
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnSetItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnInsertItemCalled)], minCapacity);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnRemoveItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnClearItemsCalled)], 1);

            // 要素数が容量最小数と一致すること
            Assert.AreEqual(instance.Count, minCapacity);

            // すべての要素がデフォルト要素と一致すること
            foreach (var t in instance)
            {
                Assert.AreEqual(t, "test");
            }
        }

        [TestCase(5, "1", true)]
        [TestCase(5, "6", false)]
        [TestCase(5, null, false)]
        public static void ContainsTest(int initLength, string item, bool result)
        {
            var instance = MakeCollectionForMethodTest(initLength, out var countDic);
            var containsResult = false;

            var errorOccured = false;
            try
            {
                containsResult = instance.Contains(item);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);

            // 取得した値が意図した値と一致すること
            Assert.AreEqual(containsResult, result);

            // 各Virtualメソッドが呼ばれていないこと
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnSetItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnInsertItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnRemoveItemCalled)], 0);
            Assert.AreEqual(countDic[nameof(CollectionTest1.OnClearItemsCalled)], 0);
        }

        [TestCase(5, "1", 1)]
        [TestCase(5, "6", -1)]
        [TestCase(5, null, -1)]
        public static void IndexOfTest(int initLength, string item, int result)
        {
            var instance = MakeCollectionForMethodTest(initLength, out _);
            var indexOfResult = -1;

            var errorOccured = false;
            try
            {
                indexOfResult = instance.IndexOf(item);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);

            // 取得した値が意図した値と一致すること
            Assert.AreEqual(indexOfResult, result);


            // 初期値が変化していないこと
            for (var i = 0; i < initLength; i++)
            {
                Assert.AreEqual(instance[i], i.ToString());
            }
        }

        public static void CopyToTest(int initLength, int arrayLength, int index, bool isError)
        {
            var instance = MakeCollectionForMethodTest(initLength, out _);
            var copyArray = MakeStringArray(arrayLength);

            var errorOccured = false;
            try
            {
                instance.CopyTo(copyArray, index);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);

            // 初期値が変化していないこと
            for (var i = 0; i < initLength; i++)
            {
                Assert.AreEqual(instance[i], i.ToString());
            }

            if (errorOccured) return;

            // 配列の要素（コピー領域より前）が変化していないこと
            for (var i = 0; i < index; i++)
            {
                Assert.AreEqual(copyArray[i], (i * 100).ToString());
            }

            // 配列の要素（コピーした領域）がコピーした内容で上書きされていること
            for (var i = 0; i < initLength; i++)
            {
                Assert.AreEqual(copyArray[i + index], i.ToString());
            }

            // 配列の要素（コピー領域より後）が変化していないこと
            for (var i = 0; i < initLength - index; i++)
            {
                Assert.AreEqual(copyArray[i + initLength - index], (i * 100).ToString());
            }
        }

        [Test]
        public static void GetEnumeratorTest()
        {
            var instance = MakeCollectionForMethodTest(5, out _);
            // foreachを用いた処理で要素が正しく取得できること
            var i = 0;
            foreach (var value in instance)
            {
                Assert.AreEqual(value, i.ToString());
                i++;
            }
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //      テストデータ
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        private static List<string> MakeStringList(int length)
        {
            if (length < 0) return null;
            var result = new List<string>();
            for (var i = 0; i < length; i++)
            {
                result.Add(i.ToString());
            }

            return result;
        }

        private static List<string> MakeStringList2(int length, bool hasNull)
        {
            // hasNullInAddLength = true の場合、リスト中にひとつだけnullを混ぜる
            if (length < 0) return null;
            var result = new List<string>();
            for (var i = 0; i < length; i++)
            {
                result.Add(
                    hasNull && i == length / 2
                        ? null
                        : (i * 100).ToString()
                );
            }

            return result;
        }

        private static string[] MakeStringArray(int length)
        {
            var result = new string[length];
            for (var i = 0; i < length; i++)
            {
                result[i] = (i * 100).ToString();
            }

            return result;
        }

        private static CollectionTest1 MakeCollectionForMethodTest(int initLength, out Dictionary<string, int> methodCalledCount)
        {
            var initStringList = MakeStringList(initLength);
            var result = initStringList == null
                ? new CollectionTest1()
                : new CollectionTest1(initStringList);

            methodCalledCount = new Dictionary<string, int>
            {
                {nameof(CollectionTest1.OnSetItemCalled), 0},
                {nameof(CollectionTest1.OnInsertItemCalled), 0},
                {nameof(CollectionTest1.OnRemoveItemCalled), 0},
                {nameof(CollectionTest1.OnClearItemsCalled), 0},
            };

            var ints = methodCalledCount;
            result.OnSetItemCalled = () => ints[nameof(CollectionTest1.OnSetItemCalled)]++;
            result.OnInsertItemCalled = () => ints[nameof(CollectionTest1.OnInsertItemCalled)]++;
            result.OnRemoveItemCalled = () => ints[nameof(CollectionTest1.OnRemoveItemCalled)]++;
            result.OnClearItemsCalled = () => ints[nameof(CollectionTest1.OnClearItemsCalled)]++;

            return result;
        }

        private static CollectionTest2 MakeCollection2ForMethodTest(List<string> initStringList, int initLength, out Dictionary<string, int> methodCalledCount)
        {
            var result = initStringList == null
                ? new CollectionTest2()
                : new CollectionTest2(initStringList.GetRange(0, initLength));

            methodCalledCount = new Dictionary<string, int>
            {
                {nameof(CollectionTest1.OnSetItemCalled), 0},
                {nameof(CollectionTest1.OnInsertItemCalled), 0},
                {nameof(CollectionTest1.OnRemoveItemCalled), 0},
                {nameof(CollectionTest1.OnClearItemsCalled), 0},
            };

            var ints = methodCalledCount;
            result.OnSetItemCalled = () => ints[nameof(CollectionTest1.OnSetItemCalled)]++;
            result.OnInsertItemCalled = () => ints[nameof(CollectionTest1.OnInsertItemCalled)]++;
            result.OnRemoveItemCalled = () => ints[nameof(CollectionTest1.OnRemoveItemCalled)]++;
            result.OnClearItemsCalled = () => ints[nameof(CollectionTest1.OnClearItemsCalled)]++;

            return result;
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //      テスト用クラス
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        public enum TestClassType
        {
            Type1, Type2, Type3, Type4, Type5, Type6
        }

        private abstract class AbsCollectionTest : RestrictedCapacityCollection<string>
        {
            public AbsCollectionTest() { }
            public AbsCollectionTest(IReadOnlyCollection<string> list) : base(list) { }
        }

        private class CollectionTest1 : AbsCollectionTest
        {
            /**
             * 正常設定
             */

            public static int MaxCapacity => 10;
            public static int MinCapacity => 0;
            public static string Default => "test";

            public Action OnSetItemCalled { get; set; }
            public Action OnInsertItemCalled { get; set; }
            public Action OnRemoveItemCalled { get; set; }
            public Action OnClearItemsCalled { get; set; }

            public override int GetMaxCapacity() => MaxCapacity;

            public override int GetMinCapacity() => MinCapacity;

            protected override string MakeDefaultItem() => Default;

            public CollectionTest1() { }

            public CollectionTest1(IReadOnlyCollection<string> list) : base(list) { }

            protected override void SetItem(int index, string item)
            {
                base.SetItem(index, item);
                OnSetItemCalled?.Invoke();
            }

            protected override void InsertItem(int index, string item)
            {
                base.InsertItem(index, item);
                OnInsertItemCalled?.Invoke();
            }

            protected override void RemoveItem(int index)
            {
                base.RemoveItem(index);
                OnRemoveItemCalled?.Invoke();
            }

            protected override void ClearItems()
            {
                base.ClearItems();
                OnClearItemsCalled?.Invoke();
            }
        }

        private class CollectionTest2 : AbsCollectionTest
        {
            /**
             * 正常設定
             * 初期要素数非0
             */

            public static int MaxCapacity => 10;
            public static int MinCapacity => 5;
            public static string Default => "test";

            public Action OnSetItemCalled { get; set; }
            public Action OnInsertItemCalled { get; set; }
            public Action OnRemoveItemCalled { get; set; }
            public Action OnClearItemsCalled { get; set; }

            public override int GetMaxCapacity() => MaxCapacity;

            public override int GetMinCapacity() => MinCapacity;

            protected override string MakeDefaultItem() => Default;

            public CollectionTest2() { }

            public CollectionTest2(IReadOnlyCollection<string> list) : base(list) { }

            protected override void SetItem(int index, string item)
            {
                base.SetItem(index, item);
                OnSetItemCalled?.Invoke();
            }

            protected override void InsertItem(int index, string item)
            {
                base.InsertItem(index, item);
                OnInsertItemCalled?.Invoke();
            }

            protected override void RemoveItem(int index)
            {
                base.RemoveItem(index);
                OnRemoveItemCalled?.Invoke();
            }

            protected override void ClearItems()
            {
                base.ClearItems();
                OnClearItemsCalled?.Invoke();
            }
        }

        private class CollectionTest3 : AbsCollectionTest
        {
            /**
             * 正常設定
             * 初期要素数非0
             * MinCapacity = MaxCapacity
             */

            public static int MaxCapacity => 10;
            public static int MinCapacity => 10;
            public static string Default => "test";

            public override int GetMaxCapacity() => MaxCapacity;

            public override int GetMinCapacity() => MinCapacity;

            protected override string MakeDefaultItem() => Default;

            public CollectionTest3() { }

            public CollectionTest3(IReadOnlyCollection<string> list) : base(list) { }
        }

        private class CollectionTest4 : AbsCollectionTest
        {
            /**
             * 異常設定（MinCapacity < 0）
             */

            public static int MaxCapacity => 10;
            public static int MinCapacity => -2;
            public static string Default => "test";

            public override int GetMaxCapacity() => MaxCapacity;

            public override int GetMinCapacity() => MinCapacity;

            protected override string MakeDefaultItem() => Default;

            public CollectionTest4() { }

            public CollectionTest4(IReadOnlyCollection<string> list) : base(list) { }
        }

        private class CollectionTest5 : AbsCollectionTest
        {
            /**
             * 異常設定（MinCapacity > MaxCapacity）
             */

            public static int MaxCapacity => 10;
            public static int MinCapacity => 11;
            public static string Default => "test";

            public override int GetMaxCapacity() => MaxCapacity;

            public override int GetMinCapacity() => MinCapacity;

            protected override string MakeDefaultItem() => Default;

            public CollectionTest5() { }

            public CollectionTest5(IReadOnlyCollection<string> list) : base(list) { }
        }

        private class CollectionTest6 : AbsCollectionTest
        {
            /**
             * 異常設定（DefaultValue＝null）
             */
            public static int MaxCapacity => 10;
            public static int MinCapacity => 0;
            public static string Default => null;

            public override int GetMaxCapacity() => MaxCapacity;

            public override int GetMinCapacity() => MinCapacity;

            protected override string MakeDefaultItem() => Default;

            public CollectionTest6() { }

            public CollectionTest6(IReadOnlyCollection<string> list) : base(list) { }
        }

    }
}