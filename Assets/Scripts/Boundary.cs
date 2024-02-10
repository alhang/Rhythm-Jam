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
    void OnCollisionEnter2D(Collision2D col) {
        Debug.Log("Hit boundary");
        if(col.gameObject.GetComponent<Projectile>() != null) {
            Destroy(col.gameObject);
        }
    }
}
