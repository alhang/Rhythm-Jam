using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public Vector3 velocity;

    public Projectile(Vector2 vel, int dam)
    {
        velocity = new Vector3(vel.x, vel.y, 0);
        damage = dam;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }
}
