using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    public Rigidbody2D player;
    public float baseSpeed = 2;
    public float baseDamage = 1;
    public int baseHealth = 10;
    public float baseRegen = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = 0;
        float vertical = 0;

        // Player Movement
        if (Input.GetKey(KeyCode.W))
        {
            vertical += baseSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            horizontal -= baseSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vertical -= baseSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            horizontal += baseSpeed;
        }

        player.velocity = new Vector2(horizontal * baseSpeed, vertical * baseSpeed);
    }

    public void OnBeatHandler()
    {

    }
}
