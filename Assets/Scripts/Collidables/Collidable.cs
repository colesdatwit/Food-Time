using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collidable : MonoBehaviour
{
    private Collider2D coll;
    [SerializeField]
    private ContactFilter2D filt;
    private List<Collider2D> collObjs = new List<Collider2D>(1);

    protected virtual void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        coll.OverlapCollider(filt, collObjs);
        foreach(var o in collObjs)
        {
            OnCollided(o.gameObject);
        }
    }

    protected abstract void OnCollided(GameObject collidedObject);

}