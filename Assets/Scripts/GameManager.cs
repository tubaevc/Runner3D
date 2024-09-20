using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    [SerializeField] private TMP_Text scoreText;
    private int scoreMultiplier = 1;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int value)
    {
        score += value * scoreMultiplier;
        UpdateScoreUI();
    }
    public void SetScoreMultiplier(int multiplier)
    {
        scoreMultiplier = multiplier;
        Debug.Log("multiplier: " + scoreMultiplier);
    }
    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}