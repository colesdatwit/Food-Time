using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;
    Animator anim;
    float horizontal;
    float vertical;

    public float runSpeed = 5.0f;
    public Camera cam;
    Vector2 mousePos;

    void Start ()
    {
        transform.eulerAngles = new Vector3 (0, 0, 0);
        body = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
    }

    void Update ()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical"); 

        if(horizontal==0&&vertical==0){     //checks to see if player is not moving IDLE
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking",false);
            anim.SetBool("isWalkingUp",false);
            anim.SetBool("isWalkingDown", false);
        }
        if(horizontal>0){       //checks to see if player is WALKING RIGHT
            gameObject.transform.localScale = new Vector3(-5,5,5);
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking",true);
            anim.SetBool("isWalkingUp",false);
            anim.SetBool("isWalkingDown", false);
        }
        if(horizontal<0){       //checks to see if player is WALKING LEFT
            gameObject.transform.localScale = new Vector3(5,5,5);
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking",true);
            anim.SetBool("isWalkingUp",false);
            anim.SetBool("isWalkingDown", false);
        }
        if(vertical>0&&horizontal==0){      //checks if player not moving horizontally and MOVING UP 
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking",false);
            anim.SetBool("isWalkingUp",true);
            anim.SetBool("isWalkingDown", false);
        }
        if(vertical<0&&horizontal==0){      //checks if player not moving horizontally and MOVING DOWN
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking",false);
            anim.SetBool("isWalkingUp",false);
            anim.SetBool("isWalkingDown", true);
        }
        
        //if(anim.GetBool("isEquipped")){     
        //  mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //

        //if(Input.GetKeyDown(KeyCode.X)){        //when e is pressed
        //    if(anim.GetBool("isEquipped"))
        //    {
        //        anim.SetBool("isEquipped", false);
        //        transform.eulerAngles = new Vector3 (0, 0, 0);
        //    }
        //    else
        //    {
        //        anim.SetBool("isEquipped", true);
        //    }
        //}
    }

    private void FixedUpdate()
    {  
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

        gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.y);
        // (anim.GetBool("isEquipped")){
        //  Vector2 lookDir = mousePos - body.position;
        //  float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //  body.rotation = angle;
        //


    }
}