using UnityEngine;
using UnityEngine.UI;

public class DebugButtonClick : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("✅ XR BUTTON CLICKED!");
        });
    }
}