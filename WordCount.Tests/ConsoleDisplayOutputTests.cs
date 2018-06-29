using Autofac;
using Moq;
using WordCount.Abstractions.Console;
using WordCount.Implementations;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests
{
    public class ConsoleDisplayOutputTests
    {
        private readonly Mock<IConsole> _mockConsole;

        private readonly ConsoleDisplayOutput _systemUnderTest;

        public ConsoleDisplayOutputTests()
        {
            _mockConsole = new Mock<IConsole>();

            var containerBuilder = new ContainerBuilder();
            containerBuilder
                .RegisterInstance(instance: _mockConsole.Object)
                .As<IConsole>()
                .SingleInstance();

            containerBuilder
                .RegisterType<ConsoleDisplayOutput>()
                .SingleInstance();

            _systemUnderTest = containerBuilder
                .Build()
                .Resolve<ConsoleDisplayOutput>();
        }

        [NamedFact]
        public void ConsoleDisplayOutputTests_Write_A()
        {
            _systemUnderTest.Write(text: "A");

            _mockConsole
                .Verify(expression: v => v.Write("A"), times: Times.Once);
        }

        [NamedFact]
        public void ConsoleDisplayOutputTests_WriteLine_A()
        {
            _systemUnderTest.WriteLine(text: "A");

            _mockConsole
                .Verify(expression: v => v.WriteLine("A"), times: Times.Once);
        }

        [NamedFact]
        public void ConsoleDisplayOutputTests_WriteErrorLine_FEHLER_Color_Red()
        {
            _systemUnderTest.WriteErrorLine("FEHLER");

            _mockConsole
                .VerifySet(v1 => v1.ForegroundColor = System.ConsoleColor.Red, Times.Once);

            _mockConsole
                .Verify(v2 => v2.WriteLine("FEHLER"), Times.Once);

            _mockConsole
                .Verify(v3 => v3.ResetColor(), Times.Once);
        }
    }
}
