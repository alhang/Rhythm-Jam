using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int baseDamage;
    public int rateOfFire;

    private BeatListener beatListener;

    private void Start()
    {
        beatListener = GetComponent<BeatListener>();
    }

    public void ChangeRateOfFire(int rateOfFire)
    {
        this.rateOfFire = rateOfFire;
        beatListener.beatInterval = rateOfFire;
    }

    public abstract void Attack();
}
