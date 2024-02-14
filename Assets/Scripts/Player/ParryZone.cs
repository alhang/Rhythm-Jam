using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class ParryZone : MonoBehaviour
{
    private HashSet<Projectile> enemyProjectiles = new HashSet<Projectile>();
    private Sword enemySword;
    private Rigidbody2D rb;
    private float rotationAmount = 200f;
    private float timeElapsed;
    private float parryTime;

    // Referenced in Player.TryParry()
    public static bool failedParry = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Projectile projectile) && projectile.target == Target.Player && projectile.isParryable)
        {
            enemyProjectiles.Add(projectile);
        }
        
        if (collision.gameObject.transform.parent && collision.gameObject.transform.parent.TryGetComponent(out Sword sword) && sword.target == Target.Player)
        {
            enemySword = sword;
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
            transform.up = Quaternion.Euler(0, 0, rotationAmount/2) * transform.up;
        }
    }
    
    // OLD PARRY METHOD
    // public bool Parry()
    // {
    //     if (enemyProjectiles.Count > 0)
    //     {
    //         //Debug.Log("Parried");
    //         foreach (Projectile projectile in enemyProjectiles)
    //         {
    //             if (projectile != null)
    //             {
    //                 projectile.Fire(-projectile.velocity, Target.Enemy, 2 * projectile.damage);
    //                 projectile.GetComponent<SpriteRenderer>().color = Color.white;
    //             }
    //         }
    //         enemyProjectiles.Clear();
    //         return true;
    //     }
    //     else
    //         return false;
        
    // }

    public IEnumerator ParrySweep()
    {
        timeElapsed = 0;
        parryTime = 0.1f;
        float initialRotation = rb.rotation;
        float targetRotation = initialRotation - rotationAmount;

        failedParry = true;

        while (timeElapsed < parryTime)
        {
            if (enemyProjectiles.Count > 0)
            {
                //Debug.Log("Parried");
                foreach (Projectile projectile in enemyProjectiles)
                {
                    if (projectile != null)
                    {
                        projectile.Fire(-projectile.velocity, Target.Enemy, 2 * projectile.damage);
                        projectile.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }
                enemyProjectiles.Clear();
                failedParry = false;
            }

            if (enemySword != null){
                Debug.Log("Parried");
                Enemy enemy = enemySword.GetComponentInParent<Enemy>();
                StartCoroutine(enemy.Stun());

                enemySword = null;
                failedParry = false;
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

