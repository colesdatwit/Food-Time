using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterPop : MonoBehaviour
{

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void playWorkAnim(Vector3 pos)
    {
        transform.position = new Vector3(pos.x,pos.y+(float).3,transform.position.z);
        anim.Play("Work");
    }

    public void playCookAnim(Vector3 pos)
    {
        transform.position = new Vector3(pos.x,pos.y+(float).4,transform.position.z);
        anim.Play("Cook");
    }
}
