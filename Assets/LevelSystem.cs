using UnityEngine;

public static class LevelSystem
{
    public static void UnlockLevel(int level)
    {
        PlayerPrefs.SetInt("level" + level, 1);
    }

    public static bool IsUnlocked(int level)
    {
        return PlayerPrefs.GetInt("level" + level) == 1;
    }
}
