using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : BeatListener
{
    public int baseDamage;
    public float baseFireRate;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void attack();
}
