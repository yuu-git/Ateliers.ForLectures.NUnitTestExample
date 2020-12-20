using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Ateliers.ForLectures.NUnitTestExample.Models;
using NUnit.Framework;

namespace Ateliers.ForLectures.NUnitTestExample.Tests.Models
{
    // テストクラスの名称は 「テスト対象のクラス名+Test」と命名すると、テストが実装されているか追いやすい。
    // これはプロジェクトによって異なるため、確認する。
    public class ProductItemTest
    {
        #region --- 引数が「Null不可能な数字」の場合に関する主なテスト観点まとめ ---
        /*
         * 1. プラスの値を入れたらどうなるのか (1, 2, ....10 etc.)
         * 2. マイナスの値を入れたらどうなるのか (-1, -2, ...-10 etc.)
         * 3. ゼロの値を入れたらどうなるのか
         * 4. プログラム上の最大値を入れたらどうなるのか　例：int.MaxValue
         * 5. プログラム上の最小値を入れたらどうなるのか　例：int.MinValue
         */
        #endregion

        // テストとして認識させる場合はこのように [Test] と入れる　※ [TestCase] は後ほど説明
        [Test]
        public void Calculation_引数が1の場合は1が返る()
        {
            // メソッドテストの基本的な書き方についてコメント

            // 1. テスト対象のクラスを生成する。
            var testModel = new ProductItem();

            // 2. テストに使う値を決める。
            var testValue = 1;

            // 3. メソッドを動かした結果、返ってくると想定する値を決める。
            var expected = 1;

            // 4. 実際にメソッドを動かしてみる。
            var result = testModel.Calculation(testValue);

            // 4.1. もし途中の結果が気になるならば、ログやコンソールに記録しても良い。
            Console.WriteLine(result);

            // 5. 返ってきた結果が正しいかを確認する。　Assert クラスを使う。関数はいろいろある。
            //    ここで返ってきた値と想定結果が異なると、テスト結果はNGで終了する。
            Assert.AreEqual(expected, result);

            // 5は3を省略して、このようにメソッドに書いても良い
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Calculation_引数がマイナス1の場合はマイナス1が返る()
        {
            // 上のテスト 『Calculation_1の場合は1が返る』 を可能な限り省略してみると、こうなる。
            // 見た目はスッキリするが、途中結果がわからないため、デバッグはやりにくくなるので注意。

            Assert.AreEqual(-1, new ProductItem().Calculation(-1));
        }

        [Test]
        public void Calculation_引数が3の場合は27が返る()
        {
            var testModel = new ProductItem();

            var testValue = 3;
            var expected = 27;

            var result = testModel.Calculation(testValue);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Calculation_引数がマイナス2の場合はマイナス8が返る()
        {
            var testModel = new ProductItem();

            var testValue = -2;
            var expected = -8;

            var result = testModel.Calculation(testValue);

            Assert.AreEqual(expected, result);
        }

        // 例外エラーが想定通りに発生するか確認するテスト
        [Test]
        public void Calculation_引数が0の場合は例外エラー()
        {
            var testModel = new ProductItem();

            var testValue = 0;

            // 今回は例外エラーメッセージが正しいかを確認したいため、元のクラスから取得する。
            var expectedErrMsg = testModel.CalculationErrMsg;

            // Assert.Throws が例外エラーが発生しても中断せず、発生したを拾うメソッドとなっている。 
            // 変数 exception に発生した例外エラーが入る。
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                // ここにエラーさせる手順を書く。
                // もしエラーが発生しなかったり、ArgumentOutOfRangeExceptionと異なるエラーが返ってきた場合
                // テストは結果NGとして終了する。
                testModel.Calculation(testValue);
            });

            // 例外エラーを確認する。
            // この場合 「exception.Message に expectedErrMsg の文字列が"含まれて"いるか」を確認している。
            Assert.IsTrue(exception.Message.Contains(expectedErrMsg));
        }

        [Test]
        public void Calculation_引数が最大値で計算できる()
        {
            // 正常終了するが、実装がダメなケース。
            // intの最大値は 2147483647 であるが、そこに3乗しているため、実際は 2147483647 * 2147483647 * 2147483647 になる。
            // しかし int にはそんな膨大な数値は入らないにもかかわらず、メソッドは正常終了している。
            // この場合は、本来であれば以下のような対応が必要。
            // -----
            // 1. 計算結果が int.MaxValue を超える場合、メソッドが例外エラーを出す
            // 2. メソッドに「int.MaxValue を超える場合 int.MaxValue の値を返す」と明記する
            // -----
            // 「そもそも、そんな膨大な数値は扱わない」と割り切ることが普通であるが、実装者が気が付かないケースもあるため
            // テストを作成するときは気を付けると、バグが減る。
            // 絶対にありえない、と割り切る場合は、特にテストしなくても良い。（プロジェクトの責任者に確認）
            var testModel = new ProductItem();

            int result = testModel.Calculation(int.MaxValue);

            // ↓そもそもこれがビルドエラーで書けない（明らかなオーバーフロー）
            //Assert.AreEqual(int.MaxValue * int.MaxValue * int.MaxValue, result);
        }

        [Test]
        public void Calculation_引数が最小値で計算するとオーバーフロー()
        {
            // Calculation_最大値で計算できる と同じ現象。ただしこちらは例外が出るかを確認している。
            // 結果、オーバーフローするにもかかわらず例外エラーとならないため、テスト結果NGになる。

            var testModel = new ProductItem();

            var exception = Assert.Throws<OverflowException>(() => testModel.Calculation(int.MinValue));
        }

        // TestCase() と書くと、テストメソッドに引数を与えることができる。
        // この場合、4パターンの値を与えて、4ケースのテストを実施する。
        [TestCase(1, 1)]
        [TestCase(2, 8)]
        [TestCase(3, 27)]
        [TestCase(10, 1000)]
        public void Calculation_計算結果が正しい(int testValue, int expected)
        {
            var testModel = new ProductItem();

            // testValue には TestCase(x, y) の x値が使用される
            var result = testModel.Calculation(testValue);

            // expected には TestCase(x, y) の y値が使用される
            Assert.AreEqual(expected, result);
        }


        #region --- 引数が「文字列」の場合に関する主なテスト観点のまとめ ---
        /*
         * 1. 1文字を入れたらどうなるのか (2バイト文字「A」, 4バイト文字「あ」,  半角数値「1」や全角「１」, etc...)
         * 2. 複数文字を入れたらどうなるのか (「AAA」,「あああ」,「111」, 「２２２」etc...)
         * 3. 空白を入れたらどうなるのか（string.Empty(""と同意義), 半角空白「" "」, 全角空白「"　"」, etc...  ）
         * 4. null を入れたらどうなるのか　※[NotNull]Attributeが付いていない場合に限る
         * 5. 文字と空白の複合ケースはどうなるのか（「A B」, 「あ　あ」「１　1 」, etc...  ）
         * 6. 記号は問題ないか（「*」, 「[」, 「*」, 「＊」, etc...  ）
         */
        #endregion


        // 以下、基本的に Calculation のテストと同じ。上記のテスト観点が違うのみ。


        [TestCase("A", "AAA")]
        [TestCase("あ", "あああ")]
        [TestCase("1", "111")]
        [TestCase("BB", "BBBBBB")]
        [TestCase("いい", "いいいいいい")]
        [TestCase("１１", "１１１１１１")]
        [TestCase("*", "***")]
        [TestCase("＃＋＋", "＃＋＋＃＋＋＃＋＋")]
        [TestCase(@"c:\", @"c:\c:\c:\")]
        [TestCase("a a", "a aa aa a")]
        [TestCase(" X", " X X X")]
        [TestCase("Y ", "Y Y Y ")]
        [TestCase(" Z ", " Z  Z  Z ")]
        public void String3Join_文字が正しく結合される(string testStr, string expected)
        {
            var testModel = new ProductItem();

            var result = testModel.String3Join(testStr);

            Assert.AreEqual(expected, result);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("　")]
        [TestCase("   ")]
        [TestCase(" 　 ")]
        public void String3Join_引数が空白文字の場合は例外エラー(string testStr)
        {
            // 複数のテストでチェック内容が同じ場合は、このようにプライベートメソッドに切り出しても良い。
            String3Join_例外チェック(testStr);
        }

        [Test]
        public void String3Join_引数がemptyやnullの場合は例外エラー()
        {
            // リストを作ってループさせてテストする。
            foreach (var str in new[] { string.Empty, null })
            {
                String3Join_例外チェック(str);
            }
        }

        private void String3Join_例外チェック(string testStr)
        {
            var testModel = new ProductItem();

            var exception = Assert.Throws<ArgumentNullException>(() => testModel.String3Join(testStr));

            Assert.IsTrue(exception.Message.Contains(testModel.String3JoinErrMsg));
        }
    }
}
