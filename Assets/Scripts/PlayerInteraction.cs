using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public QueueManager queueManager; // Reference to the QueueManager script
    public KeyCode interactKey = KeyCode.E; // Key for interaction
    private bool isNearCounter = false; // Flag to check if the player is near the counter

    void Update()
    {
        if (isNearCounter && Input.GetKeyDown(interactKey))
        {
            queueManager.OnNPCLeave();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
        if (other.CompareTag("Counter"))
        {
            isNearCounter = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Counter"))
        {
            isNearCounter = false;
        }
    }
}