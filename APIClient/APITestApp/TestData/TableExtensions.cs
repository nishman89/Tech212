using TechTalk.SpecFlow;

public static class TableExtensions
{
    public static Dictionary<string, string> ToDictionary(Table table)
    {
        var dict = new Dictionary<string, string>();
        foreach (var row in table.Rows)
        {
            dict.Add(row["key"], row["value"]);
        }
        return dict;
    }
}