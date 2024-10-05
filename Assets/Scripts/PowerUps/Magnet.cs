using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magnet : MonoBehaviour
{
    public bool isMagnetActive = false;
    public float magnetDuration = 5f;
    public float magnetRadius = 5f;

    public delegate void PowerupStateChanged(bool isActive);

    public static event PowerupStateChanged MagnetPowerup;

    private bool isPowerupActive = false;
    private Coroutine powerupCoroutine;

    private void Update()
    {
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
                    Time.deltaTime * 70f);
            }
        }
    }

    public void ActivatePowerup(float duration)
    {
        if (powerupCoroutine != null)
        {
            isMagnetActive = false;
            StopCoroutine(powerupCoroutine);
        }

        powerupCoroutine = StartCoroutine(PowerupTimer(duration));
        isMagnetActive = true;
    }

    private IEnumerator PowerupTimer(float duration)
    {
        SetPowerupState(true);
        yield return new WaitForSeconds(duration);
        SetPowerupState(false);
    }

    private void SetPowerupState(bool isActive)
    {
        isPowerupActive = isActive;
        MagnetPowerup?.Invoke(isActive);
    }
}