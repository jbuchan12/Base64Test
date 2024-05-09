namespace Base64Test;

internal class Program
{
    static void Main(string[] args)
    {
        List<int> subscriptionIds = GetSubscriptionIds("/=//B");
    }

    private static List<int> GetSubscriptionIds(string base64String)
    {
        string[] subscriptionSplit = base64String.Split('=');

        return subscriptionSplit
            .Select(sub => Base64Integer.FromString(sub).IntegerValue)
            .ToList();
    }
}