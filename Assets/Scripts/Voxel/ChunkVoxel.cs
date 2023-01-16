using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkVoxel : MonoBehaviour
{
    [SerializeField] private int chunkWidth = 5;
    [SerializeField] private int chunkHeight = 5;
    
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshFilter meshFilter;
    
    [SerializeField] private BlocksChunkManager blockTypes;
    
    public void CreateChunk()
    {
        var voxelGeneration = new VoxelGeneration(meshRenderer, meshFilter, blockTypes);
        voxelGeneration.PopulateVoxelMap(new Vector3Int(chunkWidth, chunkHeight, chunkWidth));
        for (int x = 0; x < chunkWidth; x++)
        {
            for (int z = 0; z < chunkWidth; z++)
            {
                for (int y = 0; y < chunkHeight; y++)
                {
                    voxelGeneration.CreateVoxelCube(new Vector3(x, y, z));
                }
            }
        }
    }
    void Start()
    {
        CreateChunk();
    }
}
