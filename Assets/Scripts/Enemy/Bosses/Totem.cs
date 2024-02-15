using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Totem : Enemy
{
    [SerializeField] float maxTime;
    [SerializeField] Image countDownFill;
    float elapsedTime = 0;
    public Transform parent { get; private set; }

    public GameObject healthPot;

    private void Start()
    {
        parent = transform.parent;
        StartCoroutine(CountDown());
    }

    public IEnumerator CountDown()
    {
        while (elapsedTime < maxTime)
        {
            float percentDone = (maxTime - elapsedTime) / maxTime;
            elapsedTime += Time.deltaTime;
            countDownFill.fillAmount = percentDone;
            countDownFill.color = Color.Lerp(Color.red, Color.green, percentDone);
            yield return null;
        }
        Explode();
    }

    public void Explode()
    {
        FirstBoss.avaliableSpawnPositions.Add(parent);
        weapon.Attack();
        Destroy(gameObject);
    }

    public override void Die()
    {
        FirstBoss.avaliableSpawnPositions.Add(parent);
        Instantiate(healthPot, transform.position, Quaternion.identity);
        base.Die();
    }
}

