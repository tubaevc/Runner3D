using System;
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
    private bool isPowerupActive = false;
    private Coroutine powerupCoroutine;
    
    // for 2x text
    public delegate void PowerupStateChanged(bool isActive);
    public static event PowerupStateChanged OnPowerupStateChanged;
    [SerializeField] private TMP_Text powerupText;
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
        score += isPowerupActive ? value * 2 : value;
        UpdateScoreUI();
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
        scoreText.text = "Score: " + score.ToString();
    }
}