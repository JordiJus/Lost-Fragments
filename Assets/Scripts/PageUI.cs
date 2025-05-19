using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PageUI : MonoBehaviour
{
    public TextMeshProUGUI pageText;
    public TextMeshProUGUI choiceText;
    public Image pageImage;

    public GameObject choicePanel;
    public Button choiceButtonPrefab;

    private BookManager bookManager;
    private BookPage currentPage;

    void Start()
    {
        bookManager = FindObjectOfType<BookManager>();
    }

    public void DisplayPage(BookPage page)
    {
        currentPage = page;
        if (page.isLocked)
        {
            // Show locked message only
            pageText.text = page.lockedText;
            pageImage.sprite = null;
            pageImage.color = new Color(1, 1, 1, 0); // oculta imatge
            
            choicePanel.SetActive(false);
            if (choiceText != null) choiceText.gameObject.SetActive(false);
            return;
        }
        
        pageText.text = "";
        choiceText.text = "";

        if (page.pageImage != null && pageImage != null)
        {
            pageImage.sprite = page.pageImage;
            pageImage.color = Color.white; // mostra la imatge (per si estava en transparent)
        }
        else if (pageImage != null)
        {
            pageImage.sprite = null;
            pageImage.color = new Color(1, 1, 1, 0); // oculta imatge
        }
        

        // Clear old buttons
        foreach (Transform child in choicePanel.transform)
            Destroy(child.gameObject);

        if (page.isChoicePage && page.targetChoiceIndex < 0)
        {
            // Show only choices
            choiceText.text = currentPage.choiceText;
            choicePanel.SetActive(true);
            for (int i = 0; i < page.choices.Length; i++)
            {
                int choiceIndex = i;
                Button btn = Instantiate(choiceButtonPrefab, choicePanel.transform);
                btn.GetComponentInChildren<TextMeshProUGUI>().text = page.choices[i];

                btn.onClick.AddListener(() =>
                {
                    Debug.Log("Listeners: " + btn.onClick.GetPersistentEventCount());
                    page.targetChoiceIndex = choiceIndex;
                    choiceText.text = "";
                    ShowChoiceResult();
                });
                Debug.Log("Listeners: " + btn.onClick.GetPersistentEventCount());
            }
        }
        else
        {
            if (page.isChoicePage)
            {
                // Show result content
                ShowChoiceResult();
            }
            else
            {
                pageText.text = currentPage.pageText;
            }
            
        }
    }

    void ShowChoiceResult()
    {
        choicePanel.SetActive(false);
        if (choiceText != null)
            choiceText.gameObject.SetActive(false);

        int i = currentPage.targetChoiceIndex;

        if (i < 0 || i >= currentPage.choiceResults.Length)
        {
            pageText.text = "[Invalid choice result]";
            return;
        }

        pageText.text = currentPage.choiceResults[i];

        // ✅ Show shared image if it exists
        if (pageImage != null && currentPage.pageImage != null)
        {
            pageImage.sprite = currentPage.pageImage;
        }
    }

    public void DisplayBlank()
    {
        pageText.text = "";
        choicePanel.SetActive(false);
    }
}
