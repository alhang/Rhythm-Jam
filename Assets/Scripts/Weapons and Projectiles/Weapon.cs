using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float baseDamage;
    public int rateOfFire;
    public int projectileSpeed;
    public Target target;
    protected Vector2 direction;

    private BeatListener beatListener;
    public bool isDisabled = false;

    public static bool canFire = false;

    private void Start()
    {
        beatListener = GetComponent<BeatListener>();

        if(transform.parent){
            transform.parent.TryGetComponent(out Enemy enemy);
            if(enemy)
            {
                baseDamage += enemy.baseDamage;
            }
        }
    }

    public void ChangeRateOfFire(int rateOfFire)
    {
        this.rateOfFire = rateOfFire;
        beatListener.beatInterval = rateOfFire;
    }

    public virtual void Attack()
    {
        if (isDisabled || !canFire)
            return;

        if (target == Target.Enemy)
            direction = Player.mouseDirection;
        else
        {
            direction = Vector3.Normalize(Player.position - (Vector2)transform.position);
        }

        AttackHandler();
    }

    public virtual void AttackHandler() { }
}

public enum Target
{
    Player,
    Enemy
}