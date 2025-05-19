using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToolGrabNotifier : MonoBehaviour
{
    public ToolbeltManager manager;

    void Awake()
    {
        XRGrabInteractable grab = GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            grab.selectEntered.AddListener(OnGrabbed);
        }
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        if (manager != null)
        {
            manager.OnToolTaken(gameObject);
        }
    }
}