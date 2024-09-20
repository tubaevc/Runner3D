using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierCoin : MonoBehaviour
{
    [SerializeField] private float multiplierDuration = 10f;
    private Collider coinCollider;
    private MeshRenderer meshRenderer;
    
    // for 2x text
    public static event Action<float> OnMultiplierActivated;
    public static event Action OnMultiplierDeactivated;

    private void Start()
    {
        coinCollider = GetComponent<Collider>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            StartCoroutine(ActivateMultiplier());
            coinCollider.enabled = false;
            meshRenderer.enabled = false;
        }
    }

    private IEnumerator ActivateMultiplier()
    {
        GameManager.instance.SetScoreMultiplier(2);
        Debug.Log("multiplier activated 2x"); 
        OnMultiplierActivated?.Invoke(multiplierDuration);
        
        yield return new WaitForSeconds(multiplierDuration);

        GameManager.instance.SetScoreMultiplier(1);
        Debug.Log("multiplier deactivated 1x");
        OnMultiplierDeactivated?.Invoke();
        
        Destroy(gameObject);
    }
}