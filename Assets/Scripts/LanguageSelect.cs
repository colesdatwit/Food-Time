using UnityEngine;
using UnityEngine.UI;

public class LanguageSelect : MonoBehaviour
{
    public Button spanishButton;
    public Button portugueseButton;
    public Button japaneseButton;
    public Button frenchButton;
    public Canvas languageSelectCanvas; // Reference to the Canvas that holds the language selection

    private QueueManager queueManager;

    private void Start()
    {
        // Find the QueueManager instance in the scene
        queueManager = FindObjectOfType<QueueManager>();

        // Ensure all buttons are set up correctly
        if (spanishButton != null) spanishButton.onClick.AddListener(SetSpanishLanguage);
        if (portugueseButton != null) portugueseButton.onClick.AddListener(SetPortugueseLanguage);
        if (japaneseButton != null) japaneseButton.onClick.AddListener(SetJapaneseLanguage);
        if (frenchButton != null) frenchButton.onClick.AddListener(SetFrenchLanguage);
    }

    public void SetSpanishLanguage()
    {
        if (queueManager != null)
        {
            queueManager.language = "Spanish";
            Debug.Log("Language set to Spanish");
            DeactivateLanguageSelect();
        }
    }

    public void SetPortugueseLanguage()
    {
        if (queueManager != null)
        {
            queueManager.language = "Portuguese";
            Debug.Log("Language set to Portuguese");
            DeactivateLanguageSelect();
        }
    }

    public void SetJapaneseLanguage()
    {
        if (queueManager != null)
        {
            queueManager.language = "Japanese";
            Debug.Log("Language set to Japanese");
            DeactivateLanguageSelect();
        }
    }

    public void SetFrenchLanguage()
    {
        if (queueManager != null)
        {
            queueManager.language = "French";
            Debug.Log("Language set to French");
            DeactivateLanguageSelect();
        }
    }
   
    private void DeactivateLanguageSelect()
    {
        if (languageSelectCanvas != null)
        {
            languageSelectCanvas.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("LanguageSelect Canvas is not assigned.");
        }
    }
}
