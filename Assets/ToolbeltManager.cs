using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ToolbeltManager : MonoBehaviour
{
    [Header("Toolbelt Setup")]
    public GameObject toolbeltObject;               // Visual representation of the belt
    public List<Transform> toolSlots;               // Preset positions for tools
    public List<GameObject> tools;                  // Tools that sit in the belt

    [Header("Input")]
    public InputActionReference toggleAction;       // Assign LeftHand/primaryButton

    private bool isActive = false;

    void OnEnable()
    {
        toggleAction.action.performed += ToggleToolbelt;
        toggleAction.action.Enable();
    }

    void OnDisable()
    {
        toggleAction.action.performed -= ToggleToolbelt;
        toggleAction.action.Disable();
    }

    void ToggleToolbelt(InputAction.CallbackContext ctx)
    {
        isActive = !isActive;

        if (isActive)
        {
            ShowToolbelt();
        }
        else
        {
            HideToolbelt();
        }
    }

    void ShowToolbelt()
    {
        PositionToolbelt();
        toolbeltObject.SetActive(true);

        // Reposition unheld tools to their slots
        for (int i = 0; i < tools.Count; i++)
        {
            XRGrabInteractable grab = tools[i].GetComponent<XRGrabInteractable>();
            if (grab != null && !grab.isSelected)
            {
                tools[i].transform.SetParent(toolSlots[i]);
                tools[i].transform.localPosition = Vector3.zero;
                tools[i].transform.localRotation = Quaternion.identity;

                Rigidbody rb = tools[i].GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
        }
    }

    void HideToolbelt()
    {
        toolbeltObject.SetActive(false);
        isActive = false;
    }

    void PositionToolbelt()
    {
        Camera cam = Camera.main;

        // Flatten the forward direction to horizontal only
        Vector3 flatForward = cam.transform.forward;
        flatForward.y = 0;
        flatForward.Normalize();

        
        transform.position = cam.transform.position + flatForward * 0.5f - new Vector3(0, 0.4f, 0);
        transform.rotation = Quaternion.LookRotation(flatForward) * Quaternion.Euler(0, 180f, 0);
    }

    public void OnToolTaken(GameObject tool)
    {
        if (tools.Contains(tool))
        {
            tool.transform.SetParent(null);
            HideToolbelt();
        }
    }
}