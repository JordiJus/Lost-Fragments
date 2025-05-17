using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PuzzleManager : MonoBehaviour
{
    public GameObject finalSword;
    public GameObject puzzlePieceA;
    public GameObject puzzlePieceB;
    public GameObject puzzlePieceC;
    public Transform spawnPoint;

    private int piecesPlaced = 0;
    private int totalPieces = 3;

    public void ReportPiecePlaced(string tag)
    {
        piecesPlaced++;
        if (piecesPlaced >= totalPieces)
        {
            CompletePuzzle();
        }
    }

    private void CompletePuzzle()
    {
        Debug.Log("Puzzle Complete!");

        // Remove all puzzle pieces
        Destroy(puzzlePieceA);
        Destroy(puzzlePieceB);
        Destroy(puzzlePieceC);

        // Instantiate final model
        finalSword.SetActive(true);
    }
}
