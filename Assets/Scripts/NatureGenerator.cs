using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class NatureGenerator : MonoBehaviour
{
    [SerializeField] private float chanceOfEmptyBlock = 0.4f;
    [SerializeField] private float chanceOfSpecificBlock = 0.3f;
    [SerializeField] private float chanceOfNatureBlock = 0.2f;
    [SerializeField] private ChunkGenerator chunkGenerator;
    
    public void NatureGeneration(Vector3 chunkSize)
    {
        for (int i = 0; i < chunkSize.x; i++)
        {
            for (int j = 0; j < chunkSize.z; j++)
            {
                for (int k = 0; k < chunkSize.y; k++)
                {
                    float spawnBlockChance = 0f;

                    if (k >= 0 && k <= 2)
                    {
                        spawnBlockChance = Random.Range(0.0f, 1.0f);
                        if (spawnBlockChance < chanceOfSpecificBlock)
                        {
                            chunkGenerator.AddBlockInMap(2, i, k, j);
                        }
                        else
                        {
                            chunkGenerator.AddBlockInMap(3, i, k, j);
                        }
                    }
                    else if (k == 3)
                    {
                        chunkGenerator.AddBlockInMap(0, i, k, j);
                    }
                    else if (k == 4)
                    {
                        spawnBlockChance = Random.Range(0.0f, 1.0f);
                        if (spawnBlockChance < chanceOfSpecificBlock)
                        {
                            chunkGenerator.AddBlockInMap(4, i, k, j);
                        }
                        else
                        {
                            chunkGenerator.AddBlockInMap(1, i, k, j);
                        }
                    }
                    else
                    {
                        spawnBlockChance = Random.Range(0.0f, 1.0f);
                        if (spawnBlockChance > chanceOfEmptyBlock)
                        {
                            var randomBlock = Random.Range(0, 2);
                            chunkGenerator.AddBlockInMap( randomBlock, i, k, j);
                        }
                        else
                        {
                            spawnBlockChance = Random.Range(0.0f, 1.0f);
                            if (spawnBlockChance < chanceOfNatureBlock)
                            {
                                chunkGenerator.AddBlockInMap( 5, i, k, j);
                            }
                            else
                                chunkGenerator.AddEmptyBlockInMap(i, k, j);
                        }
                    }
                }
            }
        }
    }
}
