using UnityEngine;
using System.Collections;

public abstract class Drop : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            Pickup(player);
        }
    }

    protected abstract void Pickup(Player player);
}

