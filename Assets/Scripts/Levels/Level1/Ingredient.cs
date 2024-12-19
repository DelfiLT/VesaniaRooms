using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private Ingredients thisIngredient;
    private Vector3 ingredientPosition;

    private void Start()
    {
        ingredientPosition = transform.position;
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
