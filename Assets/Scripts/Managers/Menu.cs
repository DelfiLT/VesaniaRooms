using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button playButton;
    public TextMeshProUGUI buttonText;

    private void Awake()
    {
        DataHandler.LoadData();

        buttonText.text = DataHandler.GetLevelIndex() == 0 ? "New Game" : "Continue";

        playButton.onClick.AddListener(() =>
        {
            PlayLevel();
        });
    }

    public void PlayLevel()
    {
        Debug.Log(DataHandler.GetLevelIndex());
        SceneManager.LoadScene("Level" + DataHandler.GetLevelIndex());
    }

    public void PlayLevelWithIndex(int index)
    {
        SceneManager.LoadScene("Level" + index);
    }
}