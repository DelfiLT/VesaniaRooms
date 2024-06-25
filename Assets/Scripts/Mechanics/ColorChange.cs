using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    #region Events
    public delegate void ChangeColor(bool originalColor);
    public static ChangeColor OnChangeColor;
    public static bool originalColor = true;
    #endregion

    [SerializeField] private List<AudioClip> colorChangeClips = new List<AudioClip>();
    public void ChangeColorEvent()
    {
        originalColor = !originalColor;
        OnChangeColor?.Invoke(originalColor);
        SoundManager.Instance.RandomizedSFX(colorChangeClips);
    }
}
