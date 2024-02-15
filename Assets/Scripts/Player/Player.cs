using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private BeatListener beatListener;

    // baseHealth is the current health TODO: maybe change baseHealth to currentHealth
    public static float curHealth = 10;
    // maxHealth is the maximum health possible
    public static float maxHealth = 10;
    public static Vector2 mouseDirection;
    public static Vector2 position;

    public KeyCode dashKey = KeyCode.Space;
    public float dashCooldown = 5;
    public static bool isDashOnCooldown;

    public float parryCooldown = 5;
    public static bool isParryOnCooldown;

    private ParryZone parryZone;
    [SerializeField] PlayerStatsSO playerStats;

    [SerializeField] PlayerHUD playerHUD;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        beatListener = GetComponent<BeatListener>();
        parryZone = GetComponentInChildren<ParryZone>();

        curHealth = maxHealth;
        playerHUD.UpdateHealthBar();

        StartCoroutine(RegenHealth());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isParryOnCooldown && Input.GetMouseButtonDown(0))
            TryParry();

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
            vertical += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            horizontal -= 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vertical -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            horizontal += 1;
        }

        rb.velocity = new Vector2(horizontal * playerStats.baseSpeed, vertical * playerStats.baseSpeed);
    }

    public void TakeDamage(float damageAmount)
    {
        curHealth -= damageAmount;
        // TODO: On Death action
        if (curHealth <= 0) 
        {
            curHealth = 0;
            OnDeath();
            // Debug.Log("You died");
        }

        playerHUD.UpdateHealthBar();
    }

    public void Heal(float healAmount)
    {
        curHealth += healAmount;

        if (curHealth >= maxHealth)
        {
            curHealth = maxHealth;
        }

        playerHUD.UpdateHealthBar();
    }

    // Checks if dash is on beat
    public void TryDash()
    {
        StartCoroutine(Dash());
        
        if ( !(beatListener.beatCount == 0 || beatListener.beatCount == 1))
        {
            Debug.Log(beatListener.beatCount);
            //Debug.Log("Dash is on cooldown");
            isDashOnCooldown = true;
            playerHUD.UpdateCooldowns();
            Timer timer = new Timer(dashCooldown, () => { isDashOnCooldown = false; playerHUD.UpdateCooldowns(); });
            StartCoroutine(timer.Start());
        }
    }

    // Need to add force over several frames otherwise doesn't work
    IEnumerator Dash()
    {
        float timeElapsed = 0;
        float dashTime = 0.1f;

        while (timeElapsed < dashTime) {
            rb.AddForce(Vector3.Normalize(mouseDirection) * playerStats.dashSpeed * Time.deltaTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    public void TryParry()
    {
        StartCoroutine(parryZone.ParrySweep());
        if (ParryZone.failedParry || !(beatListener.beatCount == 0 || beatListener.beatCount == 1))
        {
            //Debug.Log("Parry is on cooldown");
            isParryOnCooldown = true;
            playerHUD.UpdateCooldowns();
            Timer timer = new Timer(parryCooldown, () => { isParryOnCooldown = false; playerHUD.UpdateCooldowns(); });
            StartCoroutine(timer.Start());
        }
    }

    IEnumerator RegenHealth()
    {
        while(true)
        {
            Heal(playerStats.baseRegen);
            yield return new WaitForSeconds(playerStats.baseRegenRate);
        }
    }

    // TODO: Return to main menu on death and display some death screen
    void OnDeath()
    {

    }
}
