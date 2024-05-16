using System.Xml.Linq;
using Base64Test;
using FluentAssertions;

namespace Base64Test.Test;

[TestFixture]
[TestOf(typeof(XmlConfigurationService))]
public class XmlConfigurationServiceTest
{
    [Test]
    public void AddSubscriptionDataToXml()
    {
        XDocument subject = XmlConfigurationService.AddSubscriptionDataToXml();

        XElement? subscriptionDataElement = subject.Root?.Element("SubscriptionData");

        subscriptionDataElement?.ToString()
            .Should().Be("<SubscriptionData>B7|638514657937169899</SubscriptionData>");

    }
}