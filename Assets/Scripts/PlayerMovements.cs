using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] float horizontalMultiplier = 2;
    private Rigidbody rb;
    private float horizontalInput = 3f;
    private bool alive = true;
    [SerializeField] private float jumpForce = 400f;
    public bool isGrounded = false;
    public LayerMask groundLayer;
    public Transform groundCheck;

    private void FixedUpdate()
    {
        if (!alive)
        {
            return;
        }

        Vector3 forwardMove = transform.forward * speed * Time.deltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.deltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        CheckGrounded();
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
      //  Invoke("Restart", 2);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);
    }
}