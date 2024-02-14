using UnityEngine;
using System.Collections;

public class Pistol : Weapon
{
	public Projectile pistolBulletPrefab;

	public override void AttackHandler()
	{
		base.AttackHandler();
		Instantiate(pistolBulletPrefab).Fire(direction, transform.position, target, projectileSpeed, baseDamage);
    }
}

