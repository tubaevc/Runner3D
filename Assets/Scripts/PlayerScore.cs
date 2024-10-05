using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore instance { get; private set; }
    public int score { get; private set; } = 0;


    //  public int score = 0;
    [SerializeField] private TMP_Text scoreText;
    private bool isPowerupActive = false;
    private Coroutine powerupCoroutine;

    // for 2x text
    public delegate void PowerupStateChanged(bool isActive);

    public static event PowerupStateChanged OnPowerupStateChanged;
    [SerializeField] private TMP_Text powerupText;

    //high score
    public int highScore = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            LoadHighScore();
            Debug.Log("High Score " + highScore);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void AddScore(int value)
    {
        score += isPowerupActive ? value * 2 : value;
        UpdateScoreUI();
        CheckAndUpdateHighScore();
    }

    public void ActivatePowerup(float duration)
    {
        if (powerupCoroutine != null)
        {
            StopCoroutine(powerupCoroutine);
        }

        powerupCoroutine = StartCoroutine(PowerupTimer(duration));
    }

    private IEnumerator PowerupTimer(float duration)
    {
        SetPowerupState(true);
        yield return new WaitForSeconds(duration);
        SetPowerupState(false);
    }

    private void SetPowerupState(bool isActive)
    {
        isPowerupActive = isActive;
        OnPowerupStateChanged?.Invoke(isActive);
    }

    void UpdateScoreUI()
    {
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }

    private void CheckAndUpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
            UpdateScoreUI();
        }
    }

    public void CheckGameOver()
    {
        if (score >= highScore)
        {
            PlayerEvents.HighScore?.Invoke();
            Debug.Log("high score from check game over script");
        }
        else
        {
            PlayerEvents.OnPlayerDead?.Invoke();
            Debug.Log("game over from check game over script");
        }
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreUI();
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    public int GetHighScore()
    {
        return highScore;
    }
}