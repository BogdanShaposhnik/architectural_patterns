using InterpreterPattern;
using Xunit;

namespace InterpreterPattern.Tests
{
    public class ExpressionTests
    {
        [Theory]
        [InlineData("Hello", "Hello", true)]
        [InlineData("Hello", "World", false)]
        [InlineData("Hello World", "Hello", true)]
        [InlineData("Hello World", "World", true)]
        [InlineData("Hi", "Hi", true)]
        public void TerminalExpression_Interpret_ReturnsExpectedResult(string context, string expression, bool expectedResult)
        {
            // Arrange
            var terminalExpression = new TerminalExpression(expression);

            // Act
            bool result = terminalExpression.Interpret(context);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, false)]
        [InlineData(false, true, false)]
        [InlineData(false, false, false)]
        public void AndExpression_Interpret_ReturnsExpectedResult(bool expression1Result, bool expression2Result, bool expectedResult)
        {
            // Arrange
            var expression1 = new MockExpression(expression1Result);
            var expression2 = new MockExpression(expression2Result);
            var andExpression = new AndExpression(expression1, expression2);

            // Act
            bool result = andExpression.Interpret("");

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, true)]
        [InlineData(false, true, true)]
        [InlineData(false, false, false)]
        public void OrExpression_Interpret_ReturnsExpectedResult(bool expression1Result, bool expression2Result, bool expectedResult)
        {
            // Arrange
            var expression1 = new MockExpression(expression1Result);
            var expression2 = new MockExpression(expression2Result);
            var orExpression = new OrExpression(expression1, expression2);

            // Act
            bool result = orExpression.Interpret("");

            // Assert
            Assert.Equal(expectedResult, result);
        }

        private class MockExpression : Expression
        {
            private readonly bool result;

            public MockExpression(bool result)
            {
                this.result = result;
            }

            public override bool Interpret(string context)
            {
                return result;
            }
        }
    }
}
