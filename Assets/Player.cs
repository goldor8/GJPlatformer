using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Spawnpoint currentSpawnpoint;
    public float deathHeight = -10f;
    public PlayerInput playerInput;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < deathHeight)
        {
            Kill();
        }
    }

    public void Kill()
    {
        OnDie();
    }

    private void OnDie()
    {
        if(currentSpawnpoint == null)
            transform.position = new Vector3(0, 0, 0);
        else
            transform.position = currentSpawnpoint.transform.position;
    }

    public void EnableSpawnpoint(Spawnpoint spawnpoint)
    {
        if(currentSpawnpoint == null)
            currentSpawnpoint = spawnpoint;
        else if(spawnpoint.transform.position.x > currentSpawnpoint.transform.position.x)
            currentSpawnpoint = spawnpoint;
    }
}
