using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage { get; private set; }
    public Vector3 velocity { get; private set; }
    private float projectileSpeed;
    public Target target { get; private set; }
    public bool isParryable = true;

    public void Fire(Vector2 angle, Vector2 startingPos, Target target, float projectileSpeed, float damage)
    {
        this.damage = damage;
        this.projectileSpeed = projectileSpeed;
        this.target = target;
        transform.position = startingPos;
        velocity = Vector3.Normalize(angle) * projectileSpeed;
    }

    public void Fire(Vector2 velocity, Target target, float damage)
    {
        this.damage = damage;
        this.target = target;
        this.velocity = velocity;
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
