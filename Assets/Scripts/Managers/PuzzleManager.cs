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

    public void FinishLevel()
    {
        DataHandler.SaveData();
        if (DataHandler.GetLevelIndex() == 2)
        {
            SceneUtils.PlayScene("Menu");
        }
        else
        {
           SceneUtils.PlayScene(DataHandler.GetLevelIndex().ToString());
        }
    }
}
