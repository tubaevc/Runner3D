using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject multiplierUI; 
    [SerializeField] private TMP_Text multiplierText;
    private void OnEnable()
    {
        MultiplierCoin.OnMultiplierActivated += ShowMultiplierUI;
        MultiplierCoin.OnMultiplierDeactivated += HideMultiplierUI;
    }

    private void OnDisable()
    {
        MultiplierCoin.OnMultiplierActivated -= ShowMultiplierUI;
        MultiplierCoin.OnMultiplierDeactivated -= HideMultiplierUI;
    }

    private void ShowMultiplierUI(float duration)
    {
        if (multiplierUI != null)
        {
            multiplierUI.SetActive(true);
            StartCoroutine(UpdateMultiplierUI(duration));
        }
    }

    private void HideMultiplierUI()
    {
        if (multiplierUI != null)
        {
            multiplierUI.SetActive(false);
        }
    }

    private IEnumerator UpdateMultiplierUI(float duration)
    {
        float remainingTime = duration;
        
        while (remainingTime > 0)
        {
            if (multiplierText != null)
                multiplierText.text = $"2x ({remainingTime:F1}s)";
            
            yield return new WaitForSeconds(0.1f);
            remainingTime -= 0.1f;
        }
    }
}
