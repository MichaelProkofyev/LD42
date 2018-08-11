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

    public float pieceWidth = .5f;

    public int currentLeftPieceIndex;
    public int currentRightPieceIndex;

    [SerializeField] private Rigidbody2D[] availablePiecePrefabs = null;
    [SerializeField] private Bijouteriya bijouteriaSkeletonPrefab;

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
            var newBijouteria = Instantiate(bijouteriaSkeletonPrefab, transform.position, Quaternion.identity);
            newBijouteria.BeBorn(spawnedPieces);
            spawnedPieces = new List<SpawnedPiece>();
            leftPiecesCount = 0;
            rightPiecesCount = 0;
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
        var xOffset = (pieceWidth + pieceWidth/2f) * (leftDirection ? -leftPiecesCount : rightPiecesCount) + pieceWidth * (leftDirection ? 0.5f : -0.5f);
        var selectedPiecePrefab = availablePiecePrefabs[pieceIndex];
        Vector3 piecePosition = transform.position + Vector3.right * xOffset;
        var newPieceRb = Instantiate(selectedPiecePrefab, piecePosition, Quaternion.identity);
        newPieceRb.isKinematic = true;
        newPieceRb.transform.parent = transform;
        if (leftDirection)
        {
            spawnedPieces.Insert(0, new SpawnedPiece(newPieceRb, leftDirection));
        }
        else
        {
            spawnedPieces.Add(new SpawnedPiece(newPieceRb, leftDirection));
        }
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
}
