using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private Ingredients thisIngredient;
    [SerializeField] private GameObject originalParent;
    private Vector3 ingredientPosition;
    public bool inRecipe = false;

    private void Start()
    {
        ingredientPosition = transform.position;
    }

    public GameObject Parent
    {
        get { return originalParent; } 
    }
    public Ingredients ObjectIngredient
    {
        get { return thisIngredient; }
    }
    public Vector3 IngredientPosition
    {
        get { return ingredientPosition; }
    }

}
