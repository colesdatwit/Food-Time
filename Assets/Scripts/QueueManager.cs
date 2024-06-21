using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
public class QueueManager : MonoBehaviour
{
    public GameObject npcPrefab; // Prefab of the NPC, assign 'customer' prefab here
    public Transform[] queuePositions; // Positions in the queue, assign in the Inspector
    public float speed; // Movement speed of NPCs
    public Text messageBox; // Reference to the UI Text element, assign in the Inspector

    private List<GameObject> npcQueue = new List<GameObject>(); // List to store NPCs in the queue

    void Start()
    {
        // Initialize the queue with NPCs
        for (int i = 0; i < queuePositions.Length; i++)
        {
            SpawnNPC(i);
        }
        messageBox.gameObject.SetActive(false); // Hide the message box initially
    }

    void Update()
    {
        // Move each NPC towards its target position
        for (int i = 0; i < npcQueue.Count; i++)
        {
            GameObject npc = npcQueue[i];
            Vector3 targetPosition = queuePositions[i].position;
            npc.transform.position = Vector3.MoveTowards(npc.transform.position, targetPosition, speed * Time.deltaTime);

            // Check if the NPC is at the start of the line
            if (i == 0 && Vector3.Distance(npc.transform.position, targetPosition) < 0.1f)
            {
                DisplayMessage("NPC has reached the start of the line!");
            }
        }
    }

    public void OnNPCLeave()
    {
        if (npcQueue.Count > 0)
        {
            // Remove the first NPC from the queue
            GameObject leavingNPC = npcQueue[0];
            npcQueue.RemoveAt(0);
            Destroy(leavingNPC);

            // Move the remaining NPCs up in the line
            for (int i = 0; i < npcQueue.Count; i++)
            {
                GameObject npc = npcQueue[i];
                Customer npcScript = npc.GetComponent<Customer>();
                if (npcScript != null)
                {
                    npcScript.SetTargetPosition(queuePositions[i].position);
                }
            }

            // Spawn a new NPC at the end of the line
            SpawnNPC(queuePositions.Length - 1);
        }
    }

    private void SpawnNPC(int positionIndex)
    {
        if (positionIndex >= 0 && positionIndex < queuePositions.Length)
        {
            Thread.Sleep(1000);
            GameObject newNPC = Instantiate(npcPrefab, queuePositions[positionIndex].position, Quaternion.identity);
            Customer npcScript = newNPC.GetComponent<Customer>();
            if (npcScript != null)
            {
                npcScript.SetTargetPosition(queuePositions[positionIndex].position);
                npcQueue.Add(newNPC);
            }
            else
            {
                Debug.LogError("The NPC prefab does not have a Customer script attached.");
                Destroy(newNPC);
            }
        }
    }

    private void DisplayMessage(string message)
    {
        if (messageBox != null)
        {
            messageBox.text = message;
            messageBox.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Message box is not assigned.");
        }
    }
}