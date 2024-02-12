using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class ParryZone : MonoBehaviour
{
    private HashSet<Projectile> enemyProjectiles = new HashSet<Projectile>();
    private Rigidbody2D rb;
    private float rotationAmount = -60f;
    private float timeElapsed;
    private float parryTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

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
        // Don't follow mouse while in parry motion
        if(timeElapsed >= parryTime){
            transform.up = Player.mouseDirection;
            transform.up = Quaternion.Euler(0, 0, -30) * transform.up;
        }
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

    public IEnumerator ParrySweep()
    {
        timeElapsed = 0;
        parryTime = 0.1f;
        float initialRotation = rb.rotation;
        float targetRotation = initialRotation - rotationAmount;

        while (timeElapsed < parryTime)
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

            // Rotate in parry motion
            timeElapsed += Time.deltaTime;
            float newRotation = Mathf.Lerp(initialRotation, targetRotation, timeElapsed / parryTime);
            rb.MoveRotation(newRotation);
            yield return null;
        }

        // Ensure the rotation is exactly as intended at the end
        rb.MoveRotation(targetRotation);
    }
}

