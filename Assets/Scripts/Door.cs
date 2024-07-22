using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator anim;
    public bool knocking = false;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void DoorOpen()
    {
        anim.Play("Open");
    }

    public void DoorKnock()
    {
        anim.Play("Knock");
    }
}
