using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public LevelGenerator levelGenerator;
    public List<PowerUp> powerUps;
    private int[] levelParts;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        levelParts = levelGenerator.Generate(levelGenerator.length);
    }
    
    public PowerUp GetRandomPowerUp()
    {
        int randomIndex = UnityEngine.Random.Range(0, powerUps.Count);
        return powerUps[randomIndex];
    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        levelGenerator.SpawnLevel(Vector3.left + Vector3.up * 30 * playerInput.playerIndex, levelParts);
        Player player = playerInput.GetComponent<Player>();
        Transform playerContext = playerInput.transform.parent;
        playerContext.position += Vector3.up * 30 * playerInput.playerIndex;
        player.deathHeight += 30 * playerInput.playerIndex;
        player.baseSpawnpoint += Vector2.up * 30 * playerInput.playerIndex;
        
        if (PlayerInput.all.Count == 2)
        {
            PlayerInput.all[0].camera.rect = new Rect(0, 0, 1, 0.5f);
            PlayerInput.all[1].camera.rect = new Rect(0, 0.5f, 1, 0.5f);
        }
    }
    
    void OnPlayerLeft(PlayerInput playerInput)
    {
        
    }
}
