using System;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public Transform player;
    [Range(0f, 1f)]
    public float smooth = 0.5f;
    
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), ref velocity, smooth);
        // transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }
}
