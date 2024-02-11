using UnityEngine;
using System.Collections;

public class Shotgun : Weapon
{
    public Projectile shotgunBulletPrefab;
    public float spread;
    public float numBullets;

    public override void Attack()
    {
        base.Attack();
        for (int i = 0; i < numBullets; i++)
        {
            float offset = (i - (numBullets / 2)) * spread;

            Vector3 rotation = Quaternion.Euler(0, 0, offset) * direction;

            Instantiate(shotgunBulletPrefab).Fire(rotation, transform.position, target, projectileSpeed, baseDamage);
        }
    }
}

