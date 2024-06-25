using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private bool[] puzzles;

    public void CompletePuzzle(int puzzleIndex)
    {
        puzzles[puzzleIndex] = true;
    }

    public void FinishLevel()
    {
        DataHandler.SaveData();
        SceneManager.LoadScene("Menu");
        SoundManager.Instance.ExitLevel();
    }
}
