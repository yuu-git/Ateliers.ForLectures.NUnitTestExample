using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ateliers.ForLectures.NUnitTestExample.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductItem
    {
        /// <summary>
        /// <see cref="Calculation"/> で発生するエラーメッセージを取得します。
        /// </summary>
        public string CalculationErrMsg => "乗数に 0 を指定することはできません。";

        /// <summary>
        /// <see cref="StringJoin"/> で発生するエラーメッセージを取得します。
        /// </summary>
        public string String3JoinErrMsg => "結合する文字を null または空白にすることはできません。";

        /// <summary>
        /// 引数 i を3乗して返します。
        /// </summary>
        /// <param name="i"> 乗数の値を指定します。 </param>
        /// <returns> 計算した結果の値を返します。 </returns>
        /// <exception cref="ArgumentOutOfRangeException"> 乗数に 0 を指定することはできません。 </exception>
        public int Calculation(int i)
        {
            if (i == 0)
                throw new ArgumentOutOfRangeException(nameof(i), CalculationErrMsg);

            return i * i * i;
        }
        /// <summary>
        /// 引数 str の文字を3回繰り返して結合した文字列を返します。
        /// </summary>
        /// <param name="str"> 結合する文字列を指定します。 </param>
        /// <returns> 結合した文字列を返します。 </returns>
        /// <exception cref="ArgumentNullException"> 結合する文字列を null または空白に指定することはできません。 </exception>
        public string String3Join(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentNullException(nameof(str), String3JoinErrMsg);

            return str + str + str;
        }
    }
}
