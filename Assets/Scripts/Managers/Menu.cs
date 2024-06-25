using System;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private AudioClip playButtonClip;
    public Button playButton;
    public Button[] levels;

    private void Awake()
    {
        DataHandler.LoadData();

        foreach (Button level in levels)
        {
            int buttonIndex = Array.IndexOf(levels, level);

            if (DataHandler.GetLevelIndex() <= buttonIndex)
            {
                level.interactable = false;
            }
            else
            {
                level.interactable = true;
            }

            level.onClick.AddListener(() => {
                SceneUtils.PlayScene(buttonIndex.ToString());
            });
        }

        playButton.onClick.AddListener(() =>
        {
            SceneUtils.PlayScene(DataHandler.GetLevelIndex().ToString());
            PlayLevelAudio();
        });
    }

    private void PlayLevelAudio()
    {
        SoundManager.Instance.PlaySFX(playButtonClip);
        SoundManager.Instance.StartLevelMusic();
    }
}
