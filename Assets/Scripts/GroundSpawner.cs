using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] groundTilePrefabs;
    private Vector3 nextSpawnPoint;

    public void SpawnTile()
    {
        int randomIndex = Random.Range(0, groundTilePrefabs.Length); // Rastgele prefab seçimi
        GameObject selectedTile = groundTilePrefabs[randomIndex]; // Rastgele seçilen prefab

        GameObject temp = Instantiate(selectedTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;
    }
    
    private void Start()
    {
        SpawnTile();
        SpawnTile();
        SpawnTile();
      
      
    }
}
