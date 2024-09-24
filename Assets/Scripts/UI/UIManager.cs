using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text powerupText;

    private void Awake()
    {
        powerupText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.OnPowerupStateChanged += UpdatePowerupUI;
    }

    private void OnDisable()
    {
        GameManager.OnPowerupStateChanged -= UpdatePowerupUI;
    }

    private void UpdatePowerupUI(bool isActive)
    {
        powerupText.gameObject.SetActive(isActive);
        if (isActive)
        {
            powerupText.text = "2X";
        }
    }
}
