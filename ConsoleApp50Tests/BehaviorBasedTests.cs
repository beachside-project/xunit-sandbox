using System;
using System.Threading.Tasks;
using ConsoleApp50.BehaviorBased;
using ConsoleApp50.Models;
using Moq;
using Xunit;
using Xunit.Sdk;

/*
 * Moq の Verify* メソッドを使った Behavior test のあれこれ。
 * 前提知識として、
 * - sut のテスト: state-based testing (interaction-based testing
 * - Mock の振る舞いをテスト: behavior-based testing
 *
 * GitHub: Moq4: https://github.com/moq/moq4/wiki
 * ドキュメントはここなのかな: https://documentation.help/Moq/
 *
 */

namespace ConsoleApp50Tests
{
    public class BehaviorBasedTests
    {

        /// <summary>
        /// mock object の Verify メソッドの基礎サンプル
        /// </summary>
        [Fact]
        public async Task FundamentalsOfBehaviorBasedTesting()
        {
            // Arrange
            var personToInput = GetSamplePerson();

            var mockRepo = new Mock<IPersonRepository>();


            var sut = new ServiceForBehavior(mockRepo.Object);
            var stateToExpected = "state 2";

            // Act
            var actual = await sut.UpdatePersonAsync(personToInput, stateToExpected);

            // Assert

            // // Custom message をだせる
            mockRepo.Verify(x => x.UpdateAsync(It.IsNotNull<Person>()), "Custom error message!");
            // 引数なにが入ってメソッドが呼ばれたことを確認
            mockRepo.Verify(x => x.UpdateAsync(It.IsAny<Person>()));
            // mock のメソッドが呼ばれたかつ、引数の特定の値をチェック
            mockRepo.Verify(x => x.UpdateAsync(It.Is<Person>(p => p.State == stateToExpected)));


        }

        /// <summary>
        /// mock object の メソッドを呼んだかに関するデモ
        /// </summary>
        [Fact]
        public async Task MethodCalledTest()
        {
            // Arrange
            const string stateToExpected = "state 2";
            var personToInput = GetSamplePerson();

            var mockRepo = new Mock<IPersonRepository>();

            var sut = new ServiceForBehavior(mockRepo.Object);

            // Act
            var actual = await sut.UpdatePersonAsync(personToInput, stateToExpected);

            // Assert

            // 1度も call されていないことを確認
            mockRepo.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Never);
            // 1度だけ call されたことを確認。
            mockRepo.Verify(x => x.UpdateAsync(It.IsAny<Person>()), Times.Once);
            // 特定の回数呼ばれる確認なら Times.Exactly
            // Times.** で色んなパターンを設定可能なので If  repo のメソッドを呼ぶパターンがあるときに使う
            mockRepo.Verify(x => x.UpdateAsync(It.IsAny<Person>()), Times.Exactly(1));
        }


        /// <summary>
        /// mock object の property アクセスに関するデモ
        /// </summary>
        [Fact]
        public async Task PropertyAccessTest()
        {
            // Arrange
            const string stateToExpected = "state 2";
            var personToInput = GetSamplePerson();


            var mockRepo = new Mock<IPersonRepository>();

            var sut = new ServiceForBehavior(mockRepo.Object);

            // Act
            var actual = await sut.UpdatePersonAsync(personToInput, stateToExpected);

            // Assert

            // repo の Property "SampleProperty" を一度だけ get したか。回数の指定をなくすと回数の validateは関係なくなる
            mockRepo.VerifyGet(x => x.SampleProperty, Times.Once);
            // repo の Property "SampleProperty" に特定の値をセットしたか
            mockRepo.VerifySet(x => x.SampleProperty = stateToExpected);
            // repo の Property "SampleProperty" になにかをセットしたかでよければ It.IsAny<T> で。
            mockRepo.VerifySet(x => x.SampleProperty = It.IsAny<string>());


        }


        private static Person GetSamplePerson() => new Person
        {
            Id = "1",
            FullName = "Jack",
            Age = 18
        };
    }
}
