 using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public delegate void InteractionDelegate(GameObject player);

public class Interactable : Collidable
{
    public event InteractionDelegate OnInteractEvent;
    

    protected override void OnCollided(GameObject collidedObject)
    {
        if(collidedObject.CompareTag("PlayerCollider")&&!GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>().getIsPaused())
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Escape))
            {
                OnInteract(collidedObject);
            }
        }

    }

    protected virtual void OnInteract(GameObject player)
    {
        if (OnInteractEvent != null)
        {
            OnInteractEvent.Invoke(player);
        }
    }
}
