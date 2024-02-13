using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class TriggerExitEvent<T> : MonoBehaviour where T : MonoBehaviour
{
    public UnityEvent onTriggerExit;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out T detectable))
        {
            onTriggerExit?.Invoke();
        }
    }
}