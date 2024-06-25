using UnityEngine;

public class NotePadInteraction : MonoBehaviour
{
    [SerializeField] private AudioClip notePadSound;
    public void CallNoteSound()
    {
        SoundManager.Instance.PlaySFX(notePadSound);
    }
}
