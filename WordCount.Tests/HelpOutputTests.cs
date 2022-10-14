using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using WordCount.Abstractions.SystemAbstractions.Reflection;
using WordCount.Implementations.Output;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Output;
using WordCount.Models.Parameters;
using Xunit;

namespace WordCount.Tests;

public class HelpOutputTests
{
    [Theory, AutoMoqData]
    public void HelpOutputTests_ShowHelpIfRequested_HelpParamater_is_not_present_Expect_False(
        [Frozen] Mock<IHelpParameterParser> helpParameterParser,
        HelpOutput sut)
    {
        helpParameterParser
            .Setup(m => m.ParseHelpParameter())
            .Returns(new HelpParameter {IsPresent = false});

        var actual = sut.ShowHelpIfRequested();

        actual.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public void HelpOutputTests_If_HelpParameter_Present_Expect_Outputs(
        [Frozen] Mock<IHelpParameterParser> helpParameterParser,
        [Frozen] Mock<IAssembly> assembly,
        [Frozen] Mock<IDisplayOutput> displayOutput,
        HelpOutput sut)
    {
        helpParameterParser
            .Setup(m => m.ParseHelpParameter())
            .Returns(new HelpParameter {IsPresent = true});

        assembly
            .SetupGet(m => m.Name)
            .Returns("WordCount");

        assembly
            .SetupGet(m => m.Version)
            .Returns("1.2.3.4");

        sut.ShowHelpIfRequested();

        displayOutput.Verify(v => v.WriteLine("WordCount - 1.2.3.4"), Times.Once);
        displayOutput.Verify(v => v.WriteLine(""), Times.Once);
        displayOutput.Verify(v => v.WriteLine("-h | -help : Display this help"), Times.Once);
        displayOutput.Verify(v => v.WriteLine("-index : Display the index of the analyzed Text"), Times.Once);
        displayOutput.Verify(v => v.WriteLine("-dictionary=file : Uses the dictionary with the given file"), Times.Once);
        displayOutput.Verify(v => v.WriteLine("-stopwordlist=file : Uses the stopword with the given file. Default: stopword.txt"), Times.Once);
    }

    [Theory, AutoMoqData]
    public void HelpOutputTests_If_HelpParameter_not_present_expect_no_Outputs(
        [Frozen] Mock<IHelpParameterParser> helpParameterParser,
        [Frozen] Mock<IDisplayOutput> displayOutput,
        HelpOutput sut)
    {
        helpParameterParser
            .Setup(m => m.ParseHelpParameter())
            .Returns(new HelpParameter { IsPresent = false });

        sut.ShowHelpIfRequested();

        displayOutput.Verify(v => v.WriteLine(It.IsAny<string>()), Times.Never);
    }
}