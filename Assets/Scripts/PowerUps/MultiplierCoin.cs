using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierCoin : Coin
{
    [SerializeField] private float multiplierDuration = 10f;
    private Collider coinCollider;
    private MeshRenderer meshRenderer;

    [SerializeField] private float powerupDuration = 10f;

    private void Start()
    {
        base.Start();
        coinCollider = GetComponent<Collider>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            GameManager.instance.ActivatePowerup(powerupDuration);
            Destroy(gameObject);
            coinCollider.enabled = false;
            meshRenderer.enabled = false;
        }
    }
}