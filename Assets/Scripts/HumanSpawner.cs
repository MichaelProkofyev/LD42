using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner : MonoBehaviour {

    [SerializeField] private Human[] humanPrefabs = null;

    public float cooldown = 2f;


    public float minX = 0;
    public float maxX = 5;
    public float minY = 0;
    public float maxY = 5;

    void Start ()
    {
        StartCoroutine("SpawnAction");
	}

    IEnumerator SpawnAction()
    {
        while (true)
        {
            CreateHuman();
            yield return new WaitForSeconds(cooldown);
        }
    }

    void CreateHuman()
    {
        int humanIndex = Random.Range(0, humanPrefabs.Length);
        var newPrefab = humanPrefabs[humanIndex];
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        var newPosition = new Vector3(randomX, randomY, 0);
        var newHuman = Instantiate(newPrefab, newPosition, Quaternion.identity);
        Destroy(newHuman.gameObject, cooldown + .5f);
    }
}
