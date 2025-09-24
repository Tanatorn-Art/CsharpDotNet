public static class TwoFer
{
    public static string Speak(string name)
    {
        return $"One for {name}, one for me.";
    }

    public static string Speak()
    {
        return Speak("you");
    }
}