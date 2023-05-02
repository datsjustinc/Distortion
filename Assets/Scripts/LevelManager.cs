using UnityEngine;
using System.Collections.Generic;
using AI;

/// <summary>
/// This class handles the level difficulty of the game.
/// </summary>
public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject healPrefab;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private Transform enemies;
    [SerializeField] private Transform heals;
    
    [Space]
    
    [Header("Spawn Intervals")]
    [SerializeField]private float slowestInterval = 5f;
    [SerializeField]private float fastestInterval = 1f;
    [SerializeField]private float intervalDecrease = 0.1f;
    [SerializeField]private float currentInterval;
    
    [Space]
    
    [Header("Stats Components")]
    [SerializeField] private float maxHealthIncrease = 200f;
    [SerializeField] private float speedIncrease = 0.3f;
    [SerializeField] private float accelerationIncrease = 0.3f;

    /// <summary>
    /// This function is called at start of game.
    /// </summary>
    private void Start()
    {
        // repeats method at a set rate
        currentInterval = slowestInterval;
        InvokeRepeating(nameof(SpawnEnemies), currentInterval, currentInterval);
    }

    /// <summary>
    /// This function spawns enemy prefabs and increases difficulty level of game.
    /// </summary>
    private void SpawnEnemies()
    {
        // choose a random spawn point for enemies
        var spawnIndex1 = Random.Range(0, spawnPoints.Count);
        var spawnPoint1 = spawnPoints[spawnIndex1];

        // choose a random spawn point for healing orbs
        var spawnIndex2 = Random.Range(0, spawnPoints.Count);
        var spawnPoint2 = spawnPoints[spawnIndex2];
        
        // spawn enemy prefabs and adjust stats to make them more agile and tanky
        var enemy = Instantiate(enemyPrefab, spawnPoint1.position, spawnPoint1.rotation, enemies);
        var enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.MaxHealth += maxHealthIncrease;
        enemyScript.Health = enemyScript.MaxHealth;
        enemyScript.Agent.speed += speedIncrease;
        enemyScript.Agent.acceleration += accelerationIncrease;

        var heal = Instantiate(healPrefab, spawnPoint2.position, spawnPoint2.rotation, heals);

        // Decrease spawn interval and increase enemy stats
        currentInterval = Mathf.Clamp(currentInterval - intervalDecrease, fastestInterval, slowestInterval);
    }
}