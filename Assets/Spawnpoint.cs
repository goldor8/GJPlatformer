using System;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    public GameObject flag;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.EnableSpawnpoint(this);
            flag.SetActive(true);
        }
    }
}
