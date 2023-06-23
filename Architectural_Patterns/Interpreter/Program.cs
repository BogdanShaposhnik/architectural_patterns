using System;
using System.Collections.Generic;

namespace InterpreterPattern
{
    // Abstract Expression: Defines the interface for interpreting expressions
    public abstract class Expression
    {
        public abstract bool Interpret(string context);
    }

    // Terminal Expression: Represents a terminal expression
    public class TerminalExpression : Expression
    {
        private readonly string expression;

        public TerminalExpression(string expression)
        {
            this.expression = expression;
        }

        public override bool Interpret(string context)
        {
            return context.Contains(expression);
        }
    }

    // Non-terminal Expression: Represents a non-terminal expression
    public class AndExpression : Expression
    {
        private readonly Expression expression1;
        private readonly Expression expression2;

        public AndExpression(Expression expression1, Expression expression2)
        {
            this.expression1 = expression1;
            this.expression2 = expression2;
        }

        public override bool Interpret(string context)
        {
            return expression1.Interpret(context) && expression2.Interpret(context);
        }
    }

    // Non-terminal Expression: Represents a non-terminal expression
    public class OrExpression : Expression
    {
        private readonly Expression expression1;
        private readonly Expression expression2;

        public OrExpression(Expression expression1, Expression expression2)
        {
            this.expression1 = expression1;
            this.expression2 = expression2;
        }

        public override bool Interpret(string context)
        {
            return expression1.Interpret(context) || expression2.Interpret(context);
        }
    }

    // Client: Builds the expression tree and interprets the expressions
    public class Client
    {
        private Expression expression;

        public Client()
        {
            // Build the expression tree
            Expression terminal1 = new TerminalExpression("Hello");
            Expression terminal2 = new TerminalExpression("World");
            expression = new OrExpression(terminal1, terminal2);
        }

        public bool Interpret(string context)
        {
            // Interpret the expression with the given context
            return expression.Interpret(context);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create the client
            var client = new Client();

            // Test the interpretation
            Console.WriteLine(client.Interpret("Hello"));          // True
            Console.WriteLine(client.Interpret("World"));          // True
            Console.WriteLine(client.Interpret("Hello World"));    // True
            Console.WriteLine(client.Interpret("Hi"));             // False

            Console.ReadKey();
        }
    }
}
