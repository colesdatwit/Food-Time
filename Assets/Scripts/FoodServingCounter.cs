using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodServingCounter : Counter
{
    public QueueManager queueManager; // Reference to the QueueManager script
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
        queueManager.OnNPCLeave();
        Debug.Log("OnNPCLeave");
    }
    
}