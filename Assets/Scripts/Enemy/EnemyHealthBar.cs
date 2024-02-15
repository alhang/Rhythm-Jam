using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
	public Enemy enemy;

    public Image healthBarFill;

    private void Start()
    {
        healthBarFill.fillAmount = 1;
    }

    private void OnEnable()
    {
        enemy.OnTakeDamage += OnTakeDamage;
    }

    private void OnDisable()
    {
        enemy.OnTakeDamage -= OnTakeDamage;
    }

    private void OnTakeDamage()
    {
        healthBarFill.fillAmount = (float)(enemy.curHealth / enemy.maxHealth);
    }
}

