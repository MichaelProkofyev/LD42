using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBomb : PieceBase {

    public float explosionRadius = 3f;
    public float explosionForce = 10000f;

    void Start ()
    {
	    	
	}
	
	void Update ()
    {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        return;
        if (collision.gameObject.CompareTag("Floor"))
        {
            //Explode

            Vector2 explosionPos = new Vector2(transform.position.x, transform.position.y);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, explosionRadius);
            foreach (Collider2D hit in colliders)
            {
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    print("affected " + rb.gameObject.name);
                    AddExplosionForce(rb, explosionForce, explosionPos, explosionRadius, 3.0F);
                }
            }
        }
        OnDestroyed(this);
        Destroy(gameObject);
    }

    public void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        Vector3 baseForce = dir.normalized * explosionForce * wearoff;
        body.AddForce(baseForce);

        float upliftWearoff = 1 - upliftModifier / explosionRadius;
        Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
        body.AddForce(upliftForce);
    }
}
