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

    private bool canSetDamage = true;
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
        if (isDisabled || !canFire)
            return;

        if (target == Target.Enemy)
            direction = Player.mouseDirection;
        else
        {
            direction = Vector3.Normalize(Player.position - (Vector2)transform.position);
        }

        if(canSetDamage && transform.parent){
            transform.parent.TryGetComponent(out Enemy enemy);
            canSetDamage = false;
            if(enemy)
            {
                baseDamage = enemy.baseDamage;
            }
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