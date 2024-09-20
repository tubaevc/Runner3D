using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private PlayerMovements _playerMovements;

    private void Start()
    {
        _playerMovements = GameObject.FindObjectOfType<PlayerMovements>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            _playerMovements.Die();
        }
    }
}