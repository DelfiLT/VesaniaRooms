using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private AudioClip winClip;
    public bool[] puzzles;
    [SerializeField] private GameObject winPanel;

    private void Awake()
    {
        DataHandler.LoadData();
    }

    public void CompletePuzzle(int puzzleIndex)
    {
        puzzles[puzzleIndex] = true;
    }

    public void FinishLevel(int levelIndex)
    {
        //TO DO: Make a coroutine to unlock the padlock with an animation and then open the victory panel
        DataHandler.SaveData(levelIndex);
        winPanel.SetActive(true);
        SoundManager.Instance.PlaySFX(winClip);
    }
}
