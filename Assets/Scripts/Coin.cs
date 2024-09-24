using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Transform playerTransform;
    [SerializeField] private int coinValue = 1;
    public virtual void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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

        GameManager.instance.AddScore(coinValue);
        Destroy(gameObject);
    }
}