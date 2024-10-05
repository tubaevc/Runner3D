using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierCoin : Coin
{
    private Collider coinCollider;
    private MeshRenderer meshRenderer;

    [SerializeField] private float powerupDuration = 10f;

    public override void Start()
    {
        base.Start();
        coinCollider = GetComponent<Collider>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            PlayerScore.instance.ActivatePowerup(powerupDuration);
            Destroy(gameObject);
            coinCollider.enabled = false;
            meshRenderer.enabled = false;
        }
    }
}