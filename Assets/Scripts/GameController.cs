using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] private Text score;

	// Use this for initialization
	void Start () {
        StartCoroutine("UpdateScore");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator UpdateScore()
    {
        while (true)
        {
            score.text = Random.Range(0, 51000).ToString();
            yield return new WaitForSeconds(.1f);
        }

    }
}
