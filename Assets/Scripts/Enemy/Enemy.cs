using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public float baseSpeed = 2;
    public float baseDamage = 1;

    public float maxHealth = 1;
    public float curHealth { get; private set; }

    public float baseRegen = 1;
    public float stunDuration = 2;
    private bool isStunned = false;

    public static event Action<Enemy> OnEnemyKill;

    public event Action OnTakeDamage;

    protected static HashSet<Enemy> allEnemies = new();
    public static bool roomIsClear => allEnemies.Count == 0;

    private SpriteRenderer spriteRenderer;
    private Color startingColor;

    public static bool isAggro;

    [SerializeField] EnemyMovementSO enemyMovement;
    [SerializeField] protected Weapon weapon;

    void Awake()
    {
        allEnemies.Add(this);

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (!spriteRenderer)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        curHealth = maxHealth;
        startingColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStunned && isAggro)
            Move();

        if (Player.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        curHealth -= damageAmount;
        OnTakeDamage?.Invoke();

        StartCoroutine(TakeDamageFeedback());

        if (curHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator TakeDamageFeedback()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = startingColor;
    }

    public virtual void Die()
    {
        OnEnemyKill?.Invoke(this);
        allEnemies.Remove(this);
        Destroy(gameObject);
    }

    // Move towards Player
    void Move()
    {
        if(enemyMovement)
            enemyMovement.Move(this, randomDirection);
    }

    // Enemy is stunned and cannot move or attack for fixed time
    public IEnumerator Stun()
    {
        isStunned = true;
        if(weapon){
            weapon.isDisabled = true;
        }
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
        if(weapon){
            weapon.isDisabled = false;
        }
    }

    public static void KillAll()
    {
        List<Enemy> enemyList = allEnemies.ToList();
        foreach(Enemy enemy in enemyList)
        {
            enemy.Die();
        }
    }

    public static void AggroAllEnemies(bool state)
    {
        Weapon.canFire = state;
        isAggro = state;
    }

    Vector3 randomDirection;
    public void ChangeRandomDirection()
    {
        randomDirection = new Vector3(UnityEngine.Random.Range(-1, 1f), UnityEngine.Random.Range(-1, 1f), 0);
    }
}
