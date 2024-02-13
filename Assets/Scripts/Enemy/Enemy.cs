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

    private static HashSet<Enemy> allEnemies = new();

    private SpriteRenderer spriteRenderer;

    [SerializeField] EnemyMovementSO enemyMovement;

    void Awake()
    {
        allEnemies.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStunned)
            Move();
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
        spriteRenderer.color = Color.white;
    }

    public void Die()
    {
        OnEnemyKill?.Invoke(this);
        allEnemies.Remove(this);
        Destroy(gameObject);
    }

    // Move towards Player
    void Move()
    {
        enemyMovement.Move(this, randomDirection);
    }

    // Enemy is stunned and cannot move or attack for fixed time
    public IEnumerator Stun()
    {
        isStunned = true;
        Weapon weapon = GetComponentInChildren<Weapon>();
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

    Vector3 randomDirection;
    public void ChangeRandomDirection()
    {
        randomDirection = new Vector3(UnityEngine.Random.Range(-1, 1f), UnityEngine.Random.Range(-1, 1f), 0);
    }
}
