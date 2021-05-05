using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public UIWriter UIWriter;
    public List<Image> healthsIcon;
    public Image DeathPanel;
    public Image WinPanel;
    public Image ClickTip;
    public Image HitEffectPanel;
    public List<Image> HeroesPics;
    public Button DialogueButton;

    void Start()
    {
        for (int i = 0; i < 3 - PlayerPrefs.GetInt("Healths"); i++)
        {
            ReduceHealth(false);
        }
    }
    
    public void DisplayText(List<string> texts)
    {
        UIWriter.AddToQueue(TranslatedText(texts));
    }

    public void ReduceHealth(bool anim)
    {
        healthsIcon[healthsIcon.Count-1].gameObject.SetActive(false);
        healthsIcon.RemoveAt(healthsIcon.Count-1);
        if(anim == true){
            StartCoroutine(HitEffectPlay(0.5f));
        }
    }

    public void Fail()
    {
        DeathPanel.gameObject.SetActive(true);
        DeathPanel.gameObject.GetComponent<RandomPhrase>().GeneratePhrase();
    }

    public void Win(){
        WinPanel.gameObject.SetActive(true);
    }

    public void SetHeroesStatus(bool status){
        int count = 3;
        int level = PlayerPrefs.GetInt("Level");
        if (level >= 3){
            count += 1;
        }
        for (int i = 0; i < count; i++)
        {
            HeroesPics[i].gameObject.SetActive(status);
        }
    }

    List<string> TranslatedText(List<string> keys)
    {
        List<string> translatedText = new List<string>();
        for (int i = 0; i < keys.Count; i++)
        {
            translatedText.Add(LocalizationManager.instance.GetLocalizedValue(keys[i]));
        }
        return translatedText;
    }

    IEnumerator HitEffectPlay(float waitTime)
    {
        HitEffectPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        HitEffectPanel.gameObject.SetActive(false);
    }
}
