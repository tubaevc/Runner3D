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
    [SerializeField] private float laneDistance = 5.0f;
    [SerializeField] private float obstacleSpawnChance = 0.3f;
    [SerializeField] private float powerUpSpawnChance = 0.2f;
    [SerializeField] private int coinsToSpawn = 10;
    private List<Vector3> occupiedPositions = new List<Vector3>();
    private void Start()
    {
        _groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        SpawnObjects();
    }
    private void SpawnObjects()
    {
        for (int lane = 0; lane < 3; lane++)
        {
            float laneXPosition = (lane - 1) * laneDistance;
            Vector3 spawnPosition = new Vector3(laneXPosition, 0f, transform.position.z);

            if (Random.value < obstacleSpawnChance)
            {
                SpawnObstacle(spawnPosition);
            }
            else if (Random.value < powerUpSpawnChance)
            {
                SpawnPowerUp(spawnPosition);
            }
        }

        SpawnCoins();
    }

    private void OnTriggerExit(Collider other)
    {
        _groundSpawner.SpawnTile();
        Destroy(gameObject, 5);
    }

    private void SpawnObstacle(Vector3 position)
    {
        int randomPrefabIndex = Random.Range(0, obstaclePrefab.Length);
        GameObject selectedPrefab = obstaclePrefab[randomPrefabIndex];
        Instantiate(selectedPrefab, position, Quaternion.Euler(0, 90, 0), transform);
        occupiedPositions.Add(position);
    }

    private void SpawnPowerUp(Vector3 position)
    {
        int randomPrefabIndex = Random.Range(0, powerUpPrefabs.Length);
        GameObject selectedPrefab = powerUpPrefabs[randomPrefabIndex];
        Instantiate(selectedPrefab, position, Quaternion.identity, transform);
        occupiedPositions.Add(position);
    }

    private void SpawnCoins()
    {
        for (int i = 0; i < coinsToSpawn; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            if (spawnPosition != Vector3.zero)
            {
                GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity, transform);
                occupiedPositions.Add(spawnPosition);
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int maxAttempts = 10;
        for (int i = 0; i < maxAttempts; i++)
        {
            int randomLane = Random.Range(0, 3);
            float laneXPosition = (randomLane - 1) * laneDistance;
            Vector3 spawnPosition = GetRandomPointInCollider(GetComponent<Collider>());
            spawnPosition.x = laneXPosition;

            if (!IsPositionOccupied(spawnPosition))
            {
                return spawnPosition;
            }
        }
        return Vector3.zero;
    }

    private bool IsPositionOccupied(Vector3 position)
    {
        foreach (Vector3 occupiedPosition in occupiedPositions)
        {
            if (Vector3.Distance(position, occupiedPosition) < 1f)
            {
                return true;
            }
        }
        return false;
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