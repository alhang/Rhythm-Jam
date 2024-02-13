using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{
    public Enemy basicEnemy;
    public Enemy shotgunEnemy;
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
        Enemy enemy = Random.Range(0, 2) == 0 ? basicEnemy : shotgunEnemy;
        Instantiate(enemy, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
    }

    public void SetDifficulty(int difficulty)
    {
        spawnRate = 4 / (float)difficulty;
    }
}
