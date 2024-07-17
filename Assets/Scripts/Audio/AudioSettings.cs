using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;

    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        musicSlider.onValueChanged.AddListener(delegate {MusicValueChanged(); });
        sfxSlider.onValueChanged.AddListener(delegate { SFXValueChanged(); });
    }

    private void MusicValueChanged()
    {
        if (musicSlider.value == 0f)
        {
            masterMixer.SetFloat("MusicVolume", -80);
        }
        else
        {
            masterMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value) * 20);
        }
    }

    private void SFXValueChanged()
    {
        if (sfxSlider.value == 0f)
        {
            masterMixer.SetFloat("AmbienceVolume", -80);
            masterMixer.SetFloat("SFXVolume", -80);
        }
        else
        {
            masterMixer.SetFloat("AmbienceVolume", (Mathf.Log10(sfxSlider.value) * 20) -7.72f);
            masterMixer.SetFloat("SFXVolume", Mathf.Log10(sfxSlider.value) * 20);
        }
    }

    public void ChangeMusicByButton(float changeValue)
    {
        musicSlider.value += changeValue;
    }
    public void ChangeSFXByButton(float changeValue)
    {
        sfxSlider.value += changeValue;
    }
}
