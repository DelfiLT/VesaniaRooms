using System;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button playButton;

    private void Awake()
    {
        DataHandler.LoadData();

        playButton.onClick.AddListener(() =>
        {
            SceneUtils.PlayScene(DataHandler.GetLevelIndex().ToString());
        });
    }

}
