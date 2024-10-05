using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] float horizontalMultiplier = 5;
    private Rigidbody rb;
    private bool alive = true;
    [SerializeField] private float jumpForce = 400f;
    public bool isGrounded = false;
    public LayerMask groundLayer;
    public Transform groundCheck;
    [SerializeField] private Animator animator;

    //for speedboost
    public float normalSpeed = 35f;
    public float currentSpeed;
    private float originalSpeed;

    public float laneDistance = 5.0f;
    private int currentLane = 1; // 0 left, 1 middle, 2 right

    private float slideSpeed = 3f;

    //speed increases as play
    [SerializeField] private float speedIncreaseRate = 10f;
    [SerializeField] private float speedIncreaseInterval = 5f;
    [SerializeField] private float maxSpeed = 200f;
    private float timeSinceLastSpeedIncrease;

    //collider height settings for sliding
    [SerializeField] private CapsuleCollider playerCollider;
    private float originalColliderHeight;
    private Vector3 originalColliderCenter;
    private float slideColliderOffset = 0.5f;


    private Speedboost speedBoost;
    private Magnet magnet;

    private void Awake()
    {
        speedBoost = GetComponent<Speedboost>();
        magnet = GetComponent<Magnet>();
        if (speedBoost == null)
        {
            speedBoost = gameObject.AddComponent<Speedboost>();
            // Debug.Log("SpeedBoost component added dynamically.");
        }

        if (magnet == null)
        {
            magnet = gameObject.AddComponent<Magnet>();
            //   Debug.Log("Magnet component added dynamically.");
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = normalSpeed;

        if (playerCollider != null)
        {
            originalColliderHeight = playerCollider.height;
            originalColliderCenter = playerCollider.center;
        }
    }

    private void Update()
    {
        if (!alive)
        {
            return;
        }

        //speed increase
        timeSinceLastSpeedIncrease += Time.deltaTime;

        if (timeSinceLastSpeedIncrease >= speedIncreaseInterval && currentSpeed < maxSpeed)
        {
            currentSpeed += speedIncreaseRate;
            Debug.Log(currentSpeed);
            timeSinceLastSpeedIncrease = 0f;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0)
        {
            currentLane--; // left
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 2)
        {
            currentLane++; //right
        }

        Vector3 targetPosition = transform.position;
        targetPosition.x = (currentLane - 1) * laneDistance;

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * horizontalMultiplier);

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (animator != null)
            {
                animator.SetTrigger("Jump");
                Jump();
            }
            else
            {
                Debug.LogError("animator  not found for player");
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && isGrounded)
        {
            if (animator != null)
            {
                animator.SetTrigger("Slide");
                playerCollider.height = originalColliderHeight / 2;
                playerCollider.center = new Vector3(originalColliderCenter.x,
                    originalColliderCenter.y - slideColliderOffset / 2, originalColliderCenter.z);


                Vector3 slideDirection = transform.forward;
                rb.MovePosition(transform.position + slideDirection * Time.deltaTime * slideSpeed);


                StartCoroutine(ResetColliderAfterSlide());
            }
            else
            {
                Debug.LogError("animator  not found for player");
            }
        }

        CheckGrounded();
    }

    private IEnumerator ResetColliderAfterSlide()
    {
        yield return new WaitForSeconds(3f);

        playerCollider.height = originalColliderHeight;
        playerCollider.center = originalColliderCenter;
    }


    public void Die()
    {
        alive = false;
        Debug.Log("player is dead");

        //check gameover
        PlayerScore.instance.CheckGameOver();
        //   PlayerEvents.OnPlayerDead?.Invoke();
        Time.timeScale = 0;
        // Debug.Log("OnPlayerDead event invoked");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Magnet"))
        {
            magnet.ActivatePowerup(duration: 7f);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("SpeedBoost"))
        {
            speedBoost.ActivatePowerup(duration: 7f);
            Destroy(other.gameObject);
        }
    }
}