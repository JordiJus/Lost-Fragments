using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PiecePlacementZone : MonoBehaviour
{
    public string requiredTag = "PuzzlePieceA"; // Match for this zone
    public Transform snapTarget; // Where to place the piece exactly
    public PuzzleManager puzzleManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(requiredTag))
        {
            // Snap the piece to the correct position
            other.transform.position = snapTarget.position;
            other.transform.rotation = snapTarget.rotation;

            // Optionally disable physics or XR grab
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;

            XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null) grab.enabled = false;

            puzzleManager.ReportPiecePlaced(requiredTag);

            Debug.Log($"Piece {requiredTag} placed!");
            gameObject.SetActive(false); // prevent re-triggering
        }
    }
}
