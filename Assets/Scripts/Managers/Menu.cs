using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public ScenesData ScenesData;
    public Button newGameButton;
    public Button loadGameButton;

    private void Awake()
    {
        loadGameButton.onClick.AddListener(() =>
        {
            ScenesData.ContinueLevel();
        });

        if (ScenesData.GetLevelIndex() == 0)
        {
            newGameButton.gameObject.SetActive(true);
        }
        else
        {
            loadGameButton.gameObject.SetActive(true);
        }
    }
}