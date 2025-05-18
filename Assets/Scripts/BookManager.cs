using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public List<BookPage> pages;

    public PageUI leftPageUI;
    public PageUI rightPageUI;
    public int currentPage = 0;

    private List<BookPage> runtimePages = new();
    public AudioSource pageSound;
    public AudioClip writeSound;

    void Start()
    {
        // Create a runtime-safe copy of each page
        foreach (BookPage original in pages)
        {
            BookPage copy = ScriptableObject.Instantiate(original);
            copy.targetChoiceIndex = -1; // reset choice
            runtimePages.Add(copy);
        }

        UpdatePages();
    }

    public void FlipForward()
    {
        if (currentPage + 2 < runtimePages.Count)
        {
            currentPage += 2;
            UpdatePages();
        }
    }

    public void FlipBack()
    {
        if (currentPage - 2 >= 0)
        {
            currentPage -= 2;
            UpdatePages();
        }
    }

    public void JumpToPage(int index)
    {
        if (index >= 0 && index < runtimePages.Count)
        {
            currentPage = index - (index % 2); // ensure left page is even
            UpdatePages();
        }
    }

    void UpdatePages()
    {
        leftPageUI.DisplayPage(runtimePages[currentPage]);

        if (currentPage + 1 < runtimePages.Count)
            rightPageUI.DisplayPage(runtimePages[currentPage + 1]);
        else
            rightPageUI.DisplayBlank();
    }
    
    public void UnlockPage(int index)
    {
        
        pageSound.PlayOneShot(writeSound);
        
        if (index >= 0 && index < runtimePages.Count)
        {
            runtimePages[index].isLocked = false;
            Debug.Log($"Page {index} unlocked!");
            UpdatePages();
        }
    }
}