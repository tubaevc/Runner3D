using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody rb;
    private float horizontalInput=3f;
    private bool alive=true;
    private void FixedUpdate()
    {
        if (!alive)
        {
            return;
        }
        Vector3 forwardMove = transform.forward * speed * Time.deltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.deltaTime;
        rb.MovePosition(rb.position+forwardMove+horizontalMove);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    public void Die()
    {
        alive = false;
        Debug.Log("player is dead");
    }
}
