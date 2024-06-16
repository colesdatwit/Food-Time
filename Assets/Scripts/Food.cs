using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewFood", menuName = "Food")]
public class Food : ScriptableObject
{
    public string foodId;   
    public Sprite foodSprite; // For the visual representation of the food
    public int value;         // The value or amount of the food

    public bool isCookable;
    public bool isWorkable;
    public bool isMixable;

    public List<string> mixableFoods; // populated with foodId that is mixable


}