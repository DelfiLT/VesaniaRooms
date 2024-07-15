using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    #region Events
    public delegate void ChangeColor(bool originalColor);
    public static ChangeColor OnChangeColor;
    #endregion

    [SerializeField] private Animator _changeColorAnimator;

    public static bool originalColor = true;


    [SerializeField] private List<AudioClip> colorChangeClips = new List<AudioClip>();
    public void ChangeColorEvent()
    {
        originalColor = !originalColor;
        OnChangeColor?.Invoke(originalColor);
        SoundManager.Instance.RandomizedSFX(colorChangeClips);

        if (originalColor)
        {
            _changeColorAnimator.Play("reset_color");
        } else
        {
            _changeColorAnimator.Play("change_color");
        }
    }
}
