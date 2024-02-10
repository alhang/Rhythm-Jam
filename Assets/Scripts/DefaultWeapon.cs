using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeapon : Weapon
{
    // Use this for initialization
    void Start()
    {
        baseDamage = 1;
        baseFireRate = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void attack()
    {
        
    }
}
