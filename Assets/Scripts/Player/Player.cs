using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private BeatListener beatListener;
    public float baseSpeed = 2;
    public float dashSpeed = 3;
    public float baseDamage = 1;
    public float baseHealth = 10;
    public float baseRegen = 1;

    public static Vector2 mouseDirection;
    public static Vector2 position;

    public KeyCode dashKey = KeyCode.Space;

    public bool isDashOnCooldown;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        beatListener = GetComponent<BeatListener>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDashOnCooldown && Input.GetKeyDown(dashKey))
            TryDash();
        else
            Move();

        // Finding the position of the mouse
        SetMouseDirection();
        position = transform.position;
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

        rb.velocity = new Vector2(horizontal * baseSpeed, vertical * baseSpeed);
    }

    public void TakeDamage(float damageAmount)
    {
        baseHealth -= damageAmount;

        // TODO: On Death action
        if (baseHealth <= 0)
            Debug.Log("You died");
    }

    // Checks if dash is on beat
    public void TryDash()
    {
        Dash();
        if ( !((beatListener.beatCount == 0 && SongManager.timeSinceLastQuarterBeat < SongManager.quarterBeatInterval / 2) || (beatListener.beatCount == 3 && SongManager.timeSinceLastQuarterBeat > SongManager.quarterBeatInterval / 2)))
        {
            Debug.Log("Dash is on cooldown");
            isDashOnCooldown = true;
            SyncedTimer timer = new SyncedTimer(5, () => { isDashOnCooldown = false; Debug.Log("Dash is off cooldown"); });
            StartCoroutine(timer.Start());
        }
    }

    public void Dash()
    {
        Debug.Log("Dashed");
        StartCoroutine(DashCoroutine());
    }

    // Need to add force over several frames otherwise doesn't work
    IEnumerator DashCoroutine()
    {
        float timeElapsed = 0;
        float dashTime = 0.1f;

        while (timeElapsed < dashTime) {
            rb.AddForce(Vector3.Normalize(mouseDirection) * baseSpeed * dashSpeed * Time.deltaTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
