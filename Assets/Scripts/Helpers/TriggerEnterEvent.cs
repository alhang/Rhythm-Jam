using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class TriggerEnterEvent<T> : MonoBehaviour where T : MonoBehaviour
{
    public UnityEvent onTriggerEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out T detectable))
        {
            onTriggerEnter?.Invoke();
        }
    }
}