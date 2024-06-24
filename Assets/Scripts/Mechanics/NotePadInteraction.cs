using UnityEngine;

public class NotePadInteraction : MonoBehaviour
{
    [SerializeField] private AudioClip notePadSound;
    public void CallNoteSound()
    {
        Debug.Log("note button");
        SoundManager.Instance.PlaySFX(notePadSound);
    }
}
