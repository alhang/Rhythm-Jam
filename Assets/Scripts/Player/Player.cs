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

    public static Vector3 mouseDirection;
    public static Vector3 position;

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

        // Finding the position of the mouse
        SetMouseDirection();

        position = transform.position;
        player.velocity = new Vector2(horizontal * baseSpeed, vertical * baseSpeed);
    }

    public void SetMouseDirection()
    {
        mouseDirection = Input.mousePosition;
        mouseDirection.z = 0.0f;
        mouseDirection = Camera.main.ScreenToWorldPoint(mouseDirection);
        mouseDirection = Vector3.Normalize(mouseDirection - transform.position);
    }

    public void OnBeatHandler()
    {

    }
}
