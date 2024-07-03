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

    public bool isBakeable;
    public bool isCookable;
    public bool isWorkable;
    public bool isMixable;

    public List<string> mixableFoodsIds; // populated with foodId that is mixable
    public string bakeEvolveId; // populated with foodId that is mixable
    public string cookEvolveId; // populated with foodId that is mixable
    public string workEvolveId;
    public List<string> mixEvolveId; // Populated with foodId that is mix-evolved

}