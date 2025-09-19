public static class PhoneNumber
{
    public static (bool IsNewYork, bool IsFake, string LocalNumber) Analyze(string phoneNumber)
    {
        var parts = phoneNumber.Split('-');
        var areaCode = parts[0];
        var prefix = parts[1];
        var local = parts[2];

        return (
            areaCode == "212",
            prefix == "555",
            local
        );
        // var local = phoneNumber.Substring(phoneNumber.IndexOf("-") + 1);
        // return (phoneNumber.StartsWith("212"), phoneNumber.StartsWith("555"), local);
    }

    public static bool IsFake((bool IsNewYork, bool IsFake, string LocalNumber) phoneNumberInfo)
    {
        return phoneNumberInfo.IsFake;
    }
}
