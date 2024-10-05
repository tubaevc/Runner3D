using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multicoin : Coin
{
    [SerializeField] private int multiCoinValue = 5;
    private PlayerScore _playerScore;

    public override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        if (collider.gameObject.name != "Player")
        {
            return;
        }

        PlayerScore.instance.AddScore(multiCoinValue);
        Destroy(gameObject);
    }
}