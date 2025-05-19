using UnityEngine;
using UnityEngine.InputSystem;

public class InputDebug : MonoBehaviour
{
    public InputActionProperty triggerAction;

    void Update()
    {
        if (triggerAction.action != null && triggerAction.action.triggered)
            Debug.Log("Trigger pressed");
    }
}