using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundTile : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefab;
    [SerializeField] private GameObject[] powerUpPrefabs;
    [SerializeField] private GameObject coinPrefab;
    private GroundSpawner _groundSpawner;
    [SerializeField] private float spawnChance = 0.3f;

    [SerializeField] private float laneDistance = 5.0f;

    private void Start()
    {
        _groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        if (Random.value < spawnChance)
        {
            SpawnObstacle();
        }

        SpawnCoins();
        SpawnPowerUp();
    }

    private void OnTriggerExit(Collider other)
    {
        _groundSpawner.SpawnTile();
        Destroy(gameObject, 5);
    }

    private void SpawnObstacle()
    {
        int randomPrefabIndex = Random.Range(0, obstaclePrefab.Length);
        GameObject selectedPrefab = obstaclePrefab[randomPrefabIndex];

        int randomLane = Random.Range(0, 3);
        float laneXPosition = (randomLane - 1) * laneDistance;

        Vector3 newSpawnPosition = new Vector3(laneXPosition, 0f, transform.position.z);

        Instantiate(selectedPrefab, newSpawnPosition, Quaternion.Euler(0, 90, 0), transform);
    }

    private void SpawnPowerUp()
    {
        int randomPrefabIndex = Random.Range(0, powerUpPrefabs.Length);
        GameObject selectedPrefab = powerUpPrefabs[randomPrefabIndex];

        int randomLane = Random.Range(0, 3);
        float laneXPosition = (randomLane - 1) * laneDistance;

        Vector3 newSpawnPosition = new Vector3(laneXPosition, 0f, transform.position.z);

        Instantiate(selectedPrefab, newSpawnPosition, Quaternion.identity, transform);
    }

    private void SpawnCoins()
    {
        int coinsToSpawn = 10;
        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab);

            int randomLane = Random.Range(0, 3);
            float laneXPosition = (randomLane - 1) * laneDistance;
            Vector3 spawnPosition = GetRandomPointInCollider(GetComponent<Collider>());
            spawnPosition.x = laneXPosition;

            temp.transform.position = spawnPosition;
        }
    }

    private Vector3 GetRandomPointInCollider(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
        );

        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }

        point.y = 1;
        return point;
    }
}