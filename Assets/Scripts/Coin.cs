using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Transform playerTransform;
    [SerializeField] private int coinValue = 1;
    private PlayerScore _playerScore;
    [SerializeField] private GameObject coinParticle;
    public float particleDuration = 1f;

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

        GameObject particleEffect = Instantiate(coinParticle, transform.position, Quaternion.identity);

        particleEffect.transform.SetParent(collider.transform);

        particleEffect.transform.localPosition = new Vector3(0, 0.5f, 0);
        Destroy(particleEffect, particleDuration);

        PlayerScore.instance.AddScore(coinValue);
        Destroy(gameObject);
    }
}