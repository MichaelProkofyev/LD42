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


public class Contstruction : MonoBehaviour {

    public float pieceWidth = .5f;

    public int currentLeftPieceIndex;
    public int currentRightPieceIndex;

    [SerializeField] private PieceBase[] availablePiecePrefabs = null;
    [SerializeField] private Bijouteriya bijouteriaSkeletonPrefab;
    [SerializeField] private Transform previewParentLeft;
    [SerializeField] private Transform previewParentRight;

    PieceBase previewPieceLeft;
    PieceBase previewPieceRight;

    public List<PieceBase> spawnedPieces = new List<PieceBase>();
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
            spawnedPieces = new List<PieceBase>();
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
        Vector3 piecePosition = transform.position + Vector3.right * xOffset;

        PieceBase piece = leftDirection ? previewPieceLeft : previewPieceRight;

        //Clear the prieviews
        if (leftDirection)
        {
            Destroy(previewPieceRight.rb.gameObject);
        }
        else
        {
            Destroy(previewPieceLeft.rb.gameObject);
        }
        piece.rb.transform.position = piecePosition;
        piece.rb.transform.parent = transform;

        if (leftDirection)
        {
            spawnedPieces.Insert(0, piece);
        }
        else
        {
            spawnedPieces.Add(piece);
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

        SpawnPiece(true);
        SpawnPiece(false);
    }

    void SpawnPiece(bool isLeft)
    {
        var parentTransform = isLeft ? previewParentLeft : previewParentRight;
        int pieceIndex = isLeft ? currentLeftPieceIndex : currentRightPieceIndex;
        var selectedPiecePrefab = availablePiecePrefabs[pieceIndex];
        var newPiece = Instantiate(selectedPiecePrefab, parentTransform.position, Quaternion.identity);
        newPiece.transform.parent = parentTransform;
        float scaleX = Random.Range(pieceWidth / 2f, pieceWidth * 2);
        newPiece.transform.localScale = new Vector3(pieceWidth, pieceWidth, 1);
        newPiece.rb.isKinematic = true;

        if (isLeft)
        {
            previewPieceLeft = newPiece;
        }
        else
        {
            previewPieceRight = newPiece;
        }
    }
}
