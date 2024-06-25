using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource sfxAudioSource, musicAudioSource, ambienceAudioSource;

    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private AudioClip menuMusicClip;
    [SerializeField] private List<ScriptableMusic> musicTracks = new List<ScriptableMusic>();
    [SerializeField] private List<AudioClip> ambienceTracks = new List<AudioClip>();

    private bool firstLevelTrack = true;

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

    public void StartLevelMusic()
    {
        DataHandler.LoadData();
        StartCoroutine(LevelMusic());
        ambienceAudioSource.clip = ambienceTracks[DataHandler.GetLevelIndex()];
        StartCoroutine(FadeIn(ambienceAudioSource));
    }
    public void RandomizedSFX(List<AudioClip> clipList)
    {
        PlaySFX(clipList[Random.Range(0, clipList.Count)]);
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }
    public void StartFadeOut(AudioSource source)
    {
        StartCoroutine(FadeOut(source));
    }
    public void StartFadeIn(AudioSource source)
    {
        StartCoroutine(FadeIn(source));
    }
    public void StartTransition(AudioSource source, AudioClip newClip)
    {
        StartCoroutine(FadeOutIn(source, newClip));
    }
    public void ExitLevel()
    {
        StopAllCoroutines();
        StartFadeOut(ambienceAudioSource);
        StartFadeOut(sfxAudioSource);
        firstLevelTrack = true;
        StartCoroutine(FadeOutIn(musicAudioSource,menuMusicClip));
    }

    private IEnumerator FadeOut(AudioSource audioSource)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
    private IEnumerator FadeIn(AudioSource audioSource)
    {
        audioSource.Play();
        audioSource.volume = 0f;
        float targetVolume = 1.0f;

        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += targetVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
        audioSource.volume = targetVolume;
    }
    private IEnumerator FadeOutIn(AudioSource audioSource, AudioClip newClip)
    {
        yield return StartCoroutine(FadeOut(audioSource));
        yield return new WaitForSeconds(0.5f);
        audioSource.clip = newClip;
        audioSource.Play();
        StartCoroutine(FadeIn(audioSource));
    }
    private IEnumerator LevelMusic()
    {
        if(firstLevelTrack)
        {
            yield return StartCoroutine(FadeOutIn(musicAudioSource, musicTracks[DataHandler.GetLevelIndex()].S_groupTracks[0]));
            firstLevelTrack = false;
            float firstDelay = musicTracks[DataHandler.GetLevelIndex()].S_groupTracks[0].length - fadeDuration;
            yield return new WaitForSeconds(firstDelay);
            yield return StartCoroutine(FadeOut(musicAudioSource));
        }
        yield return new WaitForSeconds(Random.Range(4f, 10f));
        int newClip = Random.Range(0, musicTracks[DataHandler.GetLevelIndex()].S_groupTracks.Count);
        musicAudioSource.clip = musicTracks[DataHandler.GetLevelIndex()].S_groupTracks[newClip];
        StartCoroutine(FadeIn(musicAudioSource));
        float delay = musicTracks[DataHandler.GetLevelIndex()].S_groupTracks[newClip].length - fadeDuration;
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(FadeOut(musicAudioSource));
        yield return StartCoroutine(LevelMusic());
    }
}

