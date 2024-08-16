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
        // Decrease the volume gradually while it's not at mute (values go from 0f to 1f)

        while (audioSource.volume > 0)
        {
            audioSource.volume -=Time.deltaTime / fadeDuration;
            yield return null;
        }

        //Stop the source once its muted

        audioSource.Stop();
    }
    protected IEnumerator FadeIn(AudioSource audioSource)
    {
        // Play the source

        audioSource.Play();

        // Increase the volume gradually while it's not at max

        while (audioSource.volume < 1f)
        {
            audioSource.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }
    }

    protected IEnumerator FadeOutIn(AudioSource audioSource, AudioClip newClip)
    {
        // Fade the track being played out and wait a small time in silence

        yield return StartCoroutine(FadeOut(audioSource));
        yield return new WaitForSeconds(0.5f);

        // Assing the new track and fade it in

        audioSource.clip = newClip;
        StartCoroutine(FadeIn(audioSource));
    }

    protected IEnumerator LevelMusic(int level)
    {
        // Play always the first track of the list when the level is loaded
        if (firstLevelTrack)
        {
            // Fade the music being played out and then fade the first track of the level in

            yield return StartCoroutine(FadeOutIn(musicAudioSource, musicTracks[level].S_groupTracks[0]));
            firstLevelTrack = false;

            // Calculate the time at which the fade should start, which is the lenght of the track minus the duration of the fade
            //The music plays during the WaitForSeconds time

            float firstDelay = musicTracks[level].S_groupTracks[0].length - fadeDuration;
            yield return new WaitForSeconds(firstDelay);   

            // Call the fade after the wait

            yield return StartCoroutine(FadeOut(musicAudioSource));
        }

        // Randomize the time without music being played

        yield return new WaitForSeconds(Random.Range(4f, 10f));

        // Randomize and assign the next track from the musicTracks list

        int newClip = Random.Range(0, musicTracks[DataHandler.GetLevelIndex()].S_groupTracks.Count);
        musicAudioSource.clip = musicTracks[level].S_groupTracks[newClip];

        // Fade the new random track in

        StartCoroutine(FadeIn(musicAudioSource));

        // Calculate the fade time

        float delay = musicTracks[level].S_groupTracks[newClip].length - fadeDuration;
        yield return new WaitForSeconds(delay);

        // Fade the track out and loop this coroutine once it ends

        yield return StartCoroutine(FadeOut(musicAudioSource));
        yield return StartCoroutine(LevelMusic(level));
    }
}
