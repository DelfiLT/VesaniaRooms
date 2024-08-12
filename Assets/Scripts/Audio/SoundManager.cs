using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : AudioManager
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClip menuMusicClip;
    [SerializeField] private List<AudioClip> ambienceTracks = new List<AudioClip>();
    [SerializeField] private AudioClip playButtonClip;

    private void Awake()
    {
        Singleton();
    }
    private void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        else Instance = this;
        DontDestroyOnLoad(this);
    }
    public void ChangeSceneAudio(string levelName)
    {
        StopAllCoroutines();
        firstLevelTrack = true;
        if (levelName == "Menu")
        {
            ExitLevel();
            musicAudioSource.loop = true;
        }
        else
        {
            musicAudioSource.loop = false;
            int.TryParse(levelName, out int levelIndex);
            StartLevelMusic(levelIndex);
        }

    }
    public void StartLevelMusic(int level)
    {
        PlaySFX(playButtonClip);
        if ( musicTracks.Count <= level || musicTracks[level] == null) { return; }
        StartCoroutine(LevelMusic(level));
        StartCoroutine(FadeOutIn(ambienceAudioSource, ambienceTracks[level]));
    }
    public void ExitLevel()
    {
        StartCoroutine(FadeOut(ambienceAudioSource));
        StartCoroutine(FadeOutIn(musicAudioSource, menuMusicClip));
    }
    public void RandomizedSFX(List<AudioClip> clipList)
    {
        PlaySFX(clipList[Random.Range(0, clipList.Count)]);
    }
    public void PlaySFX(AudioClip clip)
    {
        if(clip == null) { return; }
        sfxAudioSource.PlayOneShot(clip);
    }

}

