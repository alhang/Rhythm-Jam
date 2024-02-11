using UnityEngine;
using System.Collections;

public class Pistol : Weapon
{
	public Projectile pistolBulletPrefab;

	public override void Attack()
	{
		Instantiate(pistolBulletPrefab).Fire(Player.mouseDirection, Player.position);
    }
}

