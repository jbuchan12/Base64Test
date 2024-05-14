using FluentAssertions;

namespace Base64Test.Test;

[TestFixture]
[TestOf(typeof(Base64Integer))]
public class Base64IntegerTests
{

    [TestCase(27U, "b")]
    [TestCase(63U, "-")]
    [TestCase(64U, "BA")]
    [TestCase(4_095U, "--")]
    [TestCase(262_143U, "---")]
    [TestCase(262_144U, "BAAA")]
    public void FromInteger_ReturnsCorrectBase64String(uint integerValue, string expectedBase64String)
    {
        Base64Integer subject = Base64Integer.FromInteger(integerValue);

        subject.StringValue
            .Should()
            .Be(expectedBase64String);
    }

    [TestCase("b", 27U)]
    [TestCase("-", 63U)]
    [TestCase("BA",64U)]
    [TestCase("--", 4_095U)]
    [TestCase("---", 262_143U)]
    [TestCase("BAAA", 262_144U)]
    public void FromString_ReturnsCorrectInteger(string base64String, uint expectedIntegerValue)
    {
        Base64Integer subject = Base64Integer.FromString(base64String);

        subject.IntegerValue
            .Should()
            .Be(expectedIntegerValue);
    }

    [Test]
    public void Min_ReturnsCorrectMinimumValue()
    {
        Base64Integer subject = Base64Integer.Min;

        subject.IntegerValue
            .Should()
            .Be(0);

        subject.StringValue
            .Should()
            .Be("A");
    }


    [Test]
    public void Max_ReturnsCorrectMaximumValue()
    {
        Base64Integer subject = Base64Integer.Max;

        subject.IntegerValue
            .Should()
            .Be(uint.MaxValue);

        subject.StringValue
            .Should()
            .Be("D-----");
    }
}