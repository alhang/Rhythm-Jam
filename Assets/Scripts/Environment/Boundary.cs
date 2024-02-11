using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Deletes projectiles that hit boundary

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.gameObject.TryGetComponent(out Projectile projectile))
        {
            Debug.Log("Hit boundary");
            projectile.Despawn();
        }
        */
    }
}
