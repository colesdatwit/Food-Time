using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingCounter : Counter
{
    public QueueManager queueManager; // Reference to the QueueManager script

    protected override void OnInteract(GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                queueManager.OnNPCLeave();
                Debug.Log($"OnNPCLeave");
            }
        }
    }
}
