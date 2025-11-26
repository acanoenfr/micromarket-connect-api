using System.Text;

namespace Com.MicroMarketConnect.API.Web.Builders;

public class HateOasUriBuilder
{
    private readonly StringBuilder _builder = new();

    internal HateOasUriBuilder AppendPathWithoutVersion(HttpRequest request)
    {
        if (!request.Path.HasValue)
            return this;
        var fragments = request.Path.Value.Split('/');
        var afterApiVersionPosition = fragments[0] == "api"
            ? 2
            : (request.Path.Value.StartsWith('/') && fragments[1] == "api")
                ? 3
                : 0;
        List<string> list;
        if (afterApiVersionPosition > 0)
        {
            list = fragments.Skip(afterApiVersionPosition).ToList();
            list.Insert(0, string.Empty);
        }
        else
        {
            list = fragments.ToList();
        }

        _builder.Append(string.Join("/", list.ToArray()));
        return this;
    }

    internal HateOasUriBuilder AppendSegmentValue(string value)
    {
        _builder.Append('/');
        _builder.Append(value);
        return this;
    }

    internal string GenerateUriString() => _builder.ToString();

    internal static HateOasUriBuilder CreateBuilder() => new();
}
