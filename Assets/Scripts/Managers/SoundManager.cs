using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource sfxAudioSource, musicAudioSource, ambienceAudioSource;
    public AudioSource MusicSource
    {
        get { return musicAudioSource; }
    }
    public AudioSource AmbienceSource
    {
        get { return ambienceAudioSource; }
    }

    public float fadeDuration = 1.5f;
    private bool firstLevelTrack = true;
    [SerializeField] private List<ScriptableMusic> musicTracks = new List<ScriptableMusic>();
    [SerializeField] private List<AudioClip> ambienceTracks = new List<AudioClip>();

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

    private void Start()
    {
        DataHandler.LoadData();
    }

    public void StartLevelMusic()
    {
        StartCoroutine(LevelMusic());
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
        StartCoroutine(FadeOut(source, fadeDuration));
    }
    public void StartFadeIn(AudioSource source)
    {
        StartCoroutine(FadeIn(source, fadeDuration));
    }
    public void StartTransition(AudioSource source, AudioClip newClip)
    {
        StartCoroutine(FadeOutIn(source, newClip, fadeDuration));
    }


    private IEnumerator FadeOut(AudioSource audioSource, float fadeDuration)
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
    private IEnumerator FadeIn(AudioSource audioSource, float fadeDuration)
    {
        Debug.Log("Music10");
        audioSource.Play();
        audioSource.volume = 0f;
        float targetVolume = 1.0f;

        while (audioSource.volume < targetVolume)
        {
            Debug.Log("MusicIn");
            audioSource.volume += targetVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
        Debug.Log("Music11");
        audioSource.volume = targetVolume;
    }
    private IEnumerator FadeOutIn(AudioSource audioSource, AudioClip newClip, float fadeDuration)
    {
        Debug.Log("Music2");
        yield return StartCoroutine(FadeOut(audioSource, fadeDuration));
        Debug.Log("Music3");
        audioSource.clip = newClip;
        audioSource.Play();

        yield return StartCoroutine(FadeIn(audioSource, fadeDuration));
        Debug.Log("Music4");
    }

    private IEnumerator LevelMusic()
    {
        if(firstLevelTrack)
        {
            Debug.Log("Music1");
            StartTransition(musicAudioSource, musicTracks[DataHandler.GetLevelIndex()].S_groupTracks[0]);
            Debug.Log("Music5");
            firstLevelTrack = false;
            float firstDelay = musicTracks[DataHandler.GetLevelIndex()].S_groupTracks[0].length - fadeDuration;
            yield return new WaitForSeconds(firstDelay);
            Debug.Log("Music6");
            yield return StartCoroutine(FadeOut(musicAudioSource, fadeDuration));
            Debug.Log("Music7");
        }
        Debug.Log("Music8");
        yield return new WaitForSeconds(Random.Range(4f, 10f));
        Debug.Log("Music9");
        int newClip = Random.Range(0, musicTracks[DataHandler.GetLevelIndex()].S_groupTracks.Count);
        musicAudioSource.clip = musicTracks[DataHandler.GetLevelIndex()].S_groupTracks[newClip];
        StartCoroutine(FadeIn(musicAudioSource, fadeDuration));
        Debug.Log("Music12");
        float delay = musicTracks[DataHandler.GetLevelIndex()].S_groupTracks[newClip].length - fadeDuration;
        yield return new WaitForSeconds(delay);
        Debug.Log("Music13");
        yield return StartCoroutine(FadeOut(musicAudioSource, fadeDuration));
        Debug.Log("Music14");
        yield return StartCoroutine(LevelMusic());
        Debug.Log("Music15");


    }
}

