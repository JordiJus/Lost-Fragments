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
            if (pageImage != null) pageImage.sprite = null;
            choicePanel.SetActive(false);
            if (choiceText != null) choiceText.gameObject.SetActive(false);
            return;
        }
        
        pageText.text = "";
        choiceText.text = "";
        if (pageImage != null) pageImage.sprite = null;
        

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
                    page.targetChoiceIndex = choiceIndex;
                    choiceText.text = "";
                    ShowChoiceResult();
                });
            }
        }
        else
        {
            // Show result content
            ShowChoiceResult();
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
