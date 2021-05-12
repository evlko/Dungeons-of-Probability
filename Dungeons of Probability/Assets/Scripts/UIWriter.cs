using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIWriter : MonoBehaviour
{
    public UIManager uiManager;
    public GameManager gameManager;
    public MoveCamera moveCamera;
    public TextMeshProUGUI displayText;
    public List<string> textToWrite;
    public bool writing = false;
    public bool end = false;

    public void AddToQueue(List<string> texts)
    {
        textToWrite.AddRange(texts);
        if (textToWrite.Count - texts.Count == 0)
        {
            StartCoroutine ("PlayText");
        }
    }

    IEnumerator PlayText()
    {
        uiManager.ClickTip.gameObject.SetActive(false);
        displayText.text = "";
        writing = true;
        bool tag = false;
        for (int i = 0; i < textToWrite[0].Length; i++)
        {
            char symbol = textToWrite[0][i];
            if (symbol == '<' && tag == false)
            {
                displayText.text += textToWrite[0].Substring(i, 15);
                displayText.text += "</color>";
                i += 14;
                tag = true;
            }
            else if (symbol == '<' && textToWrite[0][i+1] == '/' && tag == true)
            {
                i += 7;
                tag = false;
            }
            else if (tag)
            {
                displayText.text = displayText.text.Insert(displayText.text.Length - 8, symbol.ToString());
            }
            else
            {
                displayText.text += symbol;
            }
            yield return new WaitForSeconds (0.125f);
        }
        textToWrite.RemoveAt(0);
        uiManager.ClickTip.gameObject.SetActive(true);
        writing = false;
    }

    public void ClickText()
    {
        if (writing == false)
        {
            if (textToWrite.Count == 0)
            {
                if (gameManager.nextBattle == false)
                {
                    if (gameManager.healths == 0)
                    {
                        gameManager.SoundManager.PlaySound(gameManager.SoundManager.Fail);
                        uiManager.Fail();
                    }
                    else
                    {
                        uiManager.ClickTip.gameObject.SetActive(false);
                        gameManager.ChangeAnswersButtonsStatus(true);
                    }
                }
                else if(end)
                {
                    uiManager.WinPanel.gameObject.SetActive(true);
                    PlayerPrefs.SetInt("Level", 0);
                }
                else
                {
                    gameManager.EnemyDefeated();
                    moveCamera.Move();
                }
            }
            else
            {
                StartCoroutine ("PlayText");
            }
        }
        else if(writing == true)
        {
            displayText.text = textToWrite[0];
            StopAllCoroutines();
            textToWrite.RemoveAt(0);
            uiManager.ClickTip.gameObject.SetActive(true);
            writing = false;
        }
    }
}