using FluentAssertions;

namespace Base64Test.Test;

[TestFixture]
[TestOf(typeof(Base64Integer))]
public class Base64IntegerTests
{

    [TestCase(64, "/")]
    [TestCase(65, "/B")]
    [TestCase(0, "A")]
    [TestCase(129, "//B")]
    [TestCase(27, "b")]
    public void FromInteger_ReturnsCorrectBase64String(int integerValue, string expectedBase64String)
    {
        Base64Integer subject = Base64Integer.FromInteger(integerValue);

        subject.StringValue
            .Should()
            .Be(expectedBase64String);
    }

    [TestCase("/", 64)]
    [TestCase("/B", 65)]
    [TestCase("A", 0)]
    [TestCase("//B", 129)]
    [TestCase("b", 27)]
    public void FromString_ReturnsCorrectInteger(string base64String, int expectedIntegerValue)
    {
        Base64Integer subject = Base64Integer.FromString(base64String);

        subject.IntegerValue
            .Should()
            .Be(expectedIntegerValue);
    }
}