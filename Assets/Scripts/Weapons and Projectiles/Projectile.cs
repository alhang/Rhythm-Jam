using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public Vector3 velocity;
    public float projectileSpeed;

    public void Fire(Vector2 angle, Vector2 startingPos)
    {
        transform.position = startingPos;
        velocity = angle * projectileSpeed;
    }
    
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            // Take damage
            enemy.TakeDamage(damage);
            Despawn();
        }
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}
