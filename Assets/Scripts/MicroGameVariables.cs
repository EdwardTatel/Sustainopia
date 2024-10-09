using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public static class MicroGameVariables
{
    private static int lives = 3;
    private static int timer;
    private static levels difficulty = levels.easy;
    public static bool gameFailed = false;
    private static int gamesFinished = 0;
    public static bool _showUI = false;
    private static int tries = 2;

    public static bool showUI
    {
        get { return _showUI; }
        set
        {
            if (_showUI != value)
            {
                _showUI = value;
                if (OnShowUIChange != null) OnShowUIChange(_showUI);
            }
        }
    }
    public static void resetTries()
    {
        tries = 2;
    }
    public static void deductTries()
    {
        tries--;
    }
    public static int getTries()
    {
        return tries;
    }
    public static System.Action<bool> OnShowUIChange;
    public enum levels { easy, hard, medium }

    public static levels GetDifficulty()
    {
        return difficulty;
    }
    public static int GetLives()
    {
        return lives;
    }
    public static void DeductLife() 
    {
        lives -= 1;
    }
    public static void ShowUI()
    {
        MicroGameVariables.showUI = true;
    }

    public static void HideUI()
    {
        MicroGameVariables.showUI = false;
    }

    public static void SetDifficulty(string difficultystring)
    {
            switch (difficultystring)
            {
                case "easy":
                    difficulty = levels.easy;
                    break;
                case "medium":
                    difficulty = levels.medium;
                    break;
                case "hard":
                    difficulty = levels.hard;
                    break;
            default:
                    difficulty = levels.easy;
                    break;
            }

    }
    public static void DifficultyChange()
    {
        if ((gamesFinished % 3) == 0)
        {
            switch (difficulty)
            {
                case levels.easy:
                    difficulty = levels.medium;
                    break;
                default:
                    difficulty = levels.hard;
                    break;
            }
        }
    }
    public static int GetTimerDuration()
    {
        switch (difficulty)
        {
            case levels.medium:
                return 7;
            case levels.hard:
                return 5;
            default:
                return 9;
        }
    }
}
