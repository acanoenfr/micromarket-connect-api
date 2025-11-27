using Reqnroll.Assist;
using System.Globalization;

namespace Com.MicroMarketConnect.API.AcceptanceTests.Configurations.Retrievers;

internal class DateOnlyValueRetriever : IValueRetriever
{
    private readonly string _format;
    public DateOnlyValueRetriever(string format) => _format = format;

    public bool CanRetrieve(KeyValuePair<string, string> kv, Type targetType, Type propertyType)
        => propertyType == typeof(DateOnly);

    public object Retrieve(KeyValuePair<string, string> kv, Type targetType, Type propertyType)
        => DateOnly.ParseExact(kv.Value, _format, CultureInfo.InvariantCulture);
}
