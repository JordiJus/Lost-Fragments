using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class BookInputListener : MonoBehaviour
{
    public BookManager bookManager;
    public InputActionReference pageBackAction;
    public InputActionReference pageForwardAction;

    private void OnEnable()
    {
        if (pageBackAction != null)
            pageBackAction.action.performed += OnPageBack;

        if (pageForwardAction != null)
            pageForwardAction.action.performed += OnPageForward;
    }

    private void OnDisable()
    {
        if (pageBackAction != null)
            pageBackAction.action.performed -= OnPageBack;

        if (pageForwardAction != null)
            pageForwardAction.action.performed -= OnPageForward;
    }

    void OnPageBack(InputAction.CallbackContext ctx)
    {
        Debug.Log("Page Back");
        bookManager.FlipBack();
    }

    void OnPageForward(InputAction.CallbackContext ctx)
    {
        Debug.Log("Page Forward");
        bookManager.FlipForward();
    }
}
