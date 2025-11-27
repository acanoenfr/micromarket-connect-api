namespace Reqnroll;

internal static class TableExtensions
{
    internal static IEnumerable<T> CreateSetWithHeadersReMapped<T>(this Table table, Dictionary<string, string> headerMapping)
    {
        var set = ChangeHeaders(table, headerMapping);

        return set.CreateSet<T>();
    }

    internal static Table ChangeHeaders(Table table, Dictionary<string, string> headerMapping)
    {
        var mappedTable = new Table(table.Header.Select(x => headerMapping.ContainsKey(x) ? headerMapping[x] : x).ToArray());

        foreach (var row in table.Rows)
        {
            mappedTable.AddRow(row.Values.ToArray());
        }

        return mappedTable;
    }
}
