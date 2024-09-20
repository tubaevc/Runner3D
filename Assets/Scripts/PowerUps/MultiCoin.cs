using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multicoin : Coin
{
    [SerializeField] private int multiCoinValue = 5;

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

        GameManager.instance.AddScore(multiCoinValue);
        Destroy(gameObject);
    }
}