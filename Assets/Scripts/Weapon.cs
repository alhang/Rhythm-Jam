using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int baseDamage;
    public float baseFireRate;
    public Projectile projectile;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnBeatHandler()
    {

    }

    public abstract void attack();
}
