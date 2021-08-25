using ConsoleApp50;
using Xunit;

namespace ConsoleApp50Tests
{
    public class AccessibilityTests
    {
        [Fact]
        public void Test1()
        {
            var sut = new DemoInternal();

            var act1 = sut.GetFromPublic();
            var act2 = sut.GetFromInternal();
            var act3 = sut.GetFromProtectedInternal();


            Assert.NotEmpty(act1);
            Assert.NotEmpty(act2);
            Assert.NotEmpty(act3);
        }


    }
}