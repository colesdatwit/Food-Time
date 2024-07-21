using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;
    Animator anim;
    float horizontal;
    float vertical;

    public Food heldFood;

    public GameObject foodObjectSpawner;
    public GameObject foodObject;
    public GameObject pauseMenu;
    public GameObject collider;

    public float runSpeed = 5.0f;
    public Camera cam;
    Vector2 mousePos;

    void Start ()
    {
        transform.eulerAngles = new Vector3 (0, 0, 0);
        body = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
        foodObject = Instantiate(foodObjectSpawner,transform.position,Quaternion.identity);
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
    }

    void Update ()
    {
        if(!pauseMenu.GetComponent<PauseMenu>().getIsPaused())
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            if (horizontal == 0 && vertical == 0)
            {     //checks to see if player is not moving IDLE
                anim.SetBool("isIdle", true);
                anim.SetBool("isWalking", false);
                anim.SetBool("isWalkingUp", false);
                anim.SetBool("isWalkingDown", false);
            }
            if (horizontal > 0)
            {       //checks to see if player is WALKING RIGHT
                gameObject.transform.localScale = new Vector3(-5, 5, 5);
                anim.SetBool("isIdle", false);
                anim.SetBool("isWalking", true);
                anim.SetBool("isWalkingUp", false);
                anim.SetBool("isWalkingDown", false);
                foodObject.transform.position = new Vector3(gameObject.transform.position.x+(float)0.3,gameObject.transform.position.y-(float)0.45,gameObject.transform.position.z+1);
                collider.transform.position = new Vector3(gameObject.transform.position.x+(float)0.35,gameObject.transform.position.y-(float)0.2,gameObject.transform.position.z);
            }
            if (horizontal < 0)
            {       //checks to see if player is WALKING LEFT
                gameObject.transform.localScale = new Vector3(5, 5, 5);
                anim.SetBool("isIdle", false);
                anim.SetBool("isWalking", true);
                anim.SetBool("isWalkingUp", false);
                anim.SetBool("isWalkingDown", false);
                foodObject.transform.position = new Vector3(gameObject.transform.position.x-(float)0.3,gameObject.transform.position.y-(float)0.45,gameObject.transform.position.z+1);
                collider.transform.position = new Vector3(gameObject.transform.position.x-(float)0.35,gameObject.transform.position.y-(float)0.2,gameObject.transform.position.z);
            }
            if (vertical > 0 && horizontal == 0)
            {      //checks if player not moving horizontally and MOVING UP 
                anim.SetBool("isIdle", false);
                anim.SetBool("isWalking", false);
                anim.SetBool("isWalkingUp", true);
                anim.SetBool("isWalkingDown", false);
                foodObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+(float)0.5,gameObject.transform.position.z+1);
                collider.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+(float)0.3,gameObject.transform.position.z);
            }
            if (vertical < 0 && horizontal == 0)
            {      //checks if player not moving horizontally and MOVING DOWN
                anim.SetBool("isIdle", false);
                anim.SetBool("isWalking", false);
                anim.SetBool("isWalkingUp", false);
                anim.SetBool("isWalkingDown", true);
                foodObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y-(float)0.55,gameObject.transform.position.z-1);
                collider.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y-(float)0.7,gameObject.transform.position.z);
            }

            anim.SetBool("isHolding", HasFood());
        }
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        if (horizontal != 0 && vertical != 0) body.velocity = body.velocity.normalized * runSpeed;

        gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.y);
    }

    // Pick up food from the food reservoir
    public bool PickUpFood(Reservoir foodRes)
    {
        if (heldFood != null)
        {
            Debug.Log("Cannot pickup " + foodRes.name + ". Your hands are full!");
            return false;
        }
        //foodRes.PickUpFood();

        Debug.Log("picked up " + foodRes.name);
        heldFood = foodRes.ReservoirFood;
        return true;
    }

    // pickup food from counter or workstation
    public bool PickUpFood(Food food)
    {
        // check if player already has food in hands
        if (heldFood != null)
        {
            // return false if hands are full
            Debug.Log("Cannot pickup " + food.name + ". Your hands are full!");
            return false;
        }

        // yipee! food in hands!
        //Debug.Log("picked up " + food.name);
        heldFood = food;
        foodObject.GetComponent<SpriteRenderer>().sprite=heldFood.foodSprite;
        return true;
    }

    public void useCounter(Counter counter)
    {
        if (counter.GetFood() != null)
        {
          //  if (counter.getType() == "Mix") counter.
          //  if (counter.getType() == "Work")
          //  if (counter.getType() == "Oven") 
        }
    }

    // Simple getter to see if player has food in hands
    public bool HasFood()
    {
        return heldFood != null;
    }

    public Food getFood()
    { 
        return heldFood; 
    }

    // Place food function, returns the heldFood
    public Food PlaceFood()
    {
        if (!HasFood()) return null;
        Food temp = heldFood;
        heldFood = null;
        foodObject.GetComponent<SpriteRenderer>().sprite=null;
        return temp;
    }

    public void Mixed()
    {
        if (heldFood != null)
        {
            heldFood = null;
            foodObject.GetComponent<SpriteRenderer>().sprite = null;
        }
        
    }

}