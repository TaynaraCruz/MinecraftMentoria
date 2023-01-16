using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelGeneration 
{
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    
    private int _verticeIndex = 0;
    private List<Vector3> _vertices = new List<Vector3>();
    private List<int> _triangles = new List<int>();
    private List<Vector2> _uv = new List<Vector2>();
    
    private byte [,,] _voxelMap;
    private Vector3Int _mapSize;
    
    private BlocksChunkManager _blockManager;
    public VoxelGeneration(MeshRenderer meshRenderer, MeshFilter meshFilter, BlocksChunkManager blockManager)
    {
        _meshRenderer = meshRenderer;
        _meshFilter = meshFilter;
        _blockManager = blockManager;
    }

    public void PopulateVoxelMap(Vector3Int mapSize)
    {
        _mapSize = mapSize;
        _voxelMap = new byte[_mapSize.x, _mapSize.y, _mapSize.z];
        
        for (int x = 0; x < _mapSize.x; x++)
        {
            for (int z = 0; z < _mapSize.z; z++)
            {
                for (int y = 0; y < _mapSize.y; y++)
                {
                    _voxelMap[x, y, z] = 2;
                }
            }
        }
    }
    
    private bool ShouldHideFaceFromPlayer(Vector3 facePosition)
    {
        var blocks = _blockManager.GetBlocks();
        var x = Mathf.FloorToInt(facePosition.x);
        var y = Mathf.FloorToInt(facePosition.y);
        var z = Mathf.FloorToInt(facePosition.z);

        if (x < 0 || x >= _mapSize.x || y < 0 || y >= _mapSize.y || z < 0 || z >= _mapSize.z)
        {
            return false;
        }
        return blocks[_voxelMap[x, y, z]].IsSolid;
    }

    public void CreateVoxelCube(Vector3 voxelPosition)
    {
        CreateVoxelMeshData(voxelPosition);
        CreateVoxelMesh();
    }

    private void CreateVoxelMeshData(Vector3 voxelPosition)
    {
        var blockID = _voxelMap[(int) voxelPosition.x, (int) voxelPosition.y, (int) voxelPosition.z];
        var blocks = _blockManager.GetBlocks();
        for (int face = 0; face < VoxelData.NumberOfFaces; face++)
        {
            if (!ShouldHideFaceFromPlayer(voxelPosition + VoxelData.VoxelFaceDirections[face]))
            {
                CreateFaceData(face, voxelPosition);
                _meshRenderer.material = blocks[blockID].Material;
            }
            
        }
    }
    private void CreateVoxelMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = _vertices.ToArray();
        mesh.triangles = _triangles.ToArray();
        mesh.uv = _uv.ToArray();
        mesh.RecalculateNormals();
        
        _meshFilter.mesh = mesh;
    }

    private void CreateFaceData(int face, Vector3 voxelPosition)
    {
        for (int i = 0; i < VoxelData.NumberOfVerticesToDrawAVoxelFace; i++)
        {
            int meshTriangleVerticeIndex = VoxelData.VoxelMeshTriangles[face, i];
            _vertices.Add(VoxelData.VoxelVertices[meshTriangleVerticeIndex] + voxelPosition);
            
            _triangles.Add(_verticeIndex);
            _verticeIndex++;
            
            _uv.Add(VoxelData.VoxelUvs[i]);
        }
    }
    
}