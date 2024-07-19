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

    public GameObject EntryDoor;
    public GameObject ExitDoor;

    private int spawnTimer = 0;
    private int spawnTime = 3600;
    private int customerCount = 0;
    private bool[] positionFilled;
    //private List<GameObject> npcQueue = new List<GameObject>(); // List to store NPCs in the queue
    //private List<string> orders = new List<string>() { "RicePortion", "TomatoPortion", "FishPortion" }; // Possible orders
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
        //for (int i = 0; i < queuePositions.Length; i++)
        //{
        //    SpawnNPC(i);
        //}
        positionFilled = new bool[queuePositions.Length];
        for (int i = 0; i < queuePositions.Length; i++)
        {
            if(i==0)
            {
                positionFilled[i]=true;
            }
            else
            {
                positionFilled[i]=false;
            }
        }
        messageBox.gameObject.SetActive(false); // Hide the message box initially
    }

    
    void Update()
    {
        if(customerCount < 7)
        {
            if(spawnTimer == spawnTime)
            {
                SpawnNPC();
                customerCount++;
                spawnTimer = 0;
            }
            spawnTimer++;
        }
    }

    public Vector3 FindNextTargetPosition(Vector3 pos)
    {
        for (int i = 1; i < queuePositions.Length; i++)
        {
            if(pos==queuePositions[i].position)
            {
                if(positionFilled[i-1]) //pos==queuePositions[queuePositions.Length-2].position||
                {
                    if (pos==queuePositions[1].position)
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
                                    Debug.Log(preparedFood.foodId + ' ' + currentOrder);
                                    //DisplayMessage("NPC has reached the start of the line and got their " + currentOrder + "!");
                                    SoundPlayer.GetComponent<SoundPlayer>().PlayCorrect();
                                    DisplayMessage("");
                                    positionFilled[0]=false;
                                    servingCounter.RemoveServedFood();
                                    currentOrder = null; // Reset the order as it's completed
                                }
                                else
                                {
                                    SoundPlayer.GetComponent<SoundPlayer>().PlayCorrect(); 
                                    DisplayMessage("Wrong order! I asked for " + currentOrder + ", not " + preparedFood.foodId + ".");
                                    servingCounter.RemoveServedFood();
                                }
                            }
                        }
                        else
                        {
                            Debug.LogError("ServingCounter not initialized");
                        }
                    }
                    return pos;
                }
                else
                {
                    positionFilled[i]=false;
                    positionFilled[i-1]=true;
                    return queuePositions[i-1].position;
                }
            }
        }
        return new Vector3(-1,-1,-1);
    }

    public void openExitDoor()
    {
        customerCount--;
        ExitDoor.GetComponent<Door>().DoorOpen();
    }

    private void SpawnNPC()
    {
        if (!positionFilled[queuePositions.Length-1])
        {
            EntryDoor.GetComponent<Door>().DoorOpen();
            GameObject newNPC = Instantiate(npcPrefab, queuePositions[queuePositions.Length-1].position, Quaternion.identity);
            Customer npcScript = newNPC.GetComponent<Customer>();
            if (npcScript != null)
            {
                positionFilled[queuePositions.Length-1]=true;
                npcScript.qm = GameObject.FindFirstObjectByType<QueueManager>();
                npcScript.SetTargetPosition(queuePositions[queuePositions.Length-1].position);
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