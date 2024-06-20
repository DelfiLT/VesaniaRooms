using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level01 : MonoBehaviour
{
    [SerializeField] private Transform[] firstColorPositions;
    [SerializeField] private Transform[] secondColorPositions;

    [SerializeField] private GameObject[] pillows;
    [SerializeField] private LayerMask pillowMask;

    private PuzzleManager puzzleManager;
    private InputManager inputManager;

    private bool selected;
    private GameObject pillowSelected;
    private Vector3 originalPosition;

    private void Awake()
    {
        puzzleManager = GetComponent<PuzzleManager>();
        inputManager = InputManager.Instance;
    }

    private void OnTouch(Ray ray)
    {
        if (selected)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, 100, pillowMask))
            {
                GameObject hitObject = hit.transform.gameObject;

                if (hitObject != pillowSelected)
                {
                    Vector3 targetPosition = hitObject.transform.position;

                    pillowSelected.transform.LeanMove(targetPosition, 1);

                    hitObject.transform.LeanMove(originalPosition, 1);

                    selected = false;
                    pillowSelected = null;

                    CheckPuzzleCompletion();
                }
            }
        }
        else
        {
            if (Physics.Raycast(ray, out RaycastHit hit, 100, pillowMask))
            {
                GameObject hitObject = hit.transform.gameObject;
                pillowSelected = hitObject;
                originalPosition = pillowSelected.transform.position;
                pillowSelected.transform.LeanMoveLocalY(1.5f, 1);
                selected = true;
            }
        }
    }

    private void CheckPuzzleCompletion()
    {
        if (ColorChange.originalColor)
        {
            for (int i = 0; i < pillows.Length; i++)
            {
                if (PuzzleMatch(pillows[i].transform.position, firstColorPositions[i].position))
                {
                    puzzleManager.CompletePuzzle(0);
                    break;
                }
            }
        }

        if (!ColorChange.originalColor)
        {
            for (int i = 0; i < pillows.Length; i++)
            {
                if (PuzzleMatch(pillows[i].transform.position, secondColorPositions[i].position))
                {
                    puzzleManager.CompletePuzzle(1);
                    break;
                }
            }
        }

        if (puzzleManager.puzzles.All((puzzle) => puzzle))
        {
            puzzleManager.FinishLevel(0);
        }
    }

    private bool PuzzleMatch(Vector3 position1, Vector3 position2)
    {
        float maxDistance = 0.1f;
        return Vector3.Distance(position1, position2) < maxDistance;
    }

    private void OnEnable()
    {
        inputManager.OnTouch += OnTouch;
    }

    private void OnDisable()
    {
        inputManager.OnTouch -= OnTouch;
    }
}
