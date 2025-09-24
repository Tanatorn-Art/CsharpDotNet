public static class PlayAnalyzer
{
    public static string AnalyzeOnField(int shirtNum)
    {
        switch (shirtNum)
        {
            case 1:
                return "goalie";
            case 2:
                return "left back";
            case 3:
            case 4:
                return "center back";
            case 5:
                return "right back";
            case 6:
            case 7:
            case 8:
                return "midfielder";
            case 9:
                return "left wing";
            case 10:
                return "striker";
            case 11:
                return "right wing";
            default:
                return "UNKNOWN";
        }
    }

    public static string AnalyzeOffField(object report)
    {
        switch (report)
        {
            case int supporters:
                return $"There are {supporters} supporters at the match.";
            case string announcement:
                return announcement;
            case Injury injury:
                // Try different ways to access the player number
                try
                {
                    // Check for common property/field names
                    var type = injury.GetType();
                    var playerProperty = type.GetProperty("Player");
                    if (playerProperty != null)
                    {
                        var playerNumber = playerProperty.GetValue(injury);
                        return $"Oh no! Player {playerNumber} is injured. Medics are on the field.";
                    }

                    var playerField = type.GetField("Player");
                    if (playerField != null)
                    {
                        var playerNumber = playerField.GetValue(injury);
                        return $"Oh no! Player {playerNumber} is injured. Medics are on the field.";
                    }

                    // Try other common field names
                    foreach (var fieldName in new[] { "_player", "player", "PlayerNumber", "playerId" })
                    {
                        var field = type.GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                        if (field != null)
                        {
                            var playerNumber = field.GetValue(injury);
                            return $"Oh no! Player {playerNumber} is injured. Medics are on the field.";
                        }
                    }
                }
                catch
                {
                    // Fallback if reflection fails
                }
                return "Oh no! A player is injured. Medics are on the field.";
            case Foul:
                return "The referee deemed a foul.";
            case Incident:
                return "An incident happened.";
            case Manager manager when manager.Club != null:
                return $"{manager.Name} ({manager.Club})";
            case Manager manager:
                return manager.Name;
            default:
                return "";
        }
    }
}