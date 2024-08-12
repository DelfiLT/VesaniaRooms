using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1.5f;

    [SerializeField] protected List<ScriptableMusic> musicTracks = new List<ScriptableMusic>();
    [SerializeField] protected AudioSource sfxAudioSource, musicAudioSource, ambienceAudioSource;

    protected bool firstLevelTrack = true;
    protected IEnumerator FadeOut(AudioSource audioSource)
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
    protected IEnumerator FadeIn(AudioSource audioSource)
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

    protected IEnumerator FadeOutIn(AudioSource audioSource, AudioClip newClip)
    {
        yield return StartCoroutine(FadeOut(audioSource));
        yield return new WaitForSeconds(0.5f);
        audioSource.clip = newClip;
        audioSource.Play();
        StartCoroutine(FadeIn(audioSource));
    }

    protected IEnumerator LevelMusic(int level)
    {
        if (firstLevelTrack)
        {
            yield return StartCoroutine(FadeOutIn(musicAudioSource, musicTracks[level].S_groupTracks[0]));
            firstLevelTrack = false;
            float firstDelay = musicTracks[level].S_groupTracks[0].length - fadeDuration;
            yield return new WaitForSeconds(firstDelay);
            yield return StartCoroutine(FadeOut(musicAudioSource));
        }
        yield return new WaitForSeconds(Random.Range(4f, 10f));
        int newClip = Random.Range(0, musicTracks[DataHandler.GetLevelIndex()].S_groupTracks.Count);
        musicAudioSource.clip = musicTracks[level].S_groupTracks[newClip];
        StartCoroutine(FadeIn(musicAudioSource));
        float delay = musicTracks[level].S_groupTracks[newClip].length - fadeDuration;
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(FadeOut(musicAudioSource));
        yield return StartCoroutine(LevelMusic(level));
    }
}
