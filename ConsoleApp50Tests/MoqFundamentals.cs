using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp50.BehaviorBased;
using ConsoleApp50.Models;
using Moq;
using Xunit;

namespace ConsoleApp50Tests
{
    public class MoqFundamentals
    {


        [Fact]
        public void ReadableSetupDemo()
        {
            var personToExpected = GetSamplePerson();

            // Arrange
            //var mockRepo = new Mock<IPersonRepository>();
            //mockRepo.Setup(x => x.ExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            //mockRepo.Setup(x => x.UpdateAsync(It.IsAny<Person>())).ReturnsAsync(personToExpected);

            //var mockRepoObj = mockRepo.Object;

            // 上の Arrange は以下のように書ける
            var mockRepoObj = Mock.Of<IPersonRepository>(
                repo =>
                    repo.ExistsAsync(It.IsAny<string>()) == Task.FromResult(true) 
                 && repo.UpdateAsync(It.IsAny<Person>()) == Task.FromResult(personToExpected)
                 );

            Assert.True(true);
        }



        /// <summary>
        /// mock object の特定のメソッドにアクセスするごとに別の値を返す
        /// </summary>
        [Fact]
        public async Task SetupSequenceDemo()
        {
            // Arrange
            var mockRepo = new Mock<IPersonRepository>();
            // 1回目は false, 2回目は true を返すパターン
            mockRepo.SetupSequence(x => x.ExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(false)
                .ReturnsAsync(true);

            var sut = new ServiceForBehavior(mockRepo.Object);

            // Act & Assert
            var firstResponse = await sut.ExistsPersonAsync("1");
            Assert.False(firstResponse);

            var secondResponse = await sut.ExistsPersonAsync("1");
            Assert.True(secondResponse);
        }







        /// <summary>
        /// mock object のメソッドで Exception を投げる例
        /// </summary>
        [Fact]
        public async Task ExceptionSampleTest()
        {
            // Arrange
            const string stateToExpected = "state 2";
            var personToInput = GetSamplePerson();

            var mockRepo = new Mock<IPersonRepository>();
            // Generics で exception 発生を定義
            mockRepo.Setup(x => x.UpdateAsync(It.IsAny<Person>()))
                .Throws<Exception>();
            // Generics で non-exception 発生を定義
            mockRepo.Setup(x => x.GetAsync(It.IsAny<string>()))
                .Throws(new Exception("custom message"));

            var sut = new ServiceForBehavior(mockRepo.Object);

            // Act & Assert

            // Assert
            await Assert.ThrowsAsync<Exception>(() => sut.UpdatePersonAsync(personToInput, stateToExpected));
            // どっちがいいんだろ????
            await Assert.ThrowsAsync<Exception>(async () => await sut.UpdatePersonAsync(personToInput, stateToExpected));
        }

        private static Person GetSamplePerson() => new Person
        {
            Id = "1",
            FullName = "Jack",
            Age = 18
        };
    }
}
