using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour {

    public float speed = 5f;
    public bool movingRight = true;
    public float lifeTime = 5f;

    private float birthTime;

    [SerializeField] private Gradient gradient;

    // Use this for initialization
    void Start () {
        birthTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (birthTime + lifeTime < Time.time)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 translation = Vector3.right * (movingRight ? 1 : -1) * speed * Time.deltaTime;
            transform.Translate(translation);
        }
	}
}
