using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public GameObject pieceB;
    public GameObject pieceC;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PuzzlePieceA")) // Tag Piece A
        {
            pieceB.SetActive(true);
            pieceC.SetActive(true);

            Debug.Log("Puzzle started!");
            gameObject.SetActive(false); // Optional: disable this trigger
        }
    }
}
