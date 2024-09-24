using System;
using System.Collections;
using System.Collections.Generic;
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

    public bool isMagnetActive = false;
    public float magnetDuration = 5f;
    public float magnetRadius = 5f;


    public float speedBoostMultiplier = 2f;
    public float speedBoostDuration = 7f;
    private bool isSpeedBoostActive = false;

    //for speedboost
    public float normalSpeed = 35f;
    private float currentSpeed;
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

        if (isMagnetActive)
        {
            CollectNearbyCoins();
        }
    }

    private IEnumerator ResetColliderAfterSlide()
    {
        yield return new WaitForSeconds(3f);

        playerCollider.height = originalColliderHeight;
        playerCollider.center = originalColliderCenter;
    }

    private void CollectNearbyCoins()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, magnetRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Coin"))
            {
                hitCollider.transform.position = Vector3.MoveTowards(hitCollider.transform.position, transform.position,
                    Time.deltaTime * 70f);
            }
        }
    }

    public void Die()
    {
        alive = false;
        Debug.Log("player is dead");
        Invoke("Restart", 2);
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
            ActivateMagnet();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("SpeedBoost"))
        {
            ActivateSpeedBoost();
            Destroy(other.gameObject);
        }
    }


    private void ActivateSpeedBoost()
    {
        if (!isSpeedBoostActive)
        {
            isSpeedBoostActive = true;
            originalSpeed = currentSpeed; // keep the speed
            currentSpeed *= speedBoostMultiplier;
            StartCoroutine(DeactivateSpeedBoostAfterDelay());
        }
        else
        {
            // if already refresh time
            StopCoroutine(DeactivateSpeedBoostAfterDelay());
            StartCoroutine(DeactivateSpeedBoostAfterDelay());
        }
    }

    private IEnumerator DeactivateSpeedBoostAfterDelay()
    {
        yield return new WaitForSeconds(speedBoostDuration);
        isSpeedBoostActive = false;
        currentSpeed = originalSpeed; // return the speed
    }


    private void ActivateMagnet()
    {
        isMagnetActive = true;
        StartCoroutine(DeactivateMagnetAfterDelay());
    }

    private IEnumerator DeactivateMagnetAfterDelay()
    {
        yield return new WaitForSeconds(magnetDuration);
        isMagnetActive = false;
    }
}