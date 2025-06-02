using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    private const int MAX_PLAYERS = 2;

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] playerPrefabs;

    private void Awake()
    {
        if (spawnPoints.Length < MAX_PLAYERS || playerPrefabs.Length < MAX_PLAYERS)
        {
            Debug.LogError("Not enough player spawn points or prefabs!");
            return;
        }

        for (int i = 0; i < MAX_PLAYERS; i++)
        {
            Instantiate(playerPrefabs[i], spawnPoints[i].position, spawnPoints[i].rotation);
        }
    }
}