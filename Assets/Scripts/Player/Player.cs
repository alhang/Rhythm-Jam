using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public Rigidbody2D player;
    public float baseSpeed = 2;
    public float baseDamage = 1;
    public int baseHealth = 10;
    public float baseRegen = 1;

    public static Vector2 mouseDirection;
    public static Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: On Death action
        if(baseHealth <= 0) {
            Debug.Log("Player Died");
        }

        Move();

        // Finding the position of the mouse
        SetMouseDirection();
    }

    public void SetMouseDirection()
    {
        mouseDirection = Input.mousePosition;
        mouseDirection = Camera.main.ScreenToWorldPoint(mouseDirection);
        mouseDirection = mouseDirection - (Vector2)transform.position;
    }

    void Move()
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

        position = transform.position;
        player.velocity = new Vector2(horizontal * baseSpeed, vertical * baseSpeed);
    }

    public void TakeDamage(int damageAmount)
    {
        baseHealth -= damageAmount;
    }
}
