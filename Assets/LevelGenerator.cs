using System;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class LevelGenerator : MonoBehaviour
{
    public List<LevelPart> levelParts;
    public int length = 10;

    public void Generate(int lenght)
    {
        float offset = 0;
        Random random = new Random((uint) DateTime.UtcNow.Second);
        
        for (int i = 0; i < lenght; i++)
        {
            int randomIndex = random.NextInt(0, levelParts.Count);
            LevelPart part = levelParts[randomIndex];
            offset += part.width/2;
            GameObject partInstance = Instantiate(part.gameObject, new Vector3(offset, 0, 0), Quaternion.identity);
            offset += part.width/2;
            
            // Set the parent of the instantiated part to the generator
            partInstance.transform.SetParent(transform);
        }
    }

    void Start()
    {
        Generate(length);
    }
}
