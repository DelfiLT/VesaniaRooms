using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUtils : MonoBehaviour
{
    public static void PlayScene(string name)
    {
        SceneManager.LoadScene(name);
        SoundManager.Instance.ChangeSceneAudio(name);
    }

}
