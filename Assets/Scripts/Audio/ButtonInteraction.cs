using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    [SerializeField] private AudioClip notePadSound;
    [SerializeField] private AudioClip genericButtonSound;
    [SerializeField] private AudioClip secondaryButtonSound;
    public void CallNoteSound()
    {
        SoundManager.Instance.PlaySFX(notePadSound);
    }

    public void CallButtonSound()
    {
        SoundManager.Instance.PlaySFX(genericButtonSound);
    }

    public void CallSecondaryButtonSound()
    {
        SoundManager.Instance.PlaySFX(secondaryButtonSound);
    }
}
