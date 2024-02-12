using UnityEngine;
using System.Collections.Generic;

public class ParryZone : MonoBehaviour
{
    private HashSet<Projectile> enemyProjectiles = new HashSet<Projectile>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Projectile projectile) && projectile.target == Target.Player)
        {
            enemyProjectiles.Add(projectile);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Projectile projectile) && projectile.target == Target.Player)
        {
            enemyProjectiles.Remove(projectile);
        }
    }

    private void Update()
    {
        transform.up = Player.mouseDirection;
    }

    public void Parry()
    {
        if (enemyProjectiles.Count > 0)
        {
            Debug.Log("Parried");
            foreach (Projectile projectile in enemyProjectiles)
            {
                if (projectile != null)
                {
                    projectile.Fire(-projectile.velocity, Target.Enemy, 2 * projectile.damage);
                    projectile.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            enemyProjectiles.Clear();
        }
        
    }
}

