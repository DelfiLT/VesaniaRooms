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
    [SerializeField] private Animator closetDoor;
    [SerializeField] private Animator strongboxDoor;

    private Dictionary<Vector3, bool> positionsStatus;
    private List<Vector3> adjacentPositions = new List<Vector3>();

    private PuzzleManager puzzleManager;
    private InputManager inputManager;
    private bool isMoving = false;

    int currentRecipe;
    List<Ingredients> recipeIngredients;
    List<Vector3> bowlPositions;

    private void Awake()
    {
        inputManager = InputManager.Instance;
        puzzleManager = GetComponent<PuzzleManager>();
        currentRecipe = 0;
    }

    void Start()
    {
        InitializePositions();
    }

    private void OnTouch(Ray ray)
    {
        PuzzlePartOne(ray);
        PuzzlePartTwo(ray);
    }

    #region PartOne

    private void PuzzlePartOne(Ray ray)
    {
        if (puzzleManager.puzzles[0] || isMoving)
        {
            return;
        }

        if (Physics.Raycast(ray, out RaycastHit hit, 100, puzzleMask))
        {
            Vector3 hitPosition = hit.transform.localPosition;
            int index = positions.FindIndex(pos => ArePositionsClose(pos, hitPosition));

            if (index == -1) return;

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

                    if (originalPosition == Vector3.zero) return;

                    isMoving = true;

                    hit.transform.LeanMoveLocal(pos, 0.5f).setOnComplete(() => {
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
            if (!ArePositionsClose(puzzleObjects[i].transform.localPosition, finalPositions[i].transform.localPosition)) return;
        }

        puzzleManager.CompletePuzzle(0);
        strongboxDoor.SetTrigger("strongboxDoorAnimation");
        closetDoor.SetTrigger("closetDoorAnimation");
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
            return;
        }
        if (Physics.Raycast(ray, out RaycastHit hit, 100, puzzleMask))
        {
            if(hit.collider.TryGetComponent(out Ingredient ingredientType))
            {
                
            }
            else
            {
                return;
            }
        }
    }

    public void CheckRecipe (Ingredients ingredient)
    {
        recipeIngredients.Add(ingredient);

        List<Ingredients> recipeOne = new List<Ingredients> { Ingredients.Potato, Ingredients.Cheese, Ingredients.FirstFlour };
        List<Ingredients> recipeTwo = new List<Ingredients>{ Ingredients.Tomato, Ingredients.Pasta, Ingredients.Meatballs };
        List<Ingredients> recipeThree = new List<Ingredients> { Ingredients.Eggs, Ingredients.SecondFlour, Ingredients.Milk };

        if (recipeIngredients.Count == 3) {         
            switch (currentRecipe)
            {
                case 0:
                    currentRecipe++;
                    break;
                case 1:
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }

        if(currentRecipe == 2)
        {
            puzzleManager.CompletePuzzle(1);
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
