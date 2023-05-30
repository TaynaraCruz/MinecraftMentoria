using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private CombineMeshes combineMeshes;

    public void CreateChunk(Map map, Vector3 chunkSize, Vector2 chunkOffset, GameObject[] blocks)
    {
        StartCoroutine(GenerateChunk(map, chunkSize, chunkOffset, blocks));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            combineMeshes.Detach();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            combineMeshes.Combine();
        }
    }

    IEnumerator GenerateChunk(Map map, Vector3 chunkSize, Vector2 chunkOffset, GameObject[] blocks)
    {
        loadingScreen.SetActive(true);
        Time.timeScale = 0;
        InstantiateBlocksInChunk(map, chunkSize, chunkOffset, blocks);
        if (combineMeshes)
            combineMeshes.Combine();
        yield return new WaitForEndOfFrame(); 
        Time.timeScale = 1;
        loadingScreen.SetActive(false);
    }

    private void InstantiateBlocksInChunk(Map map, Vector3 chunkSize, Vector2 chunkOffset, GameObject[] blocks)
    {
        var offsetX = (int) chunkOffset.x;
        var offsetZ = (int) chunkOffset.y;
        
        for (int y = 0; y < chunkSize.y; y++)
        {
            for (int x = 0; x < chunkSize.x; x++)
            {
                for (int z = 0; z < chunkSize.z; z++)
                {
                    var blockType = map.GetBlock(x+offsetX, y, z+offsetZ);
                    var terrainHeight = map.GetTerrainHeight(new Vector3(x+offsetX, y, z+offsetZ));
                    if (blockType >= 0)
                    {
                        var obj = Instantiate(blocks[blockType], transform.TransformPoint(new Vector3(x, y, z)), Quaternion.identity, transform);
                        obj.layer = ShouldRenderInsideChunk(chunkSize, terrainHeight, x, y, z) ? 0 : 6;
                        // obj.layer = 0;
                    }
                }
            }
        }
    }
    
    private bool ShouldRenderInsideChunk(Vector3 worldSize, int terrainHeight, int posX, int posY, int posZ)
    {
        return posX == (int) worldSize.x - 1 || posY == (int) worldSize.y - 1 || posZ == (int) worldSize.z - 1
               || posX == 0 || posY == 0 || posZ == 0 || posY >= terrainHeight-1;
    }
}
