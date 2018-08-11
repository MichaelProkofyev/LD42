using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bijouteriya : MonoBehaviour {

    public List<PieceBase> spawnedPieces = new List<PieceBase>();

    [SerializeField] private LineRenderer thread;

    public void BeBorn(List<PieceBase> spawnedPieces)
    {
        this.spawnedPieces = spawnedPieces;

        for (int index = 0; index < spawnedPieces.Count; index++)
        {
            var piece = spawnedPieces[index];
            piece.rb.GetComponent<PieceBase>().OnDestroyed += OnPieceDestroyed;
            piece.rb.transform.parent = transform;
            piece.Activated();

            //Fix first and last piece
            if (index == 0 || index == spawnedPieces.Count - 1)
            {
                //piece.rb.gameObject.AddComponent<FixedJoint2D>();
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
        newJoint.distance = 1.5f;
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

    private void OnPieceDestroyed(PieceBase p)
    {
        spawnedPieces.Remove(p);
        thread.positionCount = spawnedPieces.Count;
    }
}
