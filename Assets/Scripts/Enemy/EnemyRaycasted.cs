using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyRaycasted : MonoBehaviour
{
    public event Action <bool> OnMaxEnemies;
    [SerializeField] private GameObject arrow;
    Dictionary<int, GameObject> enemies = new Dictionary<int, GameObject>();

    private int maxEnemies = 20;

    private void Start(){
        FindObjectOfType<PlayerRaycast>().OnEnemyRaycasted += Arrow;
        FindObjectOfType<ZombieHit>().OnEnemyKilled += Deactivate;
        foreach(EnemySpawner spawner in FindObjectsOfType<EnemySpawner>()){
            spawner.OnEnemySpawn += EnemyGenerated;
        }
        foreach(ZombieHit zombie in FindObjectsOfType<ZombieHit>()){
            zombie.OnEnemyKilled += EnemyKilled;
        }
    }

    private void EnemyGenerated(GameObject enemyGenerated){
        if(enemies.Count >= maxEnemies){
            OnMaxEnemies?.Invoke(true);
        }
        if(!enemies.ContainsKey(enemyGenerated.GetInstanceID())){
            enemies.Add(enemyGenerated.GetInstanceID(), enemyGenerated);
        }

        enemyGenerated.GetComponent<ZombieHit>().OnEnemyKilled += EnemyKilled;
    }

    private void EnemyKilled(GameObject enemyKilled){
        if(enemies.Count >= maxEnemies){
            OnMaxEnemies?.Invoke(false);
        }
        if(enemies.ContainsKey(enemyKilled.GetInstanceID())){
            enemies.Remove(enemyKilled.GetInstanceID());
        }

        enemyKilled.GetComponent<ZombieHit>().OnEnemyKilled -= EnemyKilled;
    }

    private void Arrow(bool state, GameObject zombie){
        if(this.gameObject == zombie){
            arrow.SetActive(state);
        }
    }

    private void Deactivate(GameObject zombieGameObject){
        if(zombieGameObject == this.gameObject){
            FindObjectOfType<PlayerRaycast>().OnEnemyRaycasted -= Arrow;
            FindObjectOfType<ZombieHit>().OnEnemyKilled -= Deactivate;
            foreach(EnemySpawner spawner in FindObjectsOfType<EnemySpawner>()){
                spawner.OnEnemySpawn -= EnemyGenerated;
            }
            foreach(ZombieHit zombie in FindObjectsOfType<ZombieHit>()){
                zombie.OnEnemyKilled -= EnemyKilled;
            }
        }
    }
}
