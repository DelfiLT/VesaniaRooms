using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "ScenesData", menuName = "ScenesData")]
public class ScenesData : ScriptableObject
{
    AsyncOperation sceneToLoad;
    public List<SceneData> levels = new List<SceneData>();
    public SceneData menu;

    public void LoadLevelWithIndex(int index)
    {
        if (index <= levels.Count)
        {
            sceneToLoad = (SceneManager.LoadSceneAsync("Level" + index.ToString()));
            if (!sceneToLoad.isDone)
            {
                Debug.Log("Loading..");
            }
        }
    }
    public void StartLevelIndex()
    {
        PlayerPrefs.SetInt("LevelIndex", 0);
    }

    public int GetLevelIndex()
    {
        return PlayerPrefs.GetInt("LevelIndex");
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("LevelIndex", GetLevelIndex() + 1);
        LoadLevelWithIndex(GetLevelIndex());
    }

    public void ContinueLevel()
    {
        LoadLevelWithIndex(GetLevelIndex());
    }

    public void NewGame()
    {
        StartLevelIndex();
        LoadLevelWithIndex(GetLevelIndex());
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(menu.sceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }
}