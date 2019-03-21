// ========================================
// Project Name : WodiLib
// File Name    : CommonEvent.cs
//
// MIT License Copyright(c) 2019 kameske
// see LICENSE file
// ========================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using WodiLib.Event;
using WodiLib.Event.EventCommand;
using WodiLib.Sys;
using WodiLib.Sys.Cmn;

namespace WodiLib.Common
{
    /// <summary>
    /// コモンイベントクラス
    /// </summary>
    public class CommonEvent
    {
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Public Constant
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        /// <summary>数値引数の数最大値</summary>
        public static readonly int NumberArgsLengthMax = 4;

        /// <summary>数値引数の数最小値</summary>
        public static readonly int NumberArgsLengthMin = 0;

        /// <summary>文字列引数の数最大値</summary>
        public static readonly int StrArgsLengthMax = 4;

        /// <summary>文字列引数の数最小値</summary>
        public static readonly int StrArgsLengthMin = 0;

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Internal Constant
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// ヘッダバイト
        /// </summary>
        internal static readonly byte[] HeaderBytes =
        {
            0x8E
        };

        /// <summary>
        /// 引数初期値の後のチェックディジット
        /// </summary>
        internal static readonly byte[] AfterInitValueBytes =
        {
            0x90
        };

        /// <summary>
        /// セルフ変数名の後のチェックディジット
        /// </summary>
        internal static readonly byte[] AfterMemoBytesSelfVariableNamesBytes =
        {
            0x91
        };

        /// <summary>
        /// 返戻値の意味の前のチェックディジット
        /// </summary>
        internal static readonly byte[] BeforeReturnValueSummaryBytesBefore =
        {
            0x92
        };

        /// <summary>
        /// コモンイベント末尾のチェックディジット（Ver2.00以前）
        /// </summary>
        internal static readonly byte[] FooterBytesBeforeVer2_00 =
        {
            0x91
        };

        /// <summary>
        /// コモンイベント末尾のチェックディジット（Ver2.00以前）
        /// </summary>
        internal static readonly byte[] FooterBytesAfterVer2_00 =
        {
            0x92
        };

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Public Property
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>コモンイベントID</summary>
        public int Id { get; set; }

        private CommonEventBootCondition condition = new CommonEventBootCondition();

        /// <summary>
        /// [NotNull] 起動条件
        /// </summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        public CommonEventBootCondition BootCondition
        {
            get => condition;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(CriteriaOperator)));
                condition = value;
            }
        }

        private int numberArgsLength = NumberArgsLengthMin;

        /// <summary>
        /// [Range(0, 4)] 数値引数の数
        /// </summary>
        /// <exception cref="PropertyOutOfRangeException">指定範囲以外の値がセットされた場合</exception>
        public int NumberArgsLength
        {
            get => numberArgsLength;
            set
            {
                if (value < NumberArgsLengthMin || NumberArgsLengthMax < value)
                    throw new PropertyOutOfRangeException(
                        ErrorMessage.OutOfRange(nameof(NumberArgsLength), NumberArgsLengthMin,
                            NumberArgsLengthMax, value));
                numberArgsLength = value;
            }
        }

        private int strArgsLength = StrArgsLengthMin;

        /// <summary>
        /// [Range(0, 4)] 文字列引数の数
        /// </summary>
        /// <exception cref="PropertyOutOfRangeException">指定範囲以外の値がセットされた場合</exception>
        public int StrArgsLength
        {
            get => strArgsLength;
            set
            {
                if (value < StrArgsLengthMin || StrArgsLengthMax < value)
                    throw new PropertyOutOfRangeException(
                        ErrorMessage.OutOfRange(nameof(StrArgsLength), StrArgsLengthMin,
                            StrArgsLengthMax, value));
                strArgsLength = value;
            }
        }

        private String name = "";

        /// <summary>
        /// [NotNull] コモンイベント名
        /// </summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        public String Name
        {
            get => name;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(Name)));
                name = value;
            }
        }

        private EventCommandList eventCommands = new EventCommandList(new[] {new Blank()});

        /// <summary>
        /// [NotNull] イベントコマンド
        /// </summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        public EventCommandList EventCommands
        {
            get => eventCommands;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(EventCommands)));
                eventCommands = value;
            }
        }

        private string description = "";

        /// <summary>
        /// [NotNull] 説明文
        /// </summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string Description
        {
            get => description;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(Description)));
                description = value;
            }
        }

        private string memo = "";

        /// <summary>
        /// [NotNull] メモ
        /// </summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        public string Memo
        {
            get => memo;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(Memo)));
                memo = value;
            }
        }

        private CommonEventLabelColor labelColor = CommonEventLabelColor.Black;

        /// <summary>ラベル色</summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        public CommonEventLabelColor LabelColor
        {
            get => labelColor;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(LabelColor)));
                labelColor = value;
            }
        }

        private string footerString = "";

        /// <summary>
        /// [NotNull] フッタ文字列
        /// </summary>
        /// <exception cref="PropertyNullException">nullをセットした場合</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string FooterString
        {
            get => footerString;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(FooterString)));
                footerString = value;
            }
        }

        /// <summary>返戻アドレス情報（Ver2.00～）</summary>
        private readonly CommonEventReturnValue returnValueInfo = new CommonEventReturnValue();

        /// <summary>
        /// [NotNull] 返戻値の意味（Ver2.00～）
        /// </summary>
        /// <exception cref="PropertyNullException">nullを設定した場合</exception>
        public string ReturnValueDescription
        {
            get => returnValueInfo.Description;
            set
            {
                if (value == null)
                    throw new PropertyNullException(
                        ErrorMessage.NotNull(nameof(ReturnValueDescription)));
                returnValueInfo.Description = value;
            }
        }

        /// <summary>
        /// 値を返すフラグ（Ver2.00～）
        /// </summary>
        public bool IsReturnValue => returnValueInfo.IsReturnValue;

        /// <summary>
        /// セルフ変数インデックス（値を返さない場合-1）（Ver2.00～）
        /// </summary>
        public int ReturnVariableIndex => returnValueInfo.ReturnVariableIndex;

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Private Property
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// 引数特殊指定情報リスト
        /// </summary>
        private CommonEventSpecialArgDescList CommonEventSpecialArgDescList { get; } =
            new CommonEventSpecialArgDescList();

        /// <summary>
        /// 変数名リスト
        /// </summary>
        private CommonEventSelfVariableNameList SelfVariableNameList { get; set; } =
            new CommonEventSelfVariableNameList();

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Public Method
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// 数値引数の情報を更新する。
        /// </summary>
        /// <param name="index">[Range(0, 4)] インデックス</param>
        /// <param name="desc">[NotNull] 情報</param>
        /// <exception cref="ArgumentOutOfRangeException">indexが指定範囲以外の場合</exception>
        /// <exception cref="ArgumentNullException">descがnullの場合</exception>
        public void UpdateSpecialNumberArgDesc(int index, CommonEventSpecialNumberArgDesc desc)
        {
            if (index < CommonEventSpecialArgDescList.NumArgListIndexMin ||
                CommonEventSpecialArgDescList.NumArgListIndexMax < index)
                throw new ArgumentOutOfRangeException(
                    ErrorMessage.OutOfRange(nameof(index), CommonEventSpecialArgDescList.NumArgListIndexMin,
                        CommonEventSpecialArgDescList.NumArgListIndexMax, index));
            if (desc == null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(desc)));

            CommonEventSpecialArgDescList.UpdateSpecialNumberArgDesc(index, desc);
        }

        /// <summary>
        /// 数値引数の情報を取得する。
        /// </summary>
        /// <param name="index">[Range(0, 4)] インデックス</param>
        /// <returns>情報インスタンス</returns>
        /// <exception cref="ArgumentOutOfRangeException">indexが指定範囲以外の場合</exception>
        public CommonEventSpecialNumberArgDesc GetSpecialNumberArgDesc(int index)
        {
            if (index < CommonEventSpecialArgDescList.NumArgListIndexMin ||
                CommonEventSpecialArgDescList.NumArgListIndexMax < index)
                throw new ArgumentOutOfRangeException(
                    ErrorMessage.OutOfRange(nameof(index), CommonEventSpecialArgDescList.NumArgListIndexMin,
                        CommonEventSpecialArgDescList.NumArgListIndexMax, index));
            return CommonEventSpecialArgDescList.GetSpecialNumberArgDesc(index);
        }

        /// <summary>
        /// 文字列引数の情報を更新する。
        /// </summary>
        /// <param name="index">[Range(0, 4)] インデックス</param>
        /// <param name="desc">[NotNull] 情報</param>
        /// <exception cref="ArgumentOutOfRangeException">indexが指定範囲以外の場合</exception>
        /// <exception cref="ArgumentNullException">descがnullの場合</exception>
        public void UpdateSpecialStringArgDesc(int index, CommonEventSpecialStringArgDesc desc)
        {
            if (index < CommonEventSpecialArgDescList.StrArgListIndexMin ||
                CommonEventSpecialArgDescList.StrArgListIndexMax < index)
                throw new ArgumentOutOfRangeException(
                    ErrorMessage.OutOfRange(nameof(index), CommonEventSpecialArgDescList.StrArgListIndexMin,
                        CommonEventSpecialArgDescList.StrArgListIndexMax, index));
            if (desc == null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(desc)));
            CommonEventSpecialArgDescList.UpdateSpecialStringArgDesc(index, desc);
        }

        /// <summary>
        /// 文字列引数の情報を取得する。
        /// </summary>
        /// <param name="index">[Range(0, 4)] インデックス</param>
        /// <returns>情報インスタンス</returns>
        /// <exception cref="ArgumentOutOfRangeException">indexが指定範囲以外の場合</exception>
        public CommonEventSpecialStringArgDesc GetSpecialStringArgDesc(int index)
        {
            if (index < CommonEventSpecialArgDescList.StrArgListIndexMin ||
                CommonEventSpecialArgDescList.StrArgListIndexMax < index)
                throw new ArgumentOutOfRangeException(
                    ErrorMessage.OutOfRange(nameof(index), CommonEventSpecialArgDescList.StrArgListIndexMin,
                        CommonEventSpecialArgDescList.StrArgListIndexMax, index));
            return CommonEventSpecialArgDescList.GetSpecialStringArgDesc(index);
        }

        /// <summary>
        /// 変数名を更新する。
        /// </summary>
        /// <param name="number">[Range(0, 99)] 変数番号</param>
        /// <param name="variableName">[NotNull] 変数名</param>
        /// <exception cref="ArgumentOutOfRangeException">numberが指定範囲以外の場合</exception>
        /// <exception cref="ArgumentNullException">variableNameがnullの場合</exception>
        public void UpdateVariableName(int number, string variableName)
        {
            var max = CommonEventSelfVariableNameList.ListMax;
            var min = CommonEventSelfVariableNameList.ListMin;
            if (number < min || max < number)
                throw new ArgumentOutOfRangeException(
                    ErrorMessage.OutOfRange(nameof(number), min, max, number));
            if (variableName == null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(variableName)));

            SelfVariableNameList.UpdateVariableName(number, variableName);
        }

        /// <summary>
        /// 変数名を取得する。
        /// </summary>
        /// <param name="number">[Range(0, 99)] 変数番号</param>
        /// <returns>変数名</returns>
        /// <exception cref="ArgumentOutOfRangeException">numberが指定範囲以外の場合</exception>
        public string GetVariableName(int number)
        {
            var max = CommonEventSelfVariableNameList.ListMax;
            var min = CommonEventSelfVariableNameList.ListMin;
            if (number < min || max < number)
                throw new ArgumentOutOfRangeException(
                    ErrorMessage.OutOfRange(nameof(number), min, max, number));

            return SelfVariableNameList.GetVariableName(number);
        }

        /// <summary>
        /// すべての変数名を更新する。
        /// </summary>
        /// <param name="variableNameList">[NotNull] 変数名リスト</param>
        /// <exception cref="ArgumentNullException">variableNameListがnullの場合</exception>
        /// <exception cref="ArgumentException">variableNameListの要素数が100以外の場合</exception>
        public void UpdateAllVariableName(IEnumerable<string> variableNameList)
        {
            var nameList = variableNameList.ToList();

            if (variableNameList == null)
                throw new ArgumentNullException(
                    ErrorMessage.NotNull(nameof(variableNameList)));

            var count = CommonEventSelfVariableNameList.ListMax;
            if (nameList.Count != count)
                throw new ArgumentException(
                    $"{nameof(variableNameList)}の要素数は{count}である必要があります。");

            SelfVariableNameList = new CommonEventSelfVariableNameList(nameList);
        }

        /// <summary>
        /// すべての変数名を取得する。
        /// </summary>
        /// <returns>変数名の集合</returns>
        public IEnumerable<string> GetAllVariableName()
        {
            return SelfVariableNameList.GetAllName();
        }

        /// <summary>
        /// 返戻セルフ変数インデックスをセットする。
        /// </summary>
        /// <param name="commonVarAddress">[Range(-1, 99)] 返戻アドレス</param>
        /// <exception cref="ArgumentOutOfRangeException">commonVarAddressが指定範囲外の場合</exception>
        public void SetReturnVariableIndex(int commonVarAddress)
        {
            try
            {
                returnValueInfo.SetReturnVariableIndex(commonVarAddress);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException(ex.Message);
            }
        }

        /// <summary>
        /// 返戻フラグをOffにする。
        /// </summary>
        public void SetReturnValueNone()
        {
            returnValueInfo.SetReturnValueNone();
        }

        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/
        //     Common
        // _/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/

        /// <summary>
        /// バイナリ変換する。
        /// </summary>
        /// <returns>バイナリデータ</returns>
        public byte[] ToBinary()
        {
            var result = new List<byte>();

            // ヘッダ
            result.AddRange(HeaderBytes);

            // コモンイベントID
            result.AddRange(Id.ToBytes(Endian.Woditor));

            // 起動条件
            result.AddRange(BootCondition.ToBinary());

            // 数値引数の数
            result.Add((byte) NumberArgsLength);

            // 文字列引数の数
            result.Add((byte) StrArgsLength);

            // コモンイベント名
            result.AddRange(new WoditorString(Name).StringByte);

            // イベントコマンド行数
            result.AddRange(EventCommands.Count.ToBytes(Endian.Woditor));

            // イベントコマンド
            foreach (var command in EventCommands.GetAll())
            {
                result.AddRange(command.ToBinary());
            }

            // メモの前の文字列
            result.AddRange(new WoditorString(Description).StringByte);

            // メモ
            result.AddRange(new WoditorString(Memo).StringByte);

            // 引数特殊指定
            result.AddRange(CommonEventSpecialArgDescList.ToBinary());

            // 引数初期値後のチェックディジット
            result.AddRange(AfterInitValueBytes);

            // コモンイベントラベル色
            result.AddRange(LabelColor.Code.ToBytes(Endian.Woditor));

            // 変数名
            result.AddRange(SelfVariableNameList.ToBinary());

            // 変数名後のチェックディジット
            result.AddRange(AfterMemoBytesSelfVariableNamesBytes);

            // フッタ文字列
            result.AddRange(new WoditorString(FooterString).StringByte);

            // コモンイベント末尾
            if (VersionConfig.IsUnderVersion(WoditorVersion.Ver2_00))
            {
                // ver2.00 未満の場合はここまで
                result.AddRange(FooterBytesBeforeVer2_00);
                return result.ToArray();
            }

            // 返戻値前のチェックディジット
            result.AddRange(BeforeReturnValueSummaryBytesBefore);

            // 返戻値
            result.AddRange(returnValueInfo.ToBinary());

            // フッタ
            result.AddRange(FooterBytesAfterVer2_00);

            return result.ToArray();
        }
    }
}