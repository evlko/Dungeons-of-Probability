using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;
    UIWriter UIWriter;
    HeroesPool heroesPool;
    public List<Image> healthsIcon;
    public Image DeathPanel;
    public Image WinPanel;
    public Image ClickTip;
    public Image HitEffectPanel;
    public List<Text> HeroesNames;
    public List<Text> HeroesLevels;
    public Button DialogueButton;
    public Transform[] FingerTips;
    public Image[] HeroesPics;
    public List<Image> Heroes;
    public Transform HintButton;
    
    void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
        UIWriter = gameObject.GetComponent<UIWriter>();
        heroesPool = gameObject.GetComponent<HeroesPool>();

        List<int> namesIndexes = new List<int>();
        List<int> levelsIndexes = new List<int>();
        List<int> picsIndexes = new List<int>();
        if(PlayerPrefs.GetInt("Level") == 0){
            namesIndexes = RandomIndexes(4, heroesPool.names.Count);
            levelsIndexes = RandomIndexes(4, heroesPool.levels.Count);
            picsIndexes = RandomIndexes(4, heroesPool.pics.Count);
            PlayerPrefsX.SetIntArray("HeroesNames", namesIndexes.ToArray());
            PlayerPrefsX.SetIntArray("HeroesLevels", levelsIndexes.ToArray());
            PlayerPrefsX.SetIntArray("HeroesPics", picsIndexes.ToArray());
        }
        
        for (int i = 0; i < HeroesLevels.Count; i++){
            HeroesLevels[i].text = LocalizationManager.instance.GetLocalizedValue("Level") + heroesPool.levels[PlayerPrefsX.GetIntArray("HeroesLevels")[i]];
            HeroesNames[i].text = heroesPool.names[PlayerPrefsX.GetIntArray("HeroesNames")[i]];
            HeroesPics[i].sprite = heroesPool.pics[PlayerPrefsX.GetIntArray("HeroesPics")[i]];
        }

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
        //gameManager.SoundManager.PlaySound(gameManager.SoundManager.Fail);
        DeathPanel.gameObject.SetActive(true);
        DeathPanel.gameObject.GetComponent<RandomPhrase>().GeneratePhrase();
        StartCoroutine(FadeIn());
    }

    public void Win(){
        gameManager.SoundManager.PlaySound(gameManager.SoundManager.Win);
        List<string> texts = new List<string>();
        texts.Add(LocalizationManager.instance.GetLocalizedValue("Win_phrase"));
        UIWriter.AddToQueue(texts);
        UIWriter.end = true;
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

    List<int> RandomIndexes(int count, int length){
        List<int> indexes = new List<int>();
        while (count > 0){
            int index = Random.Range(0, length);
            if (indexes.Contains(index) == false){
                indexes.Add(index);
                count = count - 1;
            }
        }
        return indexes;
    }

    public void SetHeroesStatus(bool status){
        int count = 3;
        int level = PlayerPrefs.GetInt("Level");
        if (level >= 3){
            count += 1;
        }
        for (int i = 0; i < count; i++)
        {
            Heroes[i].gameObject.SetActive(status);
        }
    }

    IEnumerator HitEffectPlay(float waitTime)
    {
        HitEffectPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        HitEffectPanel.gameObject.SetActive(false);
    }

    public void FingerTipsOff(){
        for (int i = 0; i < FingerTips.Length; i++){
            FingerTips[i].gameObject.SetActive(false);
        }
    }
    
    public void ShowHint(){
        UIWriter.StopAllCoroutines();
        UIWriter.writing = false;
        UIWriter.displayText.text = "";
        if (PlayerPrefs.GetString("Language") == "Russian"){
            //Application.ExternalEval("window.open('" + "https://www.notion.so/Dungeons-of-Probability-42964f9dcf60423da19c694ea2fa9b61" + "', '_blank')");
            UIWriter.displayText.text = "Решение этой задачи можно посмотреть в <color=#FFAB00>решебнике</color>. Задача: " + gameManager.currentQuestion.ToString();
        }
        else{
            UIWriter.displayText.text = "Unfortunately, we are still working on problem explanations :( Soon!";
        }
    }

    // fade from transparent to opaque
    IEnumerator FadeIn()
    {
 
        // loop over 1 second
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            DeathPanel.color = new Color(0, 0, 0, i);
            yield return null;
        }
    }
}
