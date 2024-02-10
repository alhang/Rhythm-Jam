using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float baseSpeed = 2;
    public float baseDamage = 1;
    public int baseHealth = 1;
    public float baseRegen = 1;
    public GameObject enemy;

    void Awake()
    {
        gameObject.AddComponent<BoxCollider2D>();

        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy object on death
        if(baseHealth <= 0) {
            Destroy(enemy);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // It is Projectile(Clone) because the proj spawned are named this way"
        if(col.gameObject.GetComponent<Projectile>() != null)
        {
            // Take damage
            int projectileDamage = col.gameObject.GetComponent<Projectile>().damage;
            Debug.Log("Hit by projectile took " + projectileDamage + " damage");
            baseHealth -= projectileDamage;
        }
    }
}
