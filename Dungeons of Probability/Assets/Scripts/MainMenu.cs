using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class MainMenu : MonoBehaviour
{
    public Button changeLanguageButton;
    public Button changeVolumeButton;
    public Sprite[] changeLanguageButtonStatuses;
    public Sprite[] changeVolumeButtonStatuses;
    public LocalizationManager LocalizationManager;
    public SetSound setSound;

    [DllImport("__Internal")]
    private static extern void OpenNewTab(string url);
    void Awake()
    {
        LocalizationManager = (LocalizationManager)FindObjectOfType(typeof(LocalizationManager));
        UpdateLanguageButtonStatus();
        if(PlayerPrefs.HasKey("Volume") == false){
            PlayerPrefs.SetInt("Volume", 1);
        }
        UpdateVolumeButtonStatus();
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

    public void ChangeSoundVolume(){
        int currentVolume = PlayerPrefs.GetInt("Volume");
        PlayerPrefs.SetInt("Volume", 1 - currentVolume);
        setSound.ChangeAudioListenerVolume();
        UpdateVolumeButtonStatus();
    }

    public void ExitButton(){
        OpenNewTab("https://www.gdcuffs.com");
    }

    void UpdateVolumeButtonStatus(){
        int currentVolume = PlayerPrefs.GetInt("Volume");
        changeVolumeButton.GetComponent<Image>().sprite = changeVolumeButtonStatuses[currentVolume];
    }
}
