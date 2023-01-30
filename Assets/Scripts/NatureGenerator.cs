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
        for (int i = 0; i < chunkSize.x; i++)
        {
            for (int j = 0; j < chunkSize.z; j++)
            {
                for (int k = 0; k < chunkSize.y; k++)
                {
                    float spawnBlockChance = 0f;
                    float noise = Noise.Get2DPerlin(new Vector2(i, j), (int) chunkSize.x, 500, 0.25f);
                    int terrainHeight = Mathf.FloorToInt(chunkSize.x * noise);
                    
                    if (k == 0)
                    {
                        map.AddBlockInMap(_blocks,gameObject, 3, i, k, j);
                        
                    }
                    else if (k == 3)
                    {
                        map.AddBlockInMap(_blocks,gameObject, 0, i, k, j);
                    }
                    else if (k == 4)
                    {
                        spawnBlockChance = Random.Range(0.0f, 1.0f);
                        if (spawnBlockChance < chanceOfSpecificBlock)
                        {
                            map.AddBlockInMap(_blocks,gameObject, 4, i, k, j);
                        }
                        else
                        {
                            map.AddBlockInMap(_blocks,gameObject, 1, i, k, j);
                        }
                    }
                    else
                    {
                       
                        if (k == terrainHeight)
                        {
                            var randomBlock = Random.Range(0, 2);
                            map.AddBlockInMap(_blocks, gameObject, randomBlock, i, k, j);
                        }
                        else
                        {
                            spawnBlockChance = Random.Range(0.0f, 1.0f);
                            if (spawnBlockChance < chanceOfNatureBlock)
                            {
                                map.AddBlockInMap( _blocks,gameObject, 5, i, k, j);
                            }
                            else
                                map.AddEmptyBlockInMap(i, k, j);
                        }
                        // spawnBlockChance = Random.Range(0.0f, 1.0f);
                        // if (spawnBlockChance > chanceOfEmptyBlock)
                        // {
                        //     var randomBlock = Random.Range(0, 2);
                        //    
                        //     map.AddBlockInMap(_blocks, gameObject, randomBlock, i, k, j);
                        // }
                        // else
                        // {
                        //     spawnBlockChance = Random.Range(0.0f, 1.0f);
                        //     if (spawnBlockChance < chanceOfNatureBlock)
                        //     {
                        //         map.AddBlockInMap( _blocks,gameObject, 5, i, k, j);
                        //     }
                        //     else
                        //         map.AddEmptyBlockInMap(i, k, j);
                        // }
                    }
                }
            }
        }
    }
}
