using WordCount.Implementations.ArgumentsHandling;
using WordCount.Models;
using WordCount.Tests.XUnitHelpers;
using Xunit;

namespace WordCount.Tests.ArgumentsHandlingTests
{
    public class IndexParameterParserTests
    {
        [NamedFact]
        public void IndexParameterParserTests_Args_have_no_index_Parameter_Expect_IsPresent_False()
        {
            IndexParameterParser systemUnderTest = new IndexParameterParser();
            IndexParameter actual = systemUnderTest.ParseIndexParameter(args: new[] { "mytext.txt" });

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void IndexParameterParserTests_Args_is_null_expect_IsPresent_False()
        {
            IndexParameterParser systemUnderTest = new IndexParameterParser();
            IndexParameter actual = systemUnderTest.ParseIndexParameter(args: null);

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void IndexParameterParserTests_Args_is_empty_expect_IsPresent_False()
        {
            IndexParameterParser systemUnderTest = new IndexParameterParser();
            IndexParameter actual = systemUnderTest.ParseIndexParameter(args: new string[0]);

            Assert.NotNull(@object: actual);
            Assert.False(condition: actual.IsPresent);
        }

        [NamedFact]
        public void IndexParameterParserTests_Args_has_index_Parameter_expect_IsPresent_True()
        {
            IndexParameterParser systemUnderTest = new IndexParameterParser();
            IndexParameter actual = systemUnderTest.ParseIndexParameter(args: new[] {"-index"});

            Assert.NotNull(@object: actual);
            Assert.True(condition: actual.IsPresent);
        }
    }
}
