using System;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public Transform player;
    [Range(0f, 1f)]
    public float smooth = 0.5f;

    private void LateUpdate()
    {
        
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), smooth);
        // transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }
}
