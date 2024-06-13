using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private bool[] puzzles;

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
        DataHandler.SaveData(levelIndex);

        if (DataHandler.HasNextLevel(levelIndex))
        {
            SceneUtils.PlayScene((levelIndex + 1).ToString());
        }
        else
        {
            Debug.Log("No more available levels.");
            SceneUtils.PlayScene("Menu");
        }
    }

}
