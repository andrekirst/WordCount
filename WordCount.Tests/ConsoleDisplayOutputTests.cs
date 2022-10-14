using AutoFixture.Xunit2;
using Moq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Implementations.Output;
using WordCount.Interfaces.Language;
using Xunit;

namespace WordCount.Tests;

public class ConsoleDisplayOutputTests
{
    [Theory, AutoMoqData]
    public void ConsoleDisplayOutputTests_Write_A(
        [Frozen] Mock<IConsole> console,
        ConsoleDisplayOutput sut)
    {
        sut.Write("A");

        console.Verify(v => v.Write("A"), Times.Once);
    }

    [Theory, AutoMoqData]
    public void ConsoleDisplayOutputTests_WriteLine_A(
        [Frozen] Mock<IConsole> console,
        ConsoleDisplayOutput sut)
    {
        sut.WriteLine("A");

        console.Verify(v => v.WriteLine("A"), Times.Once);
    }

    [Theory, AutoMoqData]
    public void ConsoleDisplayOutputTests_WriteErrorLine_FEHLER_Color_Red(
        [Frozen] Mock<IConsole> console,
        ConsoleDisplayOutput sut)
    {
        sut.WriteErrorLine("FEHLER");

        console.VerifySet(v1 => v1.ForegroundColor = System.ConsoleColor.Red, Times.Once);

        console.Verify(v2 => v2.WriteLine("FEHLER"), Times.Once);

        console.Verify(v3 => v3.ResetColor(), Times.Once);
    }

    [Theory, AutoMoqData]
    public void ConsoleDisplayOutputTests_WriteResourceStringWithValues(
        [Frozen] Mock<ILanguageResource> languageResource,
        [Frozen] Mock<IConsole> console,
        ConsoleDisplayOutput sut)
    {
        languageResource
            .Setup(m => m.GetResourceStringById("IDENT"))
            .Returns("Wert: {0}");

        sut.WriteResource("IDENT", "value1");

        console.Verify(v2 => v2.Write("Wert: {0}", "value1"), Times.Once);
    }

    [Theory, AutoMoqData]
    public void ConsoleDisplayOutputTests_WriteResourceStringWithValuesLine(
        [Frozen] Mock<ILanguageResource> languageResource,
        [Frozen] Mock<IConsole> console,
        ConsoleDisplayOutput sut)
    {
        languageResource
            .Setup(m => m.GetResourceStringById("IDENT"))
            .Returns("Wert: {0}");

        sut.WriteResourceLine("IDENT", "value1");

        console.Verify(v2 => v2.WriteLine("Wert: {0}", "value1"), Times.Once);
    }

    [Theory, AutoMoqData]
    public void ConsoleDisplayOutputTests_WriteErrorResourceStringWithValuesLine(
        [Frozen] Mock<ILanguageResource> languageResource,
        [Frozen] Mock<IConsole> console,
        ConsoleDisplayOutput sut)
    {
        languageResource
            .Setup(m => m.GetResourceStringById("IDENT"))
            .Returns("Wert: {0}");

        sut.WriteErrorResourceLine("IDENT", "value1");

        console.VerifySet(v => v.ForegroundColor = System.ConsoleColor.Red, Times.Once);

        console.Verify(v2 => v2.WriteLine("Wert: {0}", "value1"), Times.Once);

        console.Verify(v => v.ResetColor(), Times.Once);
    }

    [Theory, AutoMoqData]
    public void ConsoleDisplayOutputTests_WriteLine_without_text_parameter(
        [Frozen] Mock<IConsole> console,
        ConsoleDisplayOutput sut)
    {
        sut.WriteLine();

        console.Verify(v => v.WriteLine(), Times.Once);
    }
}