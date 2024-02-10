using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeapon : Weapon
{
    void Awake()
    {
        baseDamage = 1;
        baseFireRate = 1;

        projectile.velocity = new Vector3(0, 20, 0);
        projectile.damage = 1;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            attack();
        }
    }

    public override void attack()
    {
        Instantiate(projectile, transform.position, transform.rotation);
    }
}
