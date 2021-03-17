using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWriter : MonoBehaviour
{
    public UIManager uiManager;
    public GameManager gameManager;
    public Text displayText;
    public List<string> textToWrite;
    public bool writing = false;

    public void AddToQueue(List<string> texts)
    {
        textToWrite.AddRange(texts);
        if (textToWrite.Count - texts.Count == 0)
        {
            uiManager.ChangeTextPanelStatus(true);
            StartCoroutine ("PlayText");
        }
    }

    IEnumerator PlayText()
    {
        uiManager.ClickTip.gameObject.SetActive(false);
        displayText.text = "";
        writing = true;
        foreach (char c in textToWrite[0]) 
        {
            displayText.text += c;
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
                        uiManager.Fail();
                    }
                    else
                    {
                        uiManager.ChangeTextPanelStatus(false);
                    }
                }
                else
                {
                    gameManager.BeginFight();
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