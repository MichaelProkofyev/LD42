using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBase : MonoBehaviour {

    public System.Action<PieceBase> OnDestroyed = (p) => { };
    public Rigidbody2D rb;
    public Collider2D collider;

    public bool activated;
    public float activatedTime;

    public virtual void Start()
    {
        collider.enabled = false;
    }

    public virtual void Activated()
    {
        collider.enabled = true;
        rb.isKinematic = false;
        activated = true;
        activatedTime = Time.time;
    }
}
