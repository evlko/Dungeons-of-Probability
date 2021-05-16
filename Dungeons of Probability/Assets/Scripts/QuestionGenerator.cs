using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Random = UnityEngine.Random;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;
using TMPro;

public class QuestionGenerator : MonoBehaviour
{
    public List<Button> answersButtons;
    public GameManager gameManager;
    public UIWriter textWriter;
    public int randomNumber;

    public void GenerateQuestion(string difficulty)
    {
        randomNumber = GenerateQuestionNumber(difficulty);
        List<float> usedValues = ChooseExpression(randomNumber);
        List<string> question = new List<string>();
        question.Add(ReplaceValuesInText(LocalizationManager.instance.GetLocalizedValue("question_text_" + randomNumber.ToString()), usedValues));
        textWriter.AddToQueue(question);
        string symbol = "";
        if (usedValues[7] == 1f)
        {
            symbol = "%";
        }
        AssignValuesToButtons(usedValues.GetRange(4, 3), symbol);
    }

    int GenerateQuestionNumber(string difficulty)
    {
        string getArray = "Questions" + difficulty;
        int[] previousQuestions = PlayerPrefsX.GetIntArray(getArray);
        int[] allQuestions = new int[] {};
 
        switch(difficulty){
            case "Easy":
                allQuestions = new int[ ] {1, 2, 3, 11};
                break;
            case "Normal":
                allQuestions = new int[ ] {4, 5, 6};
                break;
            case "Hard":
                allQuestions = new int[ ] {7, 8, 9, 10};
                break;
        }

        int length = allQuestions.Length;

        if (previousQuestions.Sum() == length)
        {
            for (int i = 0; i < previousQuestions.Length; i++)
            {
                previousQuestions[i] = 0;
            }
        }

        List<int> possibleQuestions = new List<int>();

        for (int i = 0; i < previousQuestions.Length; i++)
        {
            if (previousQuestions[i] == 0)
            {
                possibleQuestions.Add(allQuestions[i]);
            }
        }

        int randomNumber = Random.Range(0, possibleQuestions.Count);

        for (int i = 0; i < allQuestions.Length; i++)
        {
            if (allQuestions[i] == possibleQuestions[randomNumber])
            {
                previousQuestions[i] = 1;
            }
        }

        PlayerPrefsX.SetIntArray(getArray, previousQuestions);
        return possibleQuestions[randomNumber];
    }

    private static List<float> ChooseExpression(int num)
    {
        List<float> values = new List<float>(new float[]{0, 0, 0, 0, 0, 0, 0, 0});
        // values: value1 .. value4, correctResult, incorrectResult1, incorrectResult 2, percent or not
        switch (num)
        {
            case 1:
                values[0] = Random.Range(3, 7) * 10;
                values[1] = Random.Range(2, 4);
                values[4] = (float) (Math.Pow(values[0], values[1]) / Math.Pow(100, values[1]-1));
                values[5] = values[4] / 10;
                values[6] = values[4] * 2;
                values[7] = 1f;
                break;
            case 2:
                values[4] = 6;
                values[5] = 9;
                values[6] = 5;
                break;
            case 3:
                values[0] = Random.Range(4, 9);
                values[1] = Random.Range(4, 9);
                values[2] = Random.Range(4, 17);
                values[4] = (float) Math.Round((values[0] + values[1]) / (values[0] + values[1] + values[2]) * 100, 2) ;
                values[5] = 50;
                values[6] = (float) Math.Round((values[0] + values[1]) / values[2], 2) * 10;
                values[7] = 1f;
                break;
            case 4:
                values[0] = Random.Range(5, 10);
                values[1] = Random.Range(10, 15);
                values[4] = (float) Math.Round((1 - values[0] / values[1] * values[0] / values[1]) * 100, 2);
                values[5] = (float) Math.Round(values[1] * values[0] / values[1], 2) * 10;
                values[6] = (float) Math.Round(values[4] - Random.Range(2f, 3.5f), 2);
                values[7] = 1f;
                break;
            case 5:
                values[4] = 120;
                values[5] = 25;
                values[6] = 100;
                break;
            case 6:
                values[4] = 12;
                values[5] = 7;
                values[6] = 10;
                break;
            case 7:
                values[0] = 5;
                values[1] = Random.Range(2, 4);
                values[4] = Factorial(values[0])/Factorial(values[0]-values[1]);
                values[5] = values[4] / 2;
                values[6] = 25;
                break;
            case 8:
                values[0] = 50;
                values[1] = Nearest(Random.Range(60, 91), 5);
                values[4] = values[0] * values[1] / 100;
                values[5] = values[4] * 2;
                values[6] = 50;
                values[7] = 1;
                break;
            case 9:
                values[0] = Random.Range(10, 13);
                values[1] = Random.Range(1, 6);
                values[2] = Random.Range(3, 8);
                values[3] = Random.Range(5, 8);
                values[4] = (float) Math.Round((values[1] / values[0]) * (values[2] / values[0]) * (values[3] / values[0]) * 100, 2);
                values[5] = (float) Math.Round(values[1] / values[0] + values[2] / values[0] + values[3] / values[0] * 10, 2);
                values[6] = values[0] * values[1] * values[2] / 5;
                values[7] = 1;
                break;
            case 10:
                values[0] = Random.Range(3, 6);
                values[1] = Random.Range(2, 7);
                values[2] = Random.Range(2, 5);
                values[3] = values[0] + values[1] + values[2];
                values[4] = (float) Math.Round((values[0] / values[3] * values[2] / (values[3] - 1)) * 100, 2);
                values[5] = (float) Math.Round((values[0] / values[3] * values[2] / values[3]) * 100, 2);
                values[6] = values[2] + values[0];
                values[7] = 1f;
                break;
            case 11:
                values[4] = 720;
                values[5] = 360;
                values[6] = 540;
                break;
            case 12:
                values[0] = Random.Range(3, 9);
                values[4] = values[0] * values[0];
                values[5] = values[0] + values[0];
                values[6] = (float) Math.Round((values[0] * values[0])/2);
                break;
            case 13:
                values[0] = Random.Range(3, 6);
                values[1] = Random.Range(3, 6);
                values[4] = (Factorial(values[0])/Factorial(2)*Factorial(values[0]-2))*values[1];
                values[5] = values[4] * 2;
                values[6] = (float) Math.Round(values[0]/2);
                break;
            case 14:
                values[4] = 15;
                values[5] = 30;
                values[6] = 64;
                break;
            case 15:
                values[1] = Random.Range(2, 5);
                values[4] = Factorial(5+values[1]-1)/Factorial(4)*Factorial(values[1]);
                values[5] = values[4] * 2;
                values[6] = Factorial(5) / Factorial(5-values[1])*Factorial(values[1]);
                break;
            case 19:
                values[4] = 2520;
                values[5] = 2520*2;
                values[6] = 2520+2520/2;
                break;
            case 20:
                values[1] = Random.Range(3, 5);
                values[2] = Random.Range(2, 5);
                values[0] = values[1] + values[2];
                values[4] = Factorial(values[0])/(Factorial(values[1]) * Factorial(values[2]));
                values[5] = (float) Math.Round(Factorial(values[0])/Factorial(values[1]));
                values[6] = Factorial(values[1]);
                break;
            case 21:
                values[4] = 1013;
                values[5] = 1024;
                values[6] = 2048;
                values[8] = 10;
                break;
            case 22:
                values[0] = Random.Range(6, 10);
                values[4] = values[0] * (values[0]-1) / 2;
                values[5] = values[0] * values[0];
                values[6] = values[0] * (values[0]-1);
                break;

        }

        return values;
    }

    private static float Factorial(float i)
    {
        if (i <= 1)
            return 1;
        return i * Factorial(i - 1);
    }

    private static float Nearest(float number, float near)
    {
        return (float) Math.Ceiling(number/near) * near;
    }

    public void AssignValuesToButtons(List<float> values, string symbol)
    {
        float correctAnswer = values[0];
        values = values.OrderBy(s => Random.value).ToList();
        for (int i = 0; i < 3; i++)
        {
            Button btn = answersButtons[i].GetComponent<Button>();
            answersButtons[i].GetComponentInChildren<Text>().text = values[i].ToString() + symbol;
            btn.onClick.RemoveAllListeners();
            if (values[i] == correctAnswer)
            {
                btn.onClick.AddListener(gameManager.CorrectAnswer);
            }
            else
            {
                btn.onClick.AddListener(gameManager.IncorrectAnswer);
            }
        }
    }
    
    private string ReplaceValuesInText(string textWithValues, List<float> values)
    {
        for (int i = 0; i < 7; i++)
        {
            textWithValues = textWithValues.Replace("Value" + (i+1).ToString(), values[i].ToString());
        }
        
        return textWithValues;
    }
}
