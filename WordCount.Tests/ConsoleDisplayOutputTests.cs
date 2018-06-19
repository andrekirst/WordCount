using Autofac;
using Moq;
using WordCount.Abstractions.Console;
using WordCount.Implementations;
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

        [Fact]
        public void ConsoleDisplayOutputTests_Write_A()
        {
            _systemUnderTest.Write(text: "A");

            _mockConsole
                .Verify(expression: v => v.Write("A"), times: Times.Once);
        }

        [Fact]
        public void ConsoleDisplayOutputTests_WriteLine_A()
        {
            _systemUnderTest.WriteLine(text: "A");

            _mockConsole
                .Verify(expression: v => v.WriteLine("A"), times: Times.Once);
        }
    }
}
