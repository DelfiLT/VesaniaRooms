using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip padlockClip;
    public bool[] puzzles;
    [SerializeField] private GameObject winPanel;

    private void Awake()
    {
        DataHandler.LoadData();
    }

    public void CompletePuzzle(int puzzleIndex)
    {
        puzzles[puzzleIndex] = true;
        SoundManager.Instance.PlaySFX(padlockClip);
    }

    public void FinishLevel(int levelIndex)
    {
        //DataHandler.SaveData(levelIndex);
        StartCoroutine(waitToFinish());
        SoundManager.Instance.PlaySFX(winClip);
    }

    IEnumerator waitToFinish ()
    {
        yield return new WaitForSeconds(2);
        winPanel.SetActive(true);
    }
}
