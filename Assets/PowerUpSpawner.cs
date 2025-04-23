using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    void Start()
    {
        Instantiate(GameManager.instance.GetRandomPowerUp(), transform.position, Quaternion.identity, transform.parent);
    }
}
