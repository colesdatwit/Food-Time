using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : Interactable
{
    private Food foodOnCounter;

    bool mixingCounter;
    bool workingCounter;
    protected override void OnInteract(GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                // "E" on counter with food; player with heldFood
                if (foodOnCounter != null && playerMovement.HasFood())
                {
                    Debug.Log($"Already {foodOnCounter.name} on the counter! theres no dang space!");
                    return;
                }
                // "E" on counter with no food; player with heldFood
                if (foodOnCounter == null && playerMovement.HasFood())
                { 
                    PlaceFood(playerMovement);
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

    // responsible for adding food to counter
    private void PlaceFood(PlayerMovement player)
    {
        foodOnCounter = player.PlaceFood();

        Debug.Log($"Placed {foodOnCounter.name} on the counter.");
    }

    // responsible for food removeal from counter
    private void PickUpFood(PlayerMovement player)
    {
        if (player.PickUpFood(foodOnCounter))
        {
            Debug.Log($"Picked up {foodOnCounter.name} from the counter.");
            foodOnCounter = null;
        }
        else if (!player.PickUpFood(foodOnCounter))
        {
            Debug.Log($"Cannot pick up {foodOnCounter.name}, likely due to full hands.");
        }
    }
}