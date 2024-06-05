using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DataHandler
{
    public static Wrapper wrapper;
    public static bool loading;

    public static void SaveData()
    {
        wrapper.levels ??= new List<bool>();

        wrapper.levels.Add(true);

        PlayerPrefs.SetString("levelsData", wrapper.ToJson);
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


    [System.Serializable]
    public class Wrapper
    {
        public List<bool> levels;

        public int GetLevelIndex()
        {
            return levels == null ? 0 : levels.Count;
        }

        public string ToJson => JsonUtility.ToJson(this);

        public void FromJson(string json) => JsonUtility.FromJsonOverwrite(json, this);
    }
}
