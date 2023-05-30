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
    [SerializeField] private float seed = 1.0f;

    
    public void NatureGeneration(Vector3 worldSize, Map map,  GameObject[] blocks)
    {
        MapProceduralGenerator(worldSize, map, blocks);
    }

    private void MapProceduralGenerator(Vector3 worldSize, Map map, GameObject[] blocks)
    {
        for (int x = 0; x < (int)worldSize.x; x++)
        {
            for (int z = 0; z <(int)worldSize.z; z++)
            {
                for (int y = 0; y < (int)worldSize.y; y++)
                {
                    var terrainHeight = Mathf.FloorToInt(worldSize.y * Noise.Get2DPerlin(transform.TransformPoint(new Vector2(x, z)), 0.05f, seed));
                    if (y == 0)
                    {
                        map.AddBlockInMap(blocks, 3, x, y, z, terrainHeight);
                    }
                    else if (y < terrainHeight || y == 1)
                    {
                        map.AddBlockInMap(blocks, 0, x, y, z, terrainHeight);
                    }
                    else if (y == terrainHeight)
                    {
                        var spawnBlockChance = Random.Range(0.0f, 1.0f);
                        if (spawnBlockChance < chanceOfNatureBlock)
                        {
                            map.AddBlockInMap(blocks, 5, x, y, z, terrainHeight);
                        }
                        else
                            map.AddBlockInMap(blocks, 0, x, y, z, terrainHeight);
                    }
                    else
                    {
                        map.AddEmptyBlockInMap(x, y, z, terrainHeight);
                    }
                }
            }
        }
    }
}
