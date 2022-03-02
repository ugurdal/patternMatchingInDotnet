using PatternMatching;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(true, "true")]
        [InlineData(false, "false")]
        public void BooleanShouldReturnSameString(bool val, string result)
        {
            //Arrange
            var test = new SwitchStatements();

            //Act
            var sut = test.Boolean(val);

            //Assert
            Assert.Equal(sut, result);
        }

        [Theory]
        [InlineData("Ugur", true, "Hello Ugur")]
        [InlineData("Ugur", false, "Hi")]
        [InlineData("Buket", true, "Hi Buket")]
        [InlineData("Buket", false, "Hi")]
        public void GreeterTest(string name, bool greetWithName, string result)
        {
            //Arrange
            var test = new SwitchStatements();
            
            //Act
            var sut = test.Greeter(name, greetWithName);

            //Assert
            Assert.Equal(sut, result);
        }
    }
}