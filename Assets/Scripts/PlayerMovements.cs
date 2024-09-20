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


    public float speedBoostMultiplier = 2.5f;
    public float speedBoostDuration = 7f;
    private bool isSpeedBoostActive = false;

    //for speedboost
    public float normalSpeed = 35f;
    private float currentSpeed;


    public float laneDistance = 5.0f;
    private int currentLane = 1; // 0 left, 1 middle, 2 right

    private float slideSpeed = 3f;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        if (!alive)
        {
            return;
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
                Vector3 slideDirection = transform.forward; 
                rb.MovePosition(transform.position + slideDirection * Time.deltaTime * slideSpeed); 
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

    

    private void CollectNearbyCoins()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, magnetRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Coin"))
            {
                hitCollider.transform.position = Vector3.MoveTowards(hitCollider.transform.position, transform.position,
                    Time.deltaTime * 50f);
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
            currentSpeed = normalSpeed * speedBoostMultiplier;
            StartCoroutine(DeactivateSpeedBoostAfterDelay());
        }
        else
        {
            // if already yet refresh time
            StopCoroutine(DeactivateSpeedBoostAfterDelay());
            StartCoroutine(DeactivateSpeedBoostAfterDelay());
        }
    }

    private IEnumerator DeactivateSpeedBoostAfterDelay()
    {
        yield return new WaitForSeconds(speedBoostDuration);
        isSpeedBoostActive = false;
        currentSpeed = normalSpeed;
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