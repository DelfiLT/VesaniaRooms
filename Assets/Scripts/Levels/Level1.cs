using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private LayerMask puzzleMask;
    [SerializeField] private List<Vector3> positions;
    [SerializeField] private List<GameObject> puzzleObjects;
    [SerializeField] private List<GameObject> finalPositions;
    [SerializeField, Range(0f, 8f)] int inactivePosition;

    [Header("Visual Settings")]
    [SerializeField] private LayerMask clueMask;
    [SerializeField] private Animator noteAnimator;
    [SerializeField] private GameObject noteParticle;

    private Dictionary<Vector3, bool> positionsStatus;
    private List<Vector3> adjacentPositions = new List<Vector3>();

    private PuzzleManager puzzleManager;
    private InputManager inputManager;
    private bool isMoving = false;
    private bool bookUnlocked;

    private void Awake()
    {
        bookUnlocked = false;
        inputManager = InputManager.Instance;
        puzzleManager = GetComponent<PuzzleManager>();
    }

    void Start()
    {
        InitializePositions();
    }

    private void OnTouch(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit clockHit, 100, clueMask))
        {
            noteAnimator.SetTrigger("animationNote");
            noteParticle.SetActive(true);
        }

        PuzzlePartOne(ray);
        PuzzlePartTwo(ray);
    }

    #region PartOne

    private void PuzzlePartOne(Ray ray)
    {
        if (puzzleManager.puzzles[0])
        {
            Debug.Log("Puzzle Finalizado");
            return;
        }

        if (isMoving)
        {
            Debug.Log("Otro objeto está en movimiento. Espera a que termine.");
            return;
        }

        if (Physics.Raycast(ray, out RaycastHit hit, 100, puzzleMask))
        {
            Vector3 hitPosition = hit.transform.localPosition;
            int index = positions.FindIndex(pos => ArePositionsClose(pos, hitPosition));

            if (index == -1)
            {
                Debug.LogWarning("No se encontró una posición cercana en la lista.");
                return;
            }

            adjacentPositions.Clear();
            List<int> offsets = new List<int> { -1, 1, -3, 3 };
            foreach (int offset in offsets)
            {
                int adjacentIndex = index + offset;
                if (adjacentIndex >= 0 && adjacentIndex < positions.Count)
                {
                    Vector3 adjacentPosition = positions[adjacentIndex];
                    adjacentPositions.Add(adjacentPosition);
                }
            }

            foreach (var pos in adjacentPositions)
            {
                if (positionsStatus.TryGetValue(pos, out bool isActive) && !isActive)
                {
                    Vector3 originalPosition = FindApproximatePosition(hit.transform.localPosition);

                    if (originalPosition == Vector3.zero)
                    {
                        Debug.LogWarning("No se encontró una posición aproximada en el diccionario.");
                        return;
                    }

                    isMoving = true;

                    hit.transform.LeanMoveLocal(pos, 1).setOnComplete(() => {
                        positionsStatus[originalPosition] = false;
                        positionsStatus[pos] = true;
                        isMoving = false;
                        CheckPuzzleCompletion();
                    });

                    break;
                }
            }
        }
    }

    private bool ArePositionsClose(Vector3 pos1, Vector3 pos2, float tolerance = 0.1f)
    {
        return Vector3.Distance(pos1, pos2) < tolerance;
    }

    private Vector3 FindApproximatePosition(Vector3 position)
    {
        foreach (var key in positionsStatus.Keys)
        {
            if (ArePositionsClose(key, position))
            {
                return key;
            }
        }
        return Vector3.zero;
    }

    private void CheckPuzzleCompletion()
    {
        for (int i = 0; i < finalPositions.Count; i++)
        {
            if (!ArePositionsClose(puzzleObjects[i].transform.localPosition, finalPositions[i].transform.localPosition))
            {
                Debug.Log("No Completado");
                return;
            }
        }
        puzzleManager.CompletePuzzle(0);
        //animación de abrir armario y caja fuerte
    }

    private void InitializePositions()
    {
        positionsStatus = new Dictionary<Vector3, bool>();

        for (int i = 0; i < positions.Count; i++)
        {
            Vector3 pos = positions.ElementAt(i);

            if (i == inactivePosition)
            {
                positionsStatus.Add(pos, false);
                continue;
            }

            positionsStatus.Add(pos, true);
        }
    }

    #endregion

    #region PartTwo
    private void PuzzlePartTwo(Ray ray)
    {
        if (!puzzleManager.puzzles[0])
        {
            Debug.Log("Parte uno no finalizada");
            return;
        }
    }

    #endregion
    private void OnEnable()
    {
        inputManager.OnTouch += OnTouch;
    }

    private void OnDisable()
    {
        inputManager.OnTouch -= OnTouch;
    }
}
