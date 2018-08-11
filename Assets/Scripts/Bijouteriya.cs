using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bijouteriya : MonoBehaviour {

    public List<SpawnedPiece> spawnedPieces = new List<SpawnedPiece>();

    [SerializeField] private LineRenderer thread;

    public void BeBorn(List<SpawnedPiece> spawnedPieces)
    {
        this.spawnedPieces = spawnedPieces;

        for (int index = 0; index < spawnedPieces.Count; index++)
        {
            var piece = spawnedPieces[index];
            piece.rb.transform.parent = transform;
            piece.rb.isKinematic = false;

            //Fix first and last piece
            if (index == 0 || index == spawnedPieces.Count - 1)
            {
                piece.rb.gameObject.AddComponent<FixedJoint2D>();
            }
            
            //Conect each piece with the prev one
            if (index != 0)
            {
                var newJoint = AddJoint(piece.rb.gameObject);
                newJoint.connectedBody = spawnedPieces[index - 1].rb;
            }
        }

        thread.positionCount = spawnedPieces.Count;
        thread.SetWidth(0.1f, 0.1f);
    }

    SpringJoint2D AddJoint(GameObject targetGameObject)
    {
        var newJoint = targetGameObject.AddComponent<SpringJoint2D>();
        newJoint.distance = .5f;
        newJoint.dampingRatio = .9f;
        newJoint.frequency = 5f;
        newJoint.autoConfigureDistance = false;
        return newJoint;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (thread.positionCount > 0)
        {
            var newPositions = new Vector3[spawnedPieces.Count];
            for (int i = 0; i < spawnedPieces.Count; i++)
            {
                newPositions[i] = spawnedPieces[i].rb.transform.position;
            }
            thread.SetPositions(newPositions);
        }
    }
}
