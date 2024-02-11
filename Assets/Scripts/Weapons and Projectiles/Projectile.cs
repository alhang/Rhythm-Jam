using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage;
    private Vector3 velocity;
    private float projectileSpeed;
    private Target target;

    public void Fire(Vector2 angle, Vector2 startingPos, Target target, float projectileSpeed, float damage)
    {
        this.damage = damage;
        this.projectileSpeed = projectileSpeed;
        this.target = target;
        transform.position = startingPos;
        velocity = Vector3.Normalize(angle) * projectileSpeed;
    }
    
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy) && target == Target.Enemy)
        {
            enemy.TakeDamage(damage);
            Despawn();
        }
        else if (collision.gameObject.TryGetComponent(out Player player) && target == Target.Player)
        {
            player.TakeDamage(damage);
            Despawn();
        }
        else if (collision.gameObject.TryGetComponent(out Boundary boundary))
        {
            Despawn();
        }
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}
