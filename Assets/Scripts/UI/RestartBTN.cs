using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class RestartBTN : UIBTN
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerStartPosition;

    protected override void OnClick()
    {
        Debug.Log("Restart Button clicked");
        PlayerScore.instance.ResetScore();
        PlayerEvents.OnGameRestart?.Invoke();
        RestartGame();
    }

    public void RestartGame()
    {
        player.transform.position = playerStartPosition.position;

        PlayerScore.instance.ResetScore();

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}