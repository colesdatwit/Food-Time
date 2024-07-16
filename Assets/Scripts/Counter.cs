using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Counter : Interactable
{
    public Food foodOnCounter;
    public FoodDatabase foodDatabase; // Reference to the Food Database

    public GameObject SoundPlayer;

    public GameObject foodObjectSpawner;
    GameObject foodObject;
    
    public bool isVertical;
    public bool isMixingCounter;
    public bool isWorkingCounter;
    public bool isOven;
    public bool isStove;
    public bool isGarbage;
    public bool hasReservoir = false;

    bool firstInteract = true;

    protected override void OnInteract(GameObject playerCollider)
    {
        PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                if (hasReservoir) return;
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
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log($"pressed R on {GetType()}");

                if (isMixingCounter) Mix(playerMovement);
                if (isWorkingCounter) Work();
                if (isOven) Bake();
                if (isStove) Cook();
            }
        }
    }

    // responsible for adding food to counter
    protected void PlaceFood(PlayerMovement player)
    {
        if(firstInteract)
        {
            if(isVertical)
                foodObject = Instantiate(foodObjectSpawner,new Vector3(transform.position.x,transform.position.y,transform.position.z-1),Quaternion.identity);
            else
                foodObject = Instantiate(foodObjectSpawner,new Vector3(transform.position.x,transform.position.y+(float)0.4,transform.position.z-1),Quaternion.identity);
            firstInteract=false;
        }
        if (!isGarbage){
            foodObject.GetComponent<SpriteRenderer>().sprite=player.heldFood.foodSprite;
            foodOnCounter = player.PlaceFood();
            SoundPlayer.GetComponent<SoundPlayer>().PlayPlaceFood();
            Debug.Log($"Placed {foodOnCounter.name} on the counter.");
        }
        else{
            SoundPlayer.GetComponent<SoundPlayer>().PlayTrashFood();
            Debug.Log($"Placed {player.heldFood.name} in the garbage.");
            player.PlaceFood();
        }
    }

    public void RemoveFood()
    {
        if (foodOnCounter != null)
        {
            foodOnCounter = null;
            foodObject.GetComponent<SpriteRenderer>().sprite = null;
        }
    }

    // responsible for food removeal from counter
    protected void PickUpFood(PlayerMovement player)
    {
        if (player.PickUpFood(foodOnCounter))
        {
            SoundPlayer.GetComponent<SoundPlayer>().PlayPickUpFood();
            Debug.Log($"Picked up {foodOnCounter.name} from the counter.");
            foodOnCounter = null;
            foodObject.GetComponent<SpriteRenderer>().sprite=null;
        }
        else if (!player.PickUpFood(foodOnCounter))
        {
            Debug.Log($"Cannot pick up {foodOnCounter.name}, likely due to full hands.");
        }
    }

    public Food GetFood()
    {
        return foodOnCounter;
    }

    public string GetType()
    {
        if (isMixingCounter) return "Mix";
        if (isWorkingCounter) return "Work";
        if (isOven) return "Oven";
        if (isGarbage) return "Garbage";
        return "Normal";
    }


    private void Mix(PlayerMovement player)
    {
        if (foodOnCounter != null && foodOnCounter.isMixable)
        {
            Food heldFood = player.getFood();
            if (heldFood != null)
            {
                for (int i = 0; i < foodOnCounter.mixableFoodsIds.Count; i++)
                {
                    if (heldFood.foodId == foodOnCounter.mixableFoodsIds[i])
                    {
                        if (heldFood.foodId == foodOnCounter.mixableFoodsIds[i])
                        {
                            var newFood = foodDatabase.GetFoodById(foodOnCounter.mixEvolveId[i]);
                            if (newFood != null)
                            {
                                foodOnCounter = newFood;
                                foodObject.GetComponent<SpriteRenderer>().sprite = newFood.foodSprite;
                                player.heldFood = null;
                                player.foodObject.GetComponent<SpriteRenderer>().sprite = null;
                                SoundPlayer.GetComponent<SoundPlayer>().PlayMixFood();
                                Debug.Log($"Mixed and evolved into {foodOnCounter.name}");
                                player.Mixed();
                                foodObject.GetComponent<SpriteRenderer>().sprite = newFood.foodSprite;
                                return;
                            }
                        }
                    }
                }
                Debug.Log("Held food is not mixable with the food on the counter.");
            }
            else
            {
                Debug.Log("Player is not holding any food to mix.");
            }
        }
        else
        {
            Debug.Log("Food on the counter is not mixable.");
        }
    }

    private void Work()
    {
        if (foodOnCounter != null && foodOnCounter.isWorkable)
        {
            Debug.Log($"entered work");

            // Example working logic: get the work evolution ID and fetch the new food object
            var newFood = foodDatabase.GetFoodById(foodOnCounter.workEvolveId);
            if (newFood != null)
            {
                foodOnCounter = newFood;
                foodObject.GetComponent<SpriteRenderer>().sprite = newFood.foodSprite;
                SoundPlayer.GetComponent<SoundPlayer>().PlayWorkFood();
                Debug.Log($"Worked and evolved into {foodOnCounter.name}");
            }
            else
            {
                Debug.Log("BakeEvolveId not found in the Food Database");
            }
        }
    }

    private void Bake()
    {
        if (foodOnCounter != null && foodOnCounter.isBakeable)
        {
            // Example baking logic: get the bake evolution ID and fetch the new food object
            var newFood = foodDatabase.GetFoodById(foodOnCounter.bakeEvolveId);
            if (newFood != null)
            {
                foodOnCounter = newFood;
                foodObject.GetComponent<SpriteRenderer>().sprite = newFood.foodSprite;
                SoundPlayer.GetComponent<SoundPlayer>().PlayCookFood();
                Debug.Log($"Baked and evolved into {foodOnCounter.name}");
            }
            else
            {
                Debug.Log("BakeEvolveId not found in the Food Database");
            }
        }
    }

    private void Cook()
    {
        if (foodOnCounter != null && foodOnCounter.isCookable)
        {
            // Example cooking logic: get the cook evolution ID and fetch the new food object
            var newFood = foodDatabase.GetFoodById(foodOnCounter.cookEvolveId);
            if (newFood != null)
            {
                foodOnCounter = newFood;
                foodObject.GetComponent<SpriteRenderer>().sprite = newFood.foodSprite;
                SoundPlayer.GetComponent<SoundPlayer>().PlayCookFood();
                Debug.Log($"Cooked and evolved into {foodOnCounter.name}");
            }
            else
            {
                Debug.Log("CookEvolveId not found in the Food Database");
            }
        }
    }

    private void Delete(Food food)
    {
        if (food != null)
        {
            Debug.Log($"Deleted {food.name} from the counter.");
            foodOnCounter = null;
        }
    }

}