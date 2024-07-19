using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public QueueManager qm;
    private Vector3 targetPosition;

    // Method to set the target position for the NPC
    public void SetTargetPosition(Vector3 newPosition)
    {
        targetPosition = newPosition;
    }

    void Update()
    {
        // Move the NPC towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
        if(transform.position == targetPosition)
        {
            Vector3 nextTarget=qm.FindNextTargetPosition(targetPosition);
            if(nextTarget == new Vector3(-1,-1,-1))
            {
                qm.openExitDoor();
                Destroy(gameObject);
            }
            else
            {
                targetPosition = nextTarget;
            }
        }
    }
}