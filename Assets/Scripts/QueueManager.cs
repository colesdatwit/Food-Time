using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueManager : MonoBehaviour
{
    public GameObject npcPrefab; // Prefab of the NPC, assign 'customer' prefab here
    public GameObject SoundPlayer;
    public Transform[] queuePositions; // Positions in the queue, assign in the Inspector
    public float speed; // Movement speed of NPCs
    public Text messageBox; // Reference to the UI Text element, assign in the Inspector
    public FoodServingCounter servingCounter; // Reference to the ServingCounter

    private List<GameObject> npcQueue = new List<GameObject>(); // List to store NPCs in the queue
    private List<string> orders = new List<string>() { "RicePortion", "TomatoPortion", "FishPortion" }; // Possible orders
    private string currentOrder = null; // Track the current order
    public int Score = 0;
    public string language = "Spanish";
    Dictionary<string, string> Level1 = new Dictionary<string, string>()
    {
        {"I would like some Pizza please", "BakedPizza"},
        {"I would like some Sushi please", "Sushi"},
        {"I would like some Cooked Rice please", "CookedRice"},
        {"I would like some Curry please", "CookedCurry"}
    };

    Dictionary<string, string> Level2Spanish = new Dictionary<string, string>()
    {
        {"Me da una Pizza please", "BakedPizza"},
        {"I would like some sushi por favor", "Sushi"},
        {"Me da un Cooked Rice please", "CookedRice"},
        {"I would like some Curry por favor", "CookedCurry"}
    };

    Dictionary<string, string> Level3Spanish = new Dictionary<string, string>()
    {
        {"Me da una pizza por favor", "BakedPizza"},
        {"Me da un sushi por favor", "Sushi"},
        {"Me da un arroz cocido por favor", "CookedRice"},
        {"Me da un curry por favor", "CookedCurry"},
        {"Tráeme un poco de pizza por favor", "BakedPizza"},
        {"¡Quiero curry por favor!", "CookedCurry"}
    };

    Dictionary<string, string> Level2Japaneese = new Dictionary<string, string>()
    {
        {"piza onegaishimasu", "BakedPizza"},
        {"sushi onegaishimasu", "Sushi"},
        {"I would like some gohan please", "CookedRice"},
        {"I would like some karee please", "CookedCurry"}
    };

    Dictionary<string, string> Level3Japaneese = new Dictionary<string, string>()
    {
        {"piza onegaishimasu", "BakedPizza"},
        {"sushi onegaishimasu", "Sushi"},
        {"gohan onegaishimasu", "CookedRice"},
        {"karee onegaishimasu", "CookedCurry"}
    };

    Dictionary<string, string> Level2Portuguese = new Dictionary<string, string>()
    {
        {"I would like some Pizza por favor", "BakedPizza"},
        {"I would like some Sushi por favor", "Sushi"},
        {"Por favor, posso comer Cooked Rice please", "CookedRice"},
        {"Eu preciso de um pouco de Curry please", "CookedCurry"}
    };

    Dictionary<string, string> Level3Portuguese = new Dictionary<string, string>()
    {
        {"Eu gostaria de um pouco de pizza, por favor", "BakedPizza"},
        {"Por favor, posso comer sushi", "Sushi"},
        {"Arroz cozido para mim", "CookedRice"},
        {"Um pedido de curry, por favor", "CookedCurry"}
    };

    Dictionary<string, string> Level2French = new Dictionary<string, string>()
    {
        {"I would like some Pizza please", "BakedPizza"},
        {"I would like some Sushi please", "Sushi"},
        {"I would like some Cooked Rice please", "CookedRice"},
        {"I would like some Curry please", "CookedCurry"}
    };

    Dictionary<string, string> Level3French = new Dictionary<string, string>()
    {
        {"I would like some Pizza please", "BakedPizza"},
        {"I would like some Sushi please", "Sushi"},
        {"I would like some Cooked Rice please", "CookedRice"},
        {"I would like some Curry please", "CookedCurry"}
    };
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
                    if (currentOrder == null) // Only set a new order if there is no current order
                    {
                        KeyValuePair<string, string> orderEntry = GetRandomOrder(Score, language);
                        currentOrder = orderEntry.Value;
                        DisplayMessage(orderEntry.Key);
                    }

                    Food preparedFood = servingCounter.GetFood(); // Assume GetFood returns a Food object
                    if (preparedFood != null && preparedFood.foodId != null) // Check if preparedFood and foodId are not null
                    {
                        if (preparedFood.foodId.Equals(currentOrder))
                        {
                            Debug.LogError(preparedFood.foodId + ' ' + currentOrder);
                            //DisplayMessage("NPC has reached the start of the line and got their " + currentOrder + "!");
                            SoundPlayer.GetComponent<SoundPlayer>().PlayCorrect();
                            OnNPCLeave(); // Correct order, NPC leaves
                            servingCounter.RemoveFood();
                            currentOrder = null; // Reset the order as it's completed
                        }
                        else
                        {
                            //SoundPlayer.GetComponent<SoundPlayer>().PlayWrong();  //too loud don't like
                            DisplayMessage("Wrong order! I asked for " + currentOrder + ", not " + preparedFood.foodId + ".");
                        }
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
            Score++;
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
    private KeyValuePair<string, string> GetRandomOrder(int Scores, string lang)
    {
        if (Scores < 3 || lang.Equals("English")){
            List<KeyValuePair<string, string>> entries = new List<KeyValuePair<string, string>>(Level1);
            int randomIndex = UnityEngine.Random.Range(0, entries.Count);
            return entries[randomIndex]; 
        }
        else{
            if(lang.Equals("Spanish"))
            {
                if(Scores < 6){
                List<KeyValuePair<string, string>> entries = new List<KeyValuePair<string, string>>(Level2Spanish);
                int randomIndex = UnityEngine.Random.Range(0, entries.Count);
                return entries[randomIndex];
                } else{
                    List<KeyValuePair<string, string>> entries = new List<KeyValuePair<string, string>>(Level3Spanish);
                    int randomIndex = UnityEngine.Random.Range(0, entries.Count);
                    return entries[randomIndex];
                }
            }
            else if(lang.Equals("French"))
            {
                if(Scores < 6){
                List<KeyValuePair<string, string>> entries = new List<KeyValuePair<string, string>>(Level2French);
                int randomIndex = UnityEngine.Random.Range(0, entries.Count);
                return entries[randomIndex];
                } else{
                    List<KeyValuePair<string, string>> entries = new List<KeyValuePair<string, string>>(Level3French);
                    int randomIndex = UnityEngine.Random.Range(0, entries.Count);
                    return entries[randomIndex];
                }
            }
            else if(lang.Equals("Portuguese"))
            {
                if(Scores < 6){
                List<KeyValuePair<string, string>> entries = new List<KeyValuePair<string, string>>(Level2Portuguese);
                int randomIndex = UnityEngine.Random.Range(0, entries.Count);
                return entries[randomIndex];
                } else{
                    List<KeyValuePair<string, string>> entries = new List<KeyValuePair<string, string>>(Level3Portuguese);
                    int randomIndex = UnityEngine.Random.Range(0, entries.Count);
                    return entries[randomIndex];
                }
            }
            else if(lang.Equals("Japanese"))
            {
                if(Scores < 6){
                List<KeyValuePair<string, string>> entries = new List<KeyValuePair<string, string>>(Level2Japaneese);
                int randomIndex = UnityEngine.Random.Range(0, entries.Count);
                return entries[randomIndex];
                } else{
                    List<KeyValuePair<string, string>> entries = new List<KeyValuePair<string, string>>(Level3Japaneese);
                    int randomIndex = UnityEngine.Random.Range(0, entries.Count);
                    return entries[randomIndex];
                }
            }
        }
        return new KeyValuePair<string, string>("No order", "None");
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