using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RestartPlayerPosition : MonoBehaviour
{
    public InputActionProperty resetAction; // Assign: XRI LeftHand / Secondary Button

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        resetAction.action.Enable();
    }

    void Update()
    {
        if (resetAction.action.WasPerformedThisFrame())
        {
            transform.SetPositionAndRotation(initialPosition, initialRotation);
        }
    }
}
