 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : Collidable
{
    private bool isInteracted = false;

    protected override void OnCollided(GameObject collidedObject)
    {
        if(collidedObject.tag=="Player"){
            if (Input.GetKey(KeyCode.E))
            {
                OnInteract();
            }
        }
        
    }

    protected virtual void OnInteract()
    {
        if (!isInteracted)
        {
            isInteracted = true;
            Debug.Log("INTERACT WITH " + name);
        }        
    }
}
