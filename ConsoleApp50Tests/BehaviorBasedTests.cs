using System;
using System.Threading.Tasks;
using ConsoleApp50.BehaviorBased;
using ConsoleApp50.Models;
using Moq;
using Xunit;
using Xunit.Sdk;

/*
 * Moq �� Verify* ���\�b�h���g���� Behavior test �̂��ꂱ��B
 * �O��m���Ƃ��āA
 * - sut �̃e�X�g: state-based testing (interaction-based testing
 * - Mock �̐U�镑�����e�X�g: behavior-based testing
 *
 * GitHub: Moq4: https://github.com/moq/moq4/wiki
 * �h�L�������g�͂����Ȃ̂���: https://documentation.help/Moq/
 *
 */

namespace ConsoleApp50Tests
{
    public class BehaviorBasedTests
    {

        /// <summary>
        /// mock object �� Verify ���\�b�h�̊�b�T���v��
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

            // // Custom message ��������
            mockRepo.Verify(x => x.UpdateAsync(It.IsNotNull<Person>()), "Custom error message!");
            // �����Ȃɂ������ă��\�b�h���Ă΂ꂽ���Ƃ��m�F
            mockRepo.Verify(x => x.UpdateAsync(It.IsAny<Person>()));
            // mock �̃��\�b�h���Ă΂ꂽ���A�����̓���̒l���`�F�b�N
            mockRepo.Verify(x => x.UpdateAsync(It.Is<Person>(p => p.State == stateToExpected)));


        }

        /// <summary>
        /// mock object �� ���\�b�h���Ă񂾂��Ɋւ���f��
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

            // 1�x�� call ����Ă��Ȃ����Ƃ��m�F
            mockRepo.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Never);
            // 1�x���� call ���ꂽ���Ƃ��m�F�B
            mockRepo.Verify(x => x.UpdateAsync(It.IsAny<Person>()), Times.Once);
            // ����̉񐔌Ă΂��m�F�Ȃ� Times.Exactly
            // Times.** �ŐF��ȃp�^�[����ݒ�\�Ȃ̂� If  repo �̃��\�b�h���Ăԃp�^�[��������Ƃ��Ɏg��
            mockRepo.Verify(x => x.UpdateAsync(It.IsAny<Person>()), Times.Exactly(1));
        }


        /// <summary>
        /// mock object �� property �A�N�Z�X�Ɋւ���f��
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

            // repo �� Property "SampleProperty" ����x���� get �������B�񐔂̎w����Ȃ����Ɖ񐔂� validate�͊֌W�Ȃ��Ȃ�
            mockRepo.VerifyGet(x => x.SampleProperty, Times.Once);
            // repo �� Property "SampleProperty" �ɓ���̒l���Z�b�g������
            mockRepo.VerifySet(x => x.SampleProperty = stateToExpected);
            // repo �� Property "SampleProperty" �ɂȂɂ����Z�b�g�������ł悯��� It.IsAny<T> �ŁB
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
