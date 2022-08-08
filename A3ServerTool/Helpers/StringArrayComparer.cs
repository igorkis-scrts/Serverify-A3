namespace A3ServerTool.Helpers;

public class StringArrayComparer : IComparer<string[]>
{
    public int Compare(string[] x, string[] y)
    {
        return x.Length - y.Length;
    }
}
