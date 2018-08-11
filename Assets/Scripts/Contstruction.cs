using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum InputDirection
{
    LEFT,
    RIGHT,
    UP,
    DOWN
}

public struct SpawnedPiece
{
    public Rigidbody2D rb;
    public bool isLeft;

    public SpawnedPiece(Rigidbody2D rb, bool isLeft)
    {
        this.rb = rb;
        this.isLeft = isLeft;
    }
}

public class Contstruction : MonoBehaviour {

    public float pieceWidth = 1f;

    public int currentLeftPieceIndex;
    public int currentRightPieceIndex;

    [SerializeField] private Rigidbody2D[] availablePiecePrefabs = null;

    public List<SpawnedPiece> spawnedPieces = new List<SpawnedPiece>();
    private int leftPiecesCount;
    private int rightPiecesCount;

	void Start ()
    {
        UpdateAvailablePieces();
	}
	
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AddPiece(true);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            AddPiece(false);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            GiveBirth();
        }
    }

    void AddPiece(bool leftDirection)
    {
        int pieceIndex = leftDirection ? currentLeftPieceIndex : currentRightPieceIndex;
        if (leftDirection)
        {
            leftPiecesCount++;
        }
        else
        {
            rightPiecesCount++;
        }
        var xOffset = pieceWidth * (leftDirection ? -leftPiecesCount : rightPiecesCount) + pieceWidth * (leftDirection ? 0.5f : -0.5f);
        var selectedPiecePrefab = availablePiecePrefabs[pieceIndex];
        Vector3 piecePosition = transform.position + Vector3.right * xOffset;
        var newPieceRb = Instantiate(selectedPiecePrefab, piecePosition, Quaternion.identity);
        newPieceRb.isKinematic = true;
        newPieceRb.transform.parent = transform;
        spawnedPieces.Add(new SpawnedPiece(newPieceRb, leftDirection));
        UpdateAvailablePieces();
    }

    void UpdateAvailablePieces ()
    {
        if (availablePiecePrefabs.Length == 0)
        {
            print("no pieces available");
            return;
        }
        currentLeftPieceIndex = Random.Range(0, availablePiecePrefabs.Length);
        do
        {
            currentRightPieceIndex = Random.Range(0, availablePiecePrefabs.Length);
        } while (currentRightPieceIndex == currentLeftPieceIndex);
    }

    void GiveBirth()
    {
        Rigidbody2D lastLeftRb = null;
        Rigidbody2D lastRightRb = null;

        for (int index = 0; index < spawnedPieces.Count; index++)
        {
            var piece = spawnedPieces[index];
            piece.rb.isKinematic = false;
            if (piece.isLeft)
            {
                if (lastLeftRb != null)
                {
                    var newHinge = piece.rb.gameObject.AddComponent<SpringJoint2D>();
                    newHinge.connectedBody = lastLeftRb;
                }
                lastLeftRb = piece.rb;
            }
            else
            {
                if (lastRightRb != null)
                {
                    var newHinge = piece.rb.gameObject.AddComponent<SpringJoint2D>();
                    newHinge.connectedBody = lastRightRb;
                }
                lastRightRb = piece.rb;
            }

        }

        spawnedPieces.Clear();
        leftPiecesCount = 0;
        rightPiecesCount = 0;
    }
}
