using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    public event Action <GameObject> OnEnemySpawn;
    [SerializeField] private GameObject[] enemyPrefabs;
    private GameObject enemySpawned;
    private float spawnDelay = 7.5f;
    private bool canSpawn = true;
    private bool maxEnemies = false;

    private void Awake(){
        foreach(EnemyRaycasted zombie in FindObjectsOfType<EnemyRaycasted>()){
            zombie.OnMaxEnemies += MaxEnemies;
        }
    }

    private void Update()
    {
        SpawnEnemy();
    }

    private void MaxEnemies(bool maxReached){
        maxEnemies = maxReached;
    }

    private void SpawnEnemy()
    {
        enemySpawned = enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)];
        if(canSpawn && !maxEnemies){
            StartCoroutine(SpawnEnemies(enemySpawned));
        }
    }

    IEnumerator SpawnEnemies(GameObject enemySpawned){
        canSpawn = false;
        OnEnemySpawn?.Invoke(enemySpawned);
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(enemySpawned, this.transform.position, this.transform.rotation);
        canSpawn = true;
    }
}
