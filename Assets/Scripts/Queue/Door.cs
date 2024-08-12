using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void DoorOpen()
    {
        anim.Play("Open");
        anim.SetBool("knock",false);
    }

    public void DoorKnock()
    {
        anim.Play("Knock");
    }

    public void setKnock(bool knock)
    {
        anim.SetBool("knock", knock);
    }

    public bool getKnock()
    {
        return anim.GetBool("knock");
    }
}
