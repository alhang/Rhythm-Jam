using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BeatListener
{
    public float baseSpeed = 2;
    public float baseDamage = 1;
    public int baseHealth = 1;
    public float baseRegen = 1;

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
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // It is Projectile(Clone) because the proj spawned are named this way"
        if(col.gameObject.name == "Projectile(Clone)")
        {
            Debug.Log("Hit by projectile");
        }
    }
}
