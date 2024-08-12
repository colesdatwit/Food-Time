using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public QueueManager qm;
    private Vector3 targetPosition;
    Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Method to set the target position for the NPC
    public void SetTargetPosition(Vector3 newPosition)
    {
        if(newPosition == new Vector3(0,0,0))
        {
            Destroy(gameObject);
        }
        else
        {
            targetPosition = newPosition;
        }
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
                if(targetPosition == nextTarget)
                {
                    anim.SetBool("Idle",true);
                    anim.SetBool("WalkRight",false);
                    anim.SetBool("WalkDown",false);
                    anim.SetBool("WalkUp",false);
                }
                else
                {
                    targetPosition = nextTarget;
                }
            }
        }
        else
        {
            float distY = transform.position.y - targetPosition.y;
            float distX = transform.position.x - targetPosition.x;
            if(Math.Abs(distY) > Math.Abs(distX) )
            {
                if(distY > 0)
                {
                    anim.SetBool("WalkDown",true);
                    anim.SetBool("WalkRight",false);
                    anim.SetBool("WalkUp",false);
                    anim.SetBool("Idle",false);
                }
                else
                {
                    anim.SetBool("WalkUp",true);
                    anim.SetBool("WalkDown",false);
                    anim.SetBool("WalkRight",false);
                    anim.SetBool("Idle",false);
                }
                
            }
            else
            {
                anim.SetBool("WalkRight",true);
                anim.SetBool("WalkDown",false);
                anim.SetBool("WalkUp",false);
                anim.SetBool("Idle",false);
            }
        }
    }
}