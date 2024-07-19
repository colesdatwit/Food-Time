using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodServingCounter : Counter
{
    public QueueManager queueManager; // Reference to the QueueManager script

    Animator anim;

    protected override void OnInteract(GameObject player)
    {
        anim = GetComponent<Animator>();
        PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            Debug.Log("b");
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                // "E" on counter with food; player with heldFoodDebug.Log
                Debug.Log("e");
                if (foodOnCounter != null && playerMovement.HasFood())
                {
                    Debug.Log($"Already {foodOnCounter.name} on the counter! theres no dang space!");
                    return;
                }
                // "E" on counter with no food; player with heldFood
                if (foodOnCounter == null && playerMovement.HasFood())
                { 
                    anim.Play("ServingCounterRing");
                    ServeFood(playerMovement);
                    return;
                }
                // "E" on counter with food; player with no heldFood
                else if (foodOnCounter != null)
                {
                    PickUpFood(playerMovement);
                    return;
                }
                Debug.Log($"buddy! get ur eyes checked! theres nothin on that dang counter");
            }
        }
    }

    protected void ServeFood(PlayerMovement player)
    {
        //foodObject.GetComponent<SpriteRenderer>().sprite=player.heldFood.foodSprite;
        foodOnCounter = player.PlaceFood();
        //SoundPlayer.GetComponent<SoundPlayer>().PlayPlaceFood();
        Debug.Log($"Placed {foodOnCounter.name} on the counter.");
    }

    public void RemoveServedFood()
    {
        if (foodOnCounter != null)
        {
            foodOnCounter = null;
        }
    }
}