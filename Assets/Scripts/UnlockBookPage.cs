using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class UnlockBookPage : MonoBehaviour
{
    public BookManager bookManager;
    public int pageIndexToUnlock;

    private bool unlocked = false;

    private void Awake()
    {
        // Require XRGrabInteractable
        XRGrabInteractable grab = GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            grab.selectEntered.AddListener(OnGrabbed);
        }
        else
        {
            Debug.LogWarning("UnlockBookPage: No XRGrabInteractable found on this object.");
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (unlocked) return;

        unlocked = true;
        bookManager.UnlockPage(pageIndexToUnlock);
    }
}