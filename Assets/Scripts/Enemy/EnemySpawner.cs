using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemy;
    public float spawnRate = 4;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < spawnRate){
            timer += Time.deltaTime;
        }
        else{
            timer = 0;
            spawnEnemy();
        }
    }

    // TODO: Currently placeholder spawn position
    void spawnEnemy() {
        Instantiate(enemy, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
    } 
}