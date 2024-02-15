using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    private Rigidbody2D rb;
    private float rotationAmount = 120f;
    private float timeElapsed;
    private float attackTime;

    private BoxCollider2D swordCollider;
    [SerializeField] PlayerStatsSO playerStats;

    // Start is called before the first frame update
    void Start()
    {
        swordCollider = GetComponentInChildren<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player Sword
        if(target == Target.Enemy)
        {
            // Don't follow mouse while in attack motion
            if(timeElapsed >= attackTime){
                transform.up = Player.mouseDirection;
                transform.up = Quaternion.Euler(0, 0, 60) * transform.up;
            }
        }

        if(target == Target.Player)
        {
            if(timeElapsed >= attackTime){
                Vector2 position = transform.position;
                Vector3 direction = Vector3.Normalize(Player.position - position);
                transform.up = Quaternion.Euler(0, 0, 60) * direction;
            }
        }
        
    }

    public override void AttackHandler()
	{
		base.AttackHandler();
        if(!isDisabled)
            StartCoroutine(AttackSweep());
    }

    public IEnumerator AttackSweep()
    {
        timeElapsed = 0;
        attackTime = 0.2f;
        float initialRotation = rb.rotation;
        float targetRotation = initialRotation - rotationAmount;
        swordCollider.enabled = true;

        while (timeElapsed < attackTime)
        {
            // Rotate in attack motion
            timeElapsed += Time.deltaTime;
            float newRotation = Mathf.Lerp(initialRotation, targetRotation, timeElapsed / attackTime);
            rb.MoveRotation(newRotation);
            yield return null;
        }

        swordCollider.enabled = false;
        // Ensure the rotation is exactly as intended at the end
        rb.MoveRotation(targetRotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Don't trigger when not in attack motion
        if(timeElapsed >= attackTime){
            return;
        }

        if (collision.gameObject.TryGetComponent(out Enemy enemy) && target == Target.Enemy)
        {
            enemy.TakeDamage(baseDamage + playerStats.baseDamage);
        }
        else if (collision.gameObject.TryGetComponent(out Player player) && target == Target.Player)
        {
            player.TakeDamage(baseDamage);
            Debug.Log(collision.gameObject.name + " hit!");
        }
    }
}
