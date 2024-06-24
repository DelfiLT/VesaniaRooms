using UnityEngine;
using System.Collections.Generic;

public class ColorChange : MonoBehaviour
{
    #region Events
    public delegate void ChangeColor();
    public static ChangeColor OnChangeColor;
    #endregion

    [SerializeField] private List<AudioClip> colorChangeClips = new List<AudioClip>();
    public void ChangeColorEvent()
    {
        SoundManager.Instance.RandomizedSFX(colorChangeClips);
        OnChangeColor?.Invoke();
    }
}
