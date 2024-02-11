using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int baseDamage;
    public int rateOfFire;
    public int projectileSpeed;
    public Target target;
    protected Vector2 direction;

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

    public virtual void Attack()
    {
        if (target == Target.Enemy)
            direction = Player.mouseDirection;
        else
        {
            direction = Vector3.Normalize(Player.position - (Vector2)transform.position);
        }
    }
}

public enum Target
{
    Player,
    Enemy
}