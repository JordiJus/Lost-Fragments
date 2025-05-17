using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBookPage", menuName = "Book/Page")]
public class BookPage : ScriptableObject
{
    [TextArea(4, 10)]
    public string pageText;
    public string choiceText;
    public Sprite pageImage;

    public bool isChoicePage;

    [Tooltip("Choices to show before showing text")]
    public string[] choices;

    [Tooltip("This page will show text AFTER this choice index is picked")]
    [TextArea(3, 10)]
    public string[] choiceResults;
    public int targetChoiceIndex = -1; // -1 = not chosen yet
    public bool isLocked = false;
    public string lockedText = "To be discovered...";
}