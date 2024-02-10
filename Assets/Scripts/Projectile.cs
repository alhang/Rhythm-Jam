using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public Vector3 velocity;

    void Awake()
    {
        gameObject.AddComponent<CircleCollider2D>();
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
