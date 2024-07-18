using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvent : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clipsToPlay = new List<AudioClip>();

    public void PlayEventSFX(int clip)
    {
        SoundManager.Instance.PlaySFX(clipsToPlay[clip]);
    }
}
