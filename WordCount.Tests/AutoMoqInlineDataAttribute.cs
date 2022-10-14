using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace WordCount.Tests;

public class AutoMoqInlineDataAttribute : InlineAutoDataAttribute
{
    public AutoMoqInlineDataAttribute(params object[] values)
        : base(() => new Fixture().Customize(new AutoMoqCustomization()), values)
    {
    }
}