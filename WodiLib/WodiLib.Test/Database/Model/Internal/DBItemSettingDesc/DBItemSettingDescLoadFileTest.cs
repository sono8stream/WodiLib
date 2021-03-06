using System;
using System.Collections.Generic;
using NUnit.Framework;
using WodiLib.Database;
using WodiLib.Sys.Cmn;
using WodiLib.Test.Tools;

namespace WodiLib.Test.Database.Internal.DBItemSettingDesc
{
    [TestFixture]
    public class DBItemSettingDescLoadFIleTest
    {
        private static WodiLibLogger logger;

        [SetUp]
        public static void Setup()
        {
            LoggerInitializer.SetupWodiLibLoggerForDebug();
            logger = WodiLibLogger.GetInstance();
        }

        [Test]
        public static void SettingTypeTest()
        {
            var instance = new DBItemSettingDescLoadFile((0, ""));

            // プロパティ外とした値と一致すること
            Assert.AreEqual(instance.SettingType, DBItemSpecialSettingType.LoadFile);
        }

        [Test]
        public static void DatabaseReferKindGetterTest()
        {
            var instance = new DBItemSettingDescNormal();

            var errorOccured = false;
            try
            {
                var _ = instance.DatabaseReferKind;
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
        public static void DatabaseReferKindSetterTest()
        {
            var instance = new DBItemSettingDescNormal();

            var errorOccured = false;
            try
            {
                instance.DatabaseReferKind = DBReferType.User;
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
        public static void DatabaseDbTypeIdGetterTest()
        {
            var instance = new DBItemSettingDescNormal();

            var errorOccured = false;
            try
            {
                var _ = instance.DatabaseDbTypeId;
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
        public static void DatabaseDbTypeIdSetterTest()
        {
            var instance = new DBItemSettingDescNormal();

            var errorOccured = false;
            try
            {
                instance.DatabaseDbTypeId = 0;
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
        public static void DatabaseUseAdditionalItemsFlagGetterTest()
        {
            var instance = new DBItemSettingDescNormal();

            var errorOccured = false;
            try
            {
                var _ = instance.DatabaseUseAdditionalItemsFlag;
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
        public static void DatabaseUseAdditionalItemsFlagSetterTest()
        {
            var instance = new DBItemSettingDescNormal();

            var errorOccured = false;
            try
            {
                instance.DatabaseUseAdditionalItemsFlag = true;
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
        public static void FolderNameGetterTest()
        {
            var instance = new DBItemSettingDescLoadFile((0, ""));

            var errorOccured = false;
            try
            {
                var _ = instance.FolderName;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);
        }

        [TestCase(false, false)]
        [TestCase(true, true)]
        public static void FolderNameSetterTest(bool isSetNull, bool isError)
        {
            var instance = new DBItemSettingDescLoadFile((0, ""));

            var setValue = isSetNull ? null : (DBSettingFolderName) "";

            var errorOccured = false;
            try
            {
                instance.FolderName = setValue;
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
        public static void OmissionFolderNameFlagGetterTest()
        {
            var instance = new DBItemSettingDescLoadFile((0, ""));

            var errorOccured = false;
            try
            {
                var _ = instance.OmissionFolderNameFlag;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);
        }

        [Test]
        public static void OmissionFolderNameFlagSetterTest()
        {
            var instance = new DBItemSettingDescLoadFile((0, ""));

            var errorOccured = false;
            try
            {
                instance.OmissionFolderNameFlag = false;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);
        }

        [Test]
        public static void DefaultType()
        {
            var instance = new DBItemSettingDescLoadFile();

            var errorOccured = false;
            try
            {
                var _ = instance.DefaultType;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);
        }

        [TestCase(0, null, false)]
        [TestCase(-1, "", true)]
        [TestCase(-1, "あいうえお", true)]
        [TestCase(0, "", false)]
        [TestCase(0, "あいうえお", false)]
        [TestCase(1, "", false)]
        [TestCase(1, "あいうえお", false)]
        [TestCase(2, "", true)]
        [TestCase(2, "あいうえお", true)]
        public static void ConstructorTest(int caseNumber, string description, bool isError)
        {
            var valueCase = description == null
                ? null
                : new DatabaseValueCase(caseNumber, description);

            var errorOccured = false;
            try
            {
                var _ = new DBItemSettingDescLoadFile(valueCase);
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
        public static void GetAllSpecialCaseTest()
        {
            var instance = new DBItemSettingDescLoadFile((0, ""));

            List<DatabaseValueCase> allCase = null;

            var errorOccured = false;
            try
            {
                allCase = instance.GetAllSpecialCase();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);

            // 取得した要素数が1であること
            Assert.AreEqual(allCase.Count, 1);
        }

        [TestCase(true, 1)]
        [TestCase(false, 0)]
        public static void GetAllSpecialCaseNumberTest(bool isOmission, int resultCaseNumber)
        {
            var instance = new DBItemSettingDescLoadFile((0, ""));
            instance.UpdateOmissionFolderNameFlag(isOmission);

            List<DatabaseValueCaseNumber> result = null;

            var errorOccured = false;
            try
            {
                result = instance.GetAllSpecialCaseNumber();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);

            // 取得した要素数が1であること
            Assert.AreEqual(result.Count, 1);

            // 取得した要素がそれぞれ意図した値であること
            Assert.AreEqual((int) result[0], resultCaseNumber);
        }

        [TestCase(false, 1)]
        [TestCase(true, 1)]
        public static void GetAllSpecialCaseDescriptionTest(bool useAdditionalItems,
            int resultLength)
        {
            var instance = new DBItemSettingDescLoadFile((0, ""));

            List<DatabaseValueCaseDescription> result = null;

            var errorOccured = false;
            try
            {
                result = instance.GetAllSpecialCaseDescription();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);

            // 取得した要素数が意図した値であること
            Assert.AreEqual(result.Count, 1);
        }

        [Test]
        public static void AddSpecialCaseTest()
        {
            var instance = new DBItemSettingDescLoadFile((0, ""));
            var specialCase = new DatabaseValueCase(0, "");

            var errorOccured = false;
            try
            {
                instance.AddSpecialCase(specialCase);
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
        public static void AddRangeSpecialCaseTest()
        {
            var instance = new DBItemSettingDescLoadFile((0, ""));
            var specialCases = new[]
            {
                new DatabaseValueCase(0, ""),
                new DatabaseValueCase(1, "a"),
                new DatabaseValueCase(2, "あいうえお"),
            };

            var errorOccured = false;
            try
            {
                instance.AddRangeSpecialCase(specialCases);
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
        public static void InsertSpecialCaseTest()
        {
            var instance = new DBItemSettingDescLoadFile((0, ""));
            var specialCase = new DatabaseValueCase(0, "");

            var errorOccured = false;
            try
            {
                instance.InsertSpecialCase(0, specialCase);
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
        public static void InsertRangeSpecialCaseTest()
        {
            var instance = new DBItemSettingDescLoadFile((0, ""));
            var specialCases = new[]
            {
                new DatabaseValueCase(0, ""),
                new DatabaseValueCase(1, "a"),
                new DatabaseValueCase(2, "あいうえお"),
            };

            var errorOccured = false;
            try
            {
                instance.InsertRangeSpecialCase(0, specialCases);
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
        public static void UpdateDatabaseSpecialCaseTest()
        {
            var instance = new DBItemSettingDescLoadFile((0, ""));

            var errorOccured = false;
            try
            {
                instance.UpdateDatabaseSpecialCase(0, "");
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
        public static void UpdateManualSpecialCaseTest()
        {
            var instance = new DBItemSettingDescLoadFile((0, ""));

            var errorOccured = false;
            try
            {
                instance.UpdateManualSpecialCase(0, new DatabaseValueCase(0, ""));
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
        public static void RemoveSpecialCaseAtTest()
        {
            var instance = new DBItemSettingDescLoadFile((0, ""));

            var errorOccured = false;
            try
            {
                instance.RemoveSpecialCaseAt(0);
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
        public static void ClearSpecialCaseTest()
        {
            var instance = new DBItemSettingDescLoadFile((0, ""));

            var errorOccured = false;
            try
            {
                instance.ClearSpecialCase();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生すること
            Assert.IsTrue(errorOccured);
        }

        [TestCase(null, true)]
        [TestCase("abc", false)]
        public static void UpdateDefaultFolderTest(string folderName, bool isError)
        {
            var instance = new DBItemSettingDescLoadFile((0, ""));
            var folderNameValue = folderName == null
                ? null
                : (DBSettingFolderName) folderName;

            var errorOccured = false;
            try
            {
                instance.UpdateDefaultFolder(folderNameValue);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);
        }

        [TestCase(true)]
        [TestCase(false)]
        public static void UpdateOmissionFolderNameFlagTest(bool isOmission)
        {
            var instance = new DBItemSettingDescLoadFile((0, ""));

            var errorOccured = false;
            try
            {
                instance.UpdateOmissionFolderNameFlag(isOmission);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);
        }

        private static readonly object[] CanSetItemTestCaseSource =
        {
            new object[] {null, true, false},
            new object[] {DBItemType.Int, false, false},
            new object[] {DBItemType.String, false, true},
        };

        [TestCaseSource(nameof(CanSetItemTestCaseSource))]
        public static void CanSetItemTypeTest(DBItemType type, bool isError, bool answer)
        {
            var instance = new DBItemSettingDescLoadFile();
            bool result = false;

            var errorOccured = false;
            try
            {
                result = instance.CanSetItemType(type);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーフラグが一致すること
            Assert.AreEqual(errorOccured, isError);

            if (errorOccured) return;

            // 結果が意図した値と一致すること
            Assert.AreEqual(result, answer);
        }

        private static readonly object[] EqualsTestCaseSource =
        {
            new object[] {null, false},
            new object[] {nameof(DBItemSettingDescNormal), false},
            new object[] {nameof(DBItemSettingDescDatabase), false},
            new object[] {nameof(DBItemSettingDescManual), false},
            new object[] {$"{nameof(DBItemSettingDescLoadFile)}_folder_{true}", true},
            new object[] {$"{nameof(DBItemSettingDescLoadFile)}_folder_{false}", false},
            new object[] {$"{nameof(DBItemSettingDescLoadFile)}_directory_{true}", false},
            new object[] {$"{nameof(DBItemSettingDescLoadFile)}_directory_{false}", false},
        };

        [TestCaseSource(nameof(EqualsTestCaseSource))]
        public static void Equals(string settingDescCode, bool answer)
        {
            var instance = new DBItemSettingDescLoadFile
            {
                FolderName = "folder",
                OmissionFolderNameFlag = true
            };

            IDBItemSettingDesc desc = null;
            switch (settingDescCode)
            {
                case nameof(DBItemSettingDescNormal):
                    desc = new DBItemSettingDescNormal();
                    break;
                case nameof(DBItemSettingDescDatabase):
                    desc = new DBItemSettingDescDatabase();
                    break;
                case nameof(DBItemSettingDescManual):
                    desc = new DBItemSettingDescManual();
                    break;
                case null:
                    break;
                default:
                    var settings = settingDescCode.Split('_');
                    var lfDesc = new DBItemSettingDescLoadFile();
                    lfDesc.FolderName = settings[1];
                    lfDesc.OmissionFolderNameFlag = bool.Parse(settings[2]);
                    desc = lfDesc;
                    break;
            }

            bool result = false;

            var errorOccured = false;
            try
            {
                result = instance.Equals(desc);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                errorOccured = true;
            }

            // エラーが発生しないこと
            Assert.IsFalse(errorOccured);

            // 結果が意図した値と一致すること
            Assert.AreEqual(result, answer);
        }

        [Test]
        public static void SerializeTest()
        {
            var target = new DBItemSettingDescLoadFile
            {
                FolderName = "FolderName"
            };
            var clone = DeepCloner.DeepClone(target);
            Assert.IsTrue(clone.Equals(target));
        }
    }
}