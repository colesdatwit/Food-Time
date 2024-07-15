using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
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
    }
}