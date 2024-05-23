using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewScene", menuName = "SceneData")]
public class SceneData : ScriptableObject
{
    [Header("Information")]
    public string sceneName;
    public string shortDescription;

    [Header("Level Specific")]
    public int puzzleQuantity;

    //Se puede añadir mas especificaciones de las escenas a medida que vayamos necesitando, ej: sonidos.
}