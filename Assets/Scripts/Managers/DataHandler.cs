using System.Collections.Generic;
using UnityEngine;

public static class DataHandler
{
    public static Wrapper wrapper;
    public static bool loading;
    public static int maxLevels = 2;

    public static void SaveData(int levelIndex)
    {
        wrapper.levels ??= new List<bool>();

        while (wrapper.levels.Count <= levelIndex)
        {
            wrapper.levels.Add(false);
        }

        if (!wrapper.levels[levelIndex])
        {
            wrapper.levels[levelIndex] = true;
            PlayerPrefs.SetString("levelsData", wrapper.ToJson);
        }
    }

    public static void LoadData()
    {
        wrapper = new Wrapper();

        if (loading || !PlayerPrefs.HasKey("levelsData")) return;

        loading = true;
        wrapper.FromJson(PlayerPrefs.GetString("levelsData"));
    }

    public static int GetLevelIndex()
    {
        return wrapper.GetLevelIndex();
    }

    public static bool HasNextLevel(int currentLevelIndex)
    {
        return currentLevelIndex + 1 < maxLevels;
    }

    [System.Serializable]
    public class Wrapper
    {
        public List<bool> levels;

        public int GetLevelIndex()
        {
            if (levels == null || levels.Count == 0)
            {
                return 0;
            }

            for (int i = 0; i < levels.Count; i++)
            {
                if (!levels[i])
                {
                    return i;
                }
            }

            return levels.Count;
        }

        public string ToJson => JsonUtility.ToJson(this);

        public void FromJson(string json) => JsonUtility.FromJsonOverwrite(json, this);
    }
}
