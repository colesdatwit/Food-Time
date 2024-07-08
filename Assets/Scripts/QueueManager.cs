using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueManager : MonoBehaviour
{
    public GameObject npcPrefab; // Prefab of the NPC, assign 'customer' prefab here
    public Transform[] queuePositions; // Positions in the queue, assign in the Inspector
    public float speed; // Movement speed of NPCs
    public Text messageBox; // Reference to the UI Text element, assign in the Inspector
    public FoodServingCounter servingCounter; // Reference to the ServingCounter

    private List<GameObject> npcQueue = new List<GameObject>(); // List to store NPCs in the queue
    private List<string> orders = new List<string>() { "Rice", "Rice", "Rice" }; // Possible orders

    void Start()
    {
        for (int i = 0; i < queuePositions.Length; i++)
        {
            SpawnNPC(i);
        }
        messageBox.gameObject.SetActive(false); // Hide the message box initially
    }

    void Update()
    {
        for (int i = 0; i < npcQueue.Count; i++)
        {
            GameObject npc = npcQueue[i];
            Vector3 targetPosition = queuePositions[i].position;
            npc.transform.position = Vector3.MoveTowards(npc.transform.position, targetPosition, speed * Time.deltaTime);

            if (i == 0 && Vector3.Distance(npc.transform.position, targetPosition) < 0.1f)
            {
                if (servingCounter != null) // Check if servingCounter is not null
                {
                    string order = GetRandomOrder();
                    string preparedFood = servingCounter.GetFood().foodId;
                    if (preparedFood.Equals(order))
                    {
                        DisplayMessage("NPC has reached the start of the line and got their " + order + "!");
                        OnNPCLeave(); // Correct order, NPC leaves
                    }
                    else
                    {
                        DisplayMessage("Wrong order! I asked for " + order + ", not " + preparedFood + ".");
                    }
                }
                else
                {
                    Debug.LogError("ServingCounter not initialized");
                }
            }
        }
    }

    public void OnNPCLeave()
    {
        if (npcQueue.Count > 0)
        {
            GameObject leavingNPC = npcQueue[0];
            npcQueue.RemoveAt(0);
            Destroy(leavingNPC);

            for (int i = 0; i < npcQueue.Count; i++)
            {
                GameObject npc = npcQueue[i];
                Customer npcScript = npc.GetComponent<Customer>();
                if (npcScript != null)
                {
                    npcScript.SetTargetPosition(queuePositions[i].position);
                }
            }
            SpawnNPC(queuePositions.Length - 1);
        }
    }

    private void SpawnNPC(int positionIndex)
    {
        if (positionIndex >= 0 && positionIndex < queuePositions.Length)
        {
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

    // Returns a random order from the list
    private string GetRandomOrder()
    {
        return orders[Random.Range(0, orders.Count)];
    }
}

public class ServingCounter
{
    public string GetFood()
    {
        // Return the name of the food that has been prepared, implement your logic here
        return "Pizza"; // Example hardcoded return
    }
}