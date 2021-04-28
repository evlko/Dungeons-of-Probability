﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
    public List<string> enemyPhrases;
    public bool nextBattle = true;
    public List<Animator> Animators;

    public UIManager UIManager;
    public EnemiesPool enemiesPool;
    public QuestionGenerator QuestionGenerator;
    public Transform extraAnswerButton;
    
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
            SetEnemyData();
            if (currentEnemyHealth == 3)
            {
                UIManager.DisplayText(enemyPhrases.GetRange(0, 1));
            }
            QuestionGenerator.GenerateQuestion();
        }

        if (level == 3){
            extraAnswerButton.gameObject.SetActive(true);
        }
    }

    public void BeginFight()
    {
        level += 1;
        if (level == 3){
            extraAnswerButton.gameObject.SetActive(true);
        }
        PlayerPrefs.SetInt("Level", level);
        currentEnemyNumber = Random.Range(0, enemiesPool.Enemies.Length);
        PlayerPrefs.SetInt("Enemy", currentEnemyNumber);
        PlayerPrefs.SetInt("EnemyHealths", 3);
        SetEnemyData();
        UIManager.DisplayText(enemyPhrases.GetRange(0, 1));
        QuestionGenerator.GenerateQuestion();
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
            QuestionGenerator.GenerateQuestion();
        }
        else
        {
            UIManager.DisplayText(enemyPhrases.GetRange(3, 1));
            nextBattle = true;
        }
        Enemy.GetComponent<Animator>().SetTrigger("Hit");
        ChangeAnswersButtonsStatus(false);
    }

    public void IncorrectAnswer()
    {
        healths -= 1;
        UIManager.ReduceHealth();
        if (healths > 0)
        {
            PlayerPrefs.SetInt("Healths", healths);
            UIManager.DisplayText(enemyPhrases.GetRange(2, 1));
            QuestionGenerator.GenerateQuestion();
        }
        else
        {
            UIManager.DisplayText(enemyPhrases.GetRange(4, 2));
        }
        ChangeAnswersButtonsStatus(false);
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
            Animators[i].SetBool("Show", status);
        }
    }
}
