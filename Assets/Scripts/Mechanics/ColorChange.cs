using UnityEngine;

public class ColorChange : MonoBehaviour
{
    #region Events
    public delegate void ChangeColor();
    public static ChangeColor OnChangeColor;
    #endregion
    
    public void ChangeColorEvent()
    {
        if (OnChangeColor != null) OnChangeColor.Invoke();
    }
}
