using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class NatureGenerator : MonoBehaviour
{
    [SerializeField] private BlocksManager blocksManager;
    [SerializeField] private float chanceOfEmptyBlock = 0.4f;
    [SerializeField] private float chanceOfSpecificBlock = 0.3f;
    [SerializeField] private float chanceOfNatureBlock = 0.2f;

    private GameObject[] _blocks;
    
    private void Awake()
    {
        _blocks = blocksManager.GetBlocks();
    }

    public void NatureGeneration(Vector3 chunkSize, Map map)
    {
        // MapLoopGenerator(chunkSize, map);
        MapProceduralGenerator(chunkSize, map);
    }

    private void MapProceduralGenerator(Vector3 chunkSize, Map map)
    {
        for (int y = 0; y < chunkSize.y; y++)
        {
            for (int x = 0; x < chunkSize.x; x++)
            {
                for (int z = 0; z < chunkSize.z; z++)
                {
                    var terrainHeight = Mathf.FloorToInt(chunkSize.y * Noise.Get2DPerlin(new Vector2(x, z), (int) chunkSize.x, 800, 0.25f));
                    if (y == 0)
                    {
                        map.AddBlockInMap(_blocks, gameObject, 3, x, y, z);
                    }
                    else if (y <= terrainHeight)
                    {
                        map.AddBlockInMap(_blocks, gameObject, 0, x, y, z);
                    }
                    else
                    {
                        map.AddEmptyBlockInMap(x, y, z);
                    }

                    if (y > terrainHeight)
                    {
                        var spawnBlockChance = Random.Range(0.0f, 1.0f);
                        if (spawnBlockChance < chanceOfNatureBlock)
                        {
                            map.AddBlockInMap(_blocks, gameObject, 5, x, y, z);
                        }
                        else
                            map.AddEmptyBlockInMap(x, y, z);
                    }
                }
            }
        }
    }

    private void MapLoopGenerator(Vector3 chunkSize, Map map)
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
                            map.AddBlockInMap(_blocks, gameObject, 2, i, k, j);
                        }
                        else
                        {
                            map.AddBlockInMap(_blocks, gameObject, 3, i, k, j);
                        }
                    }
                    else if (k == 3)
                    {
                        map.AddBlockInMap(_blocks, gameObject, 0, i, k, j);
                    }
                    else if (k == 4)
                    {
                        spawnBlockChance = Random.Range(0.0f, 1.0f);
                        if (spawnBlockChance < chanceOfSpecificBlock)
                        {
                            map.AddBlockInMap(_blocks, gameObject, 4, i, k, j);
                        }
                        else
                        {
                            map.AddBlockInMap(_blocks, gameObject, 1, i, k, j);
                        }
                    }
                    else
                    {
                        spawnBlockChance = Random.Range(0.0f, 1.0f);
                        if (spawnBlockChance > chanceOfEmptyBlock)
                        {
                            var randomBlock = Random.Range(0, 2);

                            map.AddBlockInMap(_blocks, gameObject, randomBlock, i, k, j);
                        }
                        else
                        {
                            spawnBlockChance = Random.Range(0.0f, 1.0f);
                            if (spawnBlockChance < chanceOfNatureBlock)
                            {
                                map.AddBlockInMap(_blocks, gameObject, 5, i, k, j);
                            }
                            else
                                map.AddEmptyBlockInMap(i, k, j);
                        }
                    }
                }
            }
        }
    }
}
