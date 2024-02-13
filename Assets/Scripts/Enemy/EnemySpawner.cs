using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{
    public Enemy[] enemyPrefabs;
    public float spawnRate = 4;
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        if(timer < spawnRate){
            timer += Time.deltaTime;
        }
        else{
            timer = 0;
            SpawnEnemy();
        }
    }

    // TODO: Currently placeholder spawn position
    private void SpawnEnemy() {
        spawnRate -= spawnRate >= 0.3f ? 0.1f : 0;
        Enemy enemy = enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)];
        Instantiate(enemy, transform);
    }

    public void SetDifficulty(int difficulty)
    {
        spawnRate = 4 / (float)difficulty;
    }
}
