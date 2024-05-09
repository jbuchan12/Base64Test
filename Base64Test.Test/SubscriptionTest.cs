using FluentAssertions;
using FluentAssertions.Execution;

namespace Base64Test.Test;

[TestFixture]
[TestOf(typeof(Subscription))]
public class SubscriptionTest
{
    [Test]
    public void Subscription_Constructor_ValidInput_Success()
    {
        DateTime currentTime = DateTime.Now;
        var subject = new Subscription($"//|{currentTime.Ticks}");

        using (new AssertionScope())
        {
            Base64Integer expectedId = Base64Integer.FromInteger(128);

            subject.Id
                .Should()
                .BeEquivalentTo(expectedId);

            subject.Expiry
                .Should()
                .Be(currentTime);
        }
    }

    [Test]
    public void Subscription_Constructor_InvalidColumnNumber_Failure()
    {
        Action action = () =>
        {
            _ = new Subscription("//");
        };

        action
            .Should()
            .Throw<InvalidSubscriptionDataFormatException>()
            .WithMessage("Subscription data requires at least 2 data columns");
    }

    [Test]
    public void Subscription_Constructor_InvalidDateTimeComponent_Failure()
    {
        Action action = () =>
        {
            _ = new Subscription("//|Invalid");
        };

        action
            .Should()
            .Throw<InvalidSubscriptionDataFormatException>()
            .WithMessage("DateTime component of the subscription string is invalid");
    }

    [TestCase(long.MaxValue)]
    [TestCase(-1)]
    public void Subscription_Constructor_DateTimeTicksAreOutOfRange_Failure(long ticks)
    {
        Action action = () =>
        {
            _ = new Subscription($"//|{ticks}");
        };

        action
            .Should()
            .Throw<InvalidSubscriptionDataFormatException>()
            .WithMessage("The Number of ticks in the datetime component were out of range, this number needs to be positive and lower than the maximum ticks");
    }
}