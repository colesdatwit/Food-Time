using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reservoir : Interactable
{
    public Food ReservoirFood; // Reference to the food data

    public GameObject SoundPlayer;

    protected override void OnInteract(GameObject player)
    {
        PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (playerMovement.getFood() == ReservoirFood)
                {
                    PlaceFood(playerMovement);
                    return;
                }
                if (!playerMovement.HasFood())
                {
                    PickUpFood(playerMovement);
                    return;
                }
            }
        }
    }

    private void PlaceFood(PlayerMovement player)
    {  
        player.PlaceFood();
        SoundPlayer.GetComponent<SoundPlayer>().PlayPlaceFood();
        Debug.Log($"Placed {ReservoirFood.name} back in the Reservoir.");
    }

    private void PickUpFood(PlayerMovement player)
    {
        player.PickUpFood(ReservoirFood);
        SoundPlayer.GetComponent<SoundPlayer>().PlayPickUpFood();
        Debug.Log($"Picked up {ReservoirFood.name} from the Reservoir.");
    }
}