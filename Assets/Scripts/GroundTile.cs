using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundTile : MonoBehaviour
{
    private GroundSpawner _groundSpawner;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject coinPrefab;
    private void Start()
    {
        _groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
       // SpawnObstacle();
        //SpawnCoins();
    }

    private void OnTriggerExit(Collider other)
    {
        _groundSpawner.SpawnTile();
        Destroy(gameObject,2);
    }

    private void SpawnObstacle()
    {
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
    }

    private void SpawnCoins()
    {
        int coinsToSpawn = 10;
        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }

    private Vector3 GetRandomPointInCollider(Collider collider)
    {
        Vector3 point = new Vector3(Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z));
        if (point!=collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }

        point.y = 1;
        return point;
    }
    
    
}
