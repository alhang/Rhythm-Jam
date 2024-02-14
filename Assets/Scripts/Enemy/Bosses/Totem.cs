using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Totem : Enemy
{
    [SerializeField] float maxTime;
    [SerializeField] Image countDownFill;
    float elapsedTime = 0;

    private void Start()
    {
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
        weapon.Attack();
        Destroy(gameObject);
    }
}

