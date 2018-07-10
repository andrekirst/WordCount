using Autofac;
using Moq;
using WordCount.Abstractions.Console;
using WordCount.Implementations;
using WordCount.Interfaces;
using WordCount.Tests.XUnitHelpers;

namespace WordCount.Tests
{
    public class ConsoleDisplayOutputTests
    {
        private readonly Mock<IConsole> _mockConsole;
        private readonly Mock<ILanguageResource> _mockLanguageResource;

        private readonly ConsoleDisplayOutput _systemUnderTest;

        public ConsoleDisplayOutputTests()
        {
            _mockConsole = new Mock<IConsole>();
            _mockLanguageResource = new Mock<ILanguageResource>();

            var containerBuilder = new ContainerBuilder();
            containerBuilder
                .RegisterInstance(instance: _mockConsole.Object)
                .As<IConsole>();

            containerBuilder
                .RegisterInstance(instance: _mockLanguageResource.Object)
                .As<ILanguageResource>();

            containerBuilder
                .RegisterType<ConsoleDisplayOutput>();

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

        [NamedFact]
        public void ConsoleDisplayOutputTests_WriteResourceStringWithValues()
        {
            _mockLanguageResource
                .Setup(m => m.GetResourceStringById("IDENT"))
                .Returns("Wert: {0}");

            _systemUnderTest.WriteResourceStringWithValues("IDENT", "value1");

            _mockConsole
                .Verify(v2 => v2.Write("Wert: {0}", "value1"), Times.Once);
        }

        [NamedFact]
        public void ConsoleDisplayOutputTests_WriteResourceStringWithValuesLine()
        {
            _mockLanguageResource
                .Setup(m => m.GetResourceStringById("IDENT"))
                .Returns("Wert: {0}");

            _systemUnderTest.WriteResourceStringWithValuesLine("IDENT", "value1");

            _mockConsole
                .Verify(v2 => v2.WriteLine("Wert: {0}", "value1"), Times.Once);
        }

        [NamedFact]
        public void ConsoleDisplayOutputTests_WriteErrorResourceStringWithValuesLine()
        {
            _mockLanguageResource
                .Setup(m => m.GetResourceStringById("IDENT"))
                .Returns("Wert: {0}");

            _systemUnderTest.WriteErrorResourceStringWithValuesLine("IDENT", "value1");

            _mockConsole
                .VerifySet(v => v.ForegroundColor = System.ConsoleColor.Red, Times.Once);

            _mockConsole
                .Verify(v2 => v2.WriteLine("Wert: {0}", "value1"), Times.Once);

            _mockConsole
                .Verify(v => v.ResetColor(), Times.Once);
        }

        [NamedFact]
        public void ConsoleDisplayOutputTests_WriteLine_without_text_parameter()
        {
            _systemUnderTest.WriteLine();

            _mockConsole
                .Verify(v => v.WriteLine(), Times.Once);
        }
    }
}
