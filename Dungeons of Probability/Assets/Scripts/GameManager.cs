using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int level = 1;
    public int healths = 3;
    public List<string> introPhrases;
    public string currentEnemy = "";
    public int currentEnemyNumber;
    public int currentEnemyHealth = 3;
    public GameObject Enemy;
    public GameObject EnemyShadow;
    public List<string> enemyPhrases;
    public bool nextBattle = true;
    public List<Transform> AnswerButtons;
    public UIManager UIManager;
    public EnemiesPool enemiesPool;
    public QuestionGenerator QuestionGenerator;
    public SoundManager SoundManager;
    
    void Start()
    {
        healths = PlayerPrefs.GetInt("Healths");
        level = PlayerPrefs.GetInt("Level");
        if (level == 0)
        {
            UIManager.DisplayText(introPhrases);
        }
        else
        {
            currentEnemyNumber = PlayerPrefs.GetInt("Enemy");
            if (currentEnemyNumber != 0){
                Enemy.GetComponent<Animator>().SetBool("Floating", false);
            }
            SetEnemyData();
            if (currentEnemyHealth == 3)
            {
                UIManager.DisplayText(enemyPhrases.GetRange(0, 1));
            }
            
            QuestionGenerator.GenerateQuestion(levelDifficulty());
        }
    }

    string levelDifficulty(){
        string difficulty = "";
        switch(level){
            case 1:
                difficulty = "Easy";
                break;
            case 2:
                difficulty = "Normal";
                break;
            case 3:
                difficulty = "Hard";
                break;
        }
        return difficulty;
    }

    public void BeginFight(){
        currentEnemyNumber = enemyNumber();
        PlayerPrefs.SetInt("Enemy", currentEnemyNumber);
        SetEnemyData();
        UIManager.DisplayText(enemyPhrases.GetRange(0, 1));
        UIManager.SetHeroesStatus(true);
        QuestionGenerator.GenerateQuestion(levelDifficulty());
    }

    int enemyNumber(){
        int number = -1;
        switch(level){
            case 1:
                number = Random.Range(1, enemiesPool.Enemies.Length);
                PlayerPrefs.SetInt("FirstEnemy", number);
                Enemy.GetComponent<Animator>().SetBool("Floating", false);
                break;
            case 2:
                number = Random.Range(1, enemiesPool.Enemies.Length);
                int previousEnemy = PlayerPrefs.GetInt("FirstEnemy");
                while (number == previousEnemy){
                    number = Random.Range(1, enemiesPool.Enemies.Length);
                }
                Enemy.GetComponent<Animator>().SetBool("Floating", false);
                break;
            case 3:
                number = 0;
                Enemy.GetComponent<Animator>().SetBool("Floating", true);
                break;
        }
        return number;
    }

    public void EnemyDefeated(){
        level += 1;
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("EnemyHealths", 3);
    }

    void SetEnemyData()
    {
        currentEnemy = enemiesPool.Enemies[currentEnemyNumber].enemyName;
        Enemy.gameObject.GetComponent<SpriteRenderer>().sprite = enemiesPool.Enemies[currentEnemyNumber].enemySprite;
        currentEnemyHealth = PlayerPrefs.GetInt("EnemyHealths");
        EnemyHealthBar enemyHealthBar = Enemy.GetComponent<EnemyHealthBar>();
        StopAllCoroutines();
        enemyHealthBar.SetStatus(true);
        enemyHealthBar.SetHealthBar(currentEnemyHealth);
        enemyHealthBar.ChangePosition(enemiesPool.Enemies[currentEnemyNumber].enemyHeathPosition);
        float enemyShadowSize = enemiesPool.Enemies[currentEnemyNumber].shadowSize;
        EnemyShadow.GetComponent<Transform>().transform.localScale = new Vector3(enemyShadowSize, enemyShadowSize, 1);
        nextBattle = false;
        GenerateEnemyPhrases();
    }

    public void CorrectAnswer()
    {
        currentEnemyHealth -= 1;
        StartCoroutine(Enemy.GetComponent<EnemyHealthBar>().ScaleObject(currentEnemyHealth*11, 1));
        if (currentEnemyHealth > 0)
        {
            PlayerPrefs.SetInt("EnemyHealths", currentEnemyHealth);
            UIManager.DisplayText(enemyPhrases.GetRange(1, 1));
            QuestionGenerator.GenerateQuestion(levelDifficulty());
        }
        else
        {
            UIManager.DisplayText(enemyPhrases.GetRange(3, 1));
            EnemyHealthBar enemyHealthBar = Enemy.GetComponent<EnemyHealthBar>();
            enemyHealthBar.SetStatus(false);
            nextBattle = true;
        }
        Enemy.GetComponent<Animator>().SetTrigger("Hit");
        UIManager.FingerTipsOff();
        ChangeAnswersButtonsStatus(false);
        SoundManager.PlaySound(SoundManager.EnemyHit);
    }

    public void IncorrectAnswer()
    {
        Analytics.CustomEvent("incorrectAnswer", new Dictionary<string, object>
        {
            { "Question Number", QuestionGenerator.randomNumber },
        });
        healths -= 1;
        UIManager.ReduceHealth(true);
        if (healths > 0)
        {
            PlayerPrefs.SetInt("Healths", healths);
            UIManager.DisplayText(enemyPhrases.GetRange(2, 1));
            QuestionGenerator.GenerateQuestion(levelDifficulty());
        }
        else
        {
            UIManager.DisplayText(enemyPhrases.GetRange(4, 2));
        }
        UIManager.FingerTipsOff();
        ChangeAnswersButtonsStatus(false);
        SoundManager.PlaySound(SoundManager.PlayerHit);
    }

    void GenerateEnemyPhrases()
    {
        enemyPhrases.Clear();
        enemyPhrases.Add("EnemyIntro_" + currentEnemy); // 0
        enemyPhrases.Add("EnemyTakesHit_" + currentEnemy); // 1
        enemyPhrases.Add("PlayerTakesHit_" + currentEnemy); // 2
        enemyPhrases.Add("EnemyDefeated_" + currentEnemy); // 3
        enemyPhrases.Add("PlayerTakesFinalHit_" + currentEnemy); // 4
        enemyPhrases.Add("PlayerDefeated_" + currentEnemy); // 5
    }

    public void ChangeAnswersButtonsStatus(bool status)
    {
        int count = 3;
        if (level == 3){
            count += 1;
        }
        for (int i = 0; i < count; i++)
        {
            AnswerButtons[i].gameObject.SetActive(status);
        }
    }
}
