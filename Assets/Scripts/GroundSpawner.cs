using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject groundTile;
    private Vector3 nextSpawnPoint;

    public void SpawnTile()
    {
       GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
       nextSpawnPoint = temp.transform.GetChild(1).transform.position;
    }
    private void Start()
    {
        SpawnTile();
        SpawnTile();
        SpawnTile();
        SpawnTile();
        SpawnTile();
        SpawnTile();
        SpawnTile();
    }
}
