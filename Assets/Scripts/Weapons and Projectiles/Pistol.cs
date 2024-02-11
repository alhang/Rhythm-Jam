using UnityEngine;
using System.Collections;

public class Pistol : Weapon
{
	public Projectile pistolBulletPrefab;

	public override void Attack()
	{
		base.Attack();
		Instantiate(pistolBulletPrefab).Fire(direction, transform.position, target, projectileSpeed, baseDamage);
    }
}

