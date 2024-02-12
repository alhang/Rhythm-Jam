using UnityEngine;
using System.Collections;

public class RoomEnter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {

        }
    }
}

