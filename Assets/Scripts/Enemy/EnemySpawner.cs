using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{
    public List<EnemySpawnrate> enemyPrefabs;

    public float difficulty;

    public RectTransform spawnArea;
    public float maxX, minX, maxY, minY;

    private void Start()
    {
        maxX = spawnArea.rect.xMax;
        minX = spawnArea.rect.xMin;
        maxY = spawnArea.rect.yMax;
        minY = spawnArea.rect.yMin;
    }

    public int PrepopulateRoom(int difficulty)
    {
        int enemiesSpawned = 0;

        this.difficulty = difficulty;
        List<EnemySpawnrate> enemySpawnrates = new List<EnemySpawnrate>(enemyPrefabs);
        while(difficulty > 0)
        {
            int randomIndex = Random.Range(0, enemySpawnrates.Count);
            EnemySpawnrate randomEnemy = enemySpawnrates[randomIndex];
            
            if(randomEnemy.difficultyRating > difficulty)
            {
                enemySpawnrates.RemoveAt(randomIndex);
            }
            else
            {
                difficulty -= randomEnemy.difficultyRating;
                SpawnEnemy(randomEnemy.prefab);
                enemiesSpawned++;
            }
        }
        return enemiesSpawned;
    }

    // TODO: Currently placeholder spawn position
    private void SpawnEnemy(Enemy prefab) {

        Instantiate(prefab, GetRandomSpawnPos(), Quaternion.identity, transform);
    }

    private Vector2 GetRandomSpawnPos()
    {
        float randX = Random.Range(minX, maxX);
        float randY = Random.Range(minY, maxY);
        return new Vector2(randX, randY);
    }
}

[System.Serializable]
public class EnemySpawnrate
{
    public Enemy prefab;
    public int difficultyRating;
}
