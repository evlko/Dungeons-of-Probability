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
    public Image ClickTip;
    public Image BottomPanel;
    public Image HitEffectPanel;

    void Start()
    {
        for (int i = 0; i < 3 - PlayerPrefs.GetInt("Healths"); i++)
        {
            ReduceHealth();
        }
    }
    
    public void DisplayText(List<string> texts)
    {
        UIWriter.AddToQueue(TranslatedText(texts));
    }

    public void ReduceHealth()
    {
        healthsIcon[healthsIcon.Count-1].gameObject.SetActive(false);
        healthsIcon.RemoveAt(healthsIcon.Count-1);
        StartCoroutine(HitEffectPlay(0.5f));
    }

    public void Fail()
    {
        DeathPanel.gameObject.SetActive(true);
        DeathPanel.gameObject.GetComponent<RandomPhrase>().GeneratePhrase();
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
        BottomPanel.gameObject.SetActive(false);
        HitEffectPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        BottomPanel.gameObject.SetActive(true);
        HitEffectPanel.gameObject.SetActive(false);
    }
}
