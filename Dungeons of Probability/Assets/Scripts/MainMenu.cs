using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button changeLanguageButton;
    public Sprite[] changeLanguageButtonStatuses;
    public LocalizationManager LocalizationManager;
    void Awake()
    {
        LocalizationManager = (LocalizationManager)FindObjectOfType(typeof(LocalizationManager));
        UpdateLanguageButtonStatus();
    }

    void Start()
    {
        
    }
    public void ChangeLanguage()
    {
        string currentLanguage = PlayerPrefs.GetString("Language");
        if(currentLanguage == "English")
        {
            PlayerPrefs.SetString("Language", "Russian");
        }
        else
        {
            PlayerPrefs.SetString("Language", "English");
        }
        LocalizationManager.LoadLocalizedText(PlayerPrefs.GetString("Language")+".json");
        UpdateLanguageButtonStatus();
    }
    
    void UpdateLanguageButtonStatus()
    {
        string currentLanguage = PlayerPrefs.GetString("Language");
        if(currentLanguage == "Russian")
        {
            changeLanguageButton.GetComponent<Image>().sprite = changeLanguageButtonStatuses[0];
        }
        else
        {
            changeLanguageButton.GetComponent<Image>().sprite = changeLanguageButtonStatuses[1];
        }
    }
}
