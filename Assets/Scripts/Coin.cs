using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 90f;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Obstacle>()!=null)
        {
            Destroy(gameObject);
            return;
        }
        if (collider.gameObject.name != "Player")
        {
            return;
        }

        GameManager.instance.AddScore();
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Rotate(0,0,turnSpeed*Time.deltaTime);
    }
}
