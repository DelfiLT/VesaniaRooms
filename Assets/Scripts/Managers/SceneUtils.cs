using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUtils : MonoBehaviour
{
    public static void PlayScene(string name)
    {
        SceneManager.LoadScene(name);
        SelectMusic(name);

    }

    private static void SelectMusic(string name)
    {
        if (name == "Menu")
        {
            SoundManager.Instance.ExitLevel();
        }
        else
        {
            SoundManager.Instance.StartLevelMusic();
        }
    }

}
