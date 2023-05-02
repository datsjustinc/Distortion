using UnityEngine;
using System.Collections.Generic;
using AI;

/// <summary>
/// This class handles the level difficulty of the game.
/// </summary>
public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<Transform> spawnPoints;
    
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
        // choose a random spawn point
        int spawnIndex = Random.Range(0, spawnPoints.Count);
        Transform spawnPoint = spawnPoints[spawnIndex];
        
        // spawn enemy prefabs and adjust stats to make them more agile and tanky
        var enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        var enemyScript = enemy.gameObject.GetComponent<Enemy>();
        enemyScript.MaxHealth += maxHealthIncrease;
        enemyScript.Agent.speed += speedIncrease;
        enemyScript.Agent.acceleration += accelerationIncrease;

        // Decrease spawn interval and increase enemy stats
        currentInterval = Mathf.Clamp(currentInterval - intervalDecrease, fastestInterval, slowestInterval);
    }
}