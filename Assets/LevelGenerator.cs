using System;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class LevelGenerator : MonoBehaviour
{
    public List<LevelPart> levelParts;
    public int length = 10;

    public int[] Generate(int lenght)
    {
        Random random = new Random((uint) DateTime.UtcNow.Second);
        int[] levelPartIndexes = new int[lenght];
        
        for (int i = 0; i < lenght; i++)
        {
            int randomIndex = random.NextInt(0, levelParts.Count);
            levelPartIndexes[i] = randomIndex;
        }

        return levelPartIndexes;
    }

    void SpawnLevel(Vector3 offset, int[] parts)
    {
        float hOffset = 0;
        for (int i = 0; i < parts.Length; i++)
        {
            LevelPart part = levelParts[parts[i]];
            hOffset += part.width/2;
            GameObject partInstance = Instantiate(part.gameObject, new Vector3(hOffset, 0, 0) + offset, Quaternion.identity);
            hOffset += part.width/2;
            
            // Set the parent of the instantiated part to the generator
            partInstance.transform.SetParent(transform);
        }
    }

    void Start()
    {
        SpawnLevel(Vector3.left, Generate(length));
    }
}
