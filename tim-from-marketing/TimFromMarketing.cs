static class Badge
{
    public static string Print(int? id, string name, string? department)
    {
        string dept = department?.ToUpper() ?? "OWNER";

        if (id.HasValue)
        {
            return $"[{id}] - {name} - {dept}";
        }
        else
        {
            return $"{name} - {dept}";
        }
    }
}
