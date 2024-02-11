using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float baseSpeed = 2;
    public float baseDamage = 1;
    public float baseHealth = 1;
    public float baseRegen = 1;
    public GameObject enemy;
    private BeatListener beatListener;
    public EnemyProjectile enemyBulletPrefab;

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
        beatListener = GetComponent<BeatListener>();
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy object on death
        if(baseHealth <= 0) {
            Destroy(enemy);
        }

        Move();
    }

    public void TakeDamage(float damageAmount)
    {
        baseHealth -= damageAmount;
    }

    // Move towards Player
    void Move()
    {
        Vector2 enemyPosition = transform.position;
        Vector3 direction = Vector3.Normalize(Player.position - enemyPosition);

        transform.position += direction * baseSpeed * Time.deltaTime;
    }

    // Fire projectile towards Player
    public void Attack()
    {
        /*
        Vector2 enemyPosition = transform.position;
        Vector3 direction = Vector3.Normalize(Player.position - enemyPosition);

        EnemyProjectile projectile = Instantiate(enemyBulletPrefab);
        projectile.damage = baseDamage;
        projectile.projectileSpeed = 30;

        projectile.Fire(direction, transform.position, Target.Player);

        Debug.Log("Enemy Atk");
        */
    }
}
