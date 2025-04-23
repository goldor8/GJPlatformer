using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            OnPickup(player);
            Destroy(gameObject);
        }
    }
    
    protected virtual void OnPickup(Player player)
    {
        // Default behavior: do nothing
        Debug.Log("PowerUp picked up by " + player.name);
    }
}
