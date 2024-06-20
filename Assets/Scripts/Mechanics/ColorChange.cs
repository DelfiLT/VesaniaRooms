using UnityEngine;

public class ColorChange : MonoBehaviour
{
    #region Events
    public delegate void ChangeColor(bool originalColor);
    public static ChangeColor OnChangeColor;
    public static bool originalColor = true;
    #endregion

    public void ChangeColorEvent()
    {
        originalColor = !originalColor;
        OnChangeColor?.Invoke(originalColor);
    }
}
