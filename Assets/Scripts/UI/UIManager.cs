using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject multiplierImage;
    [SerializeField] private GameObject magnetImage;
    [SerializeField] public GameObject speedImage;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject highScorePanel;
    [SerializeField] private TMP_Text gameOverScoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text highScore;
    private float targetScore; // remaining score
    private float displayedScore; // showing score
    public float lerpSpeed = 2.0f;

    private void Awake()
    {
        multiplierImage.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        highScorePanel.gameObject.SetActive(false);
    }

    private void Start()
    {
        highScore.text = PlayerScore.instance.highScore.ToString();
        targetScore = PlayerScore.instance.highScore - PlayerScore.instance.score;
        displayedScore = targetScore;
    }

    private void Update()
    {
        targetScore = PlayerScore.instance.highScore - PlayerScore.instance.score;

        displayedScore = Mathf.Lerp(displayedScore, targetScore, lerpSpeed * Time.deltaTime);

        highScore.text = Mathf.RoundToInt(displayedScore).ToString();
    }

    private void OnEnable()
    {
        PlayerScore.OnPowerupStateChanged += UpdatePowerupUI;
        PlayerEvents.OnPlayerDead += ShowGameOverPanel;
        PlayerEvents.HighScore += ShowHighScorePanel;
        Magnet.MagnetPowerup += MagnetPowerupUI;
        Speedboost.SpeedboostPowerup += SpeedBoostUI;
    }

    private void OnDisable()
    {
        PlayerScore.OnPowerupStateChanged -= UpdatePowerupUI;
        PlayerEvents.OnPlayerDead -= ShowGameOverPanel;
        PlayerEvents.HighScore -= ShowHighScorePanel;
        Magnet.MagnetPowerup -= MagnetPowerupUI;
        Speedboost.SpeedboostPowerup -= SpeedBoostUI;
    }

    // TODO: dictionary usage for Powerups UI 
    private void UpdatePowerupUI(bool isActive)
    {
        multiplierImage.gameObject.SetActive(isActive);
    }

    private void MagnetPowerupUI(bool isActive)
    {
        magnetImage.gameObject.SetActive(isActive);
    }

    private void SpeedBoostUI(bool isActive)
    {
        speedImage.gameObject.SetActive(isActive);
    }

    private void ShowGameOverPanel()
    {
        gameOverPanel.gameObject.SetActive(true);
        gameOverScoreText.text = PlayerScore.instance.score.ToString();
    }

    private void ShowHighScorePanel()
    {
        highScorePanel.gameObject.SetActive(true);
        highScoreText.text = PlayerScore.instance.GetHighScore().ToString();
    }
}