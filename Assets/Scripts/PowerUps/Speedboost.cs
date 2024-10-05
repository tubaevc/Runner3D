using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedboost : MonoBehaviour
{
    public float speedBoostMultiplier = 2f;

    private float originalSpeed;

    private PlayerMovements playerMovements;

    private bool isSpeedboostActive = false;
    private Coroutine powerupCoroutine;

    public delegate void PowerupStateChanged(bool isActive);

    public static event PowerupStateChanged SpeedboostPowerup;

    private void Start()
    {
        playerMovements = GetComponent<PlayerMovements>();
    }

    public void ActivatePowerup(float duration)
    {
        if (powerupCoroutine != null)
        {
            StopCoroutine(powerupCoroutine);
            SetPowerupState(false);
        }

        originalSpeed = playerMovements.currentSpeed;
        playerMovements.currentSpeed *= speedBoostMultiplier;


        powerupCoroutine = StartCoroutine(PowerupTimer(duration));
        SetPowerupState(true);
    }

    private IEnumerator PowerupTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        playerMovements.currentSpeed = originalSpeed;
        SetPowerupState(false);
    }

    private void SetPowerupState(bool isActive)
    {
        isSpeedboostActive = isActive;
        SpeedboostPowerup?.Invoke(isActive);
    }
}