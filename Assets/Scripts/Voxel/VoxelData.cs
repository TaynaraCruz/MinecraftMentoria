using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VoxelData
{
    public const int NumberOfVertices = 8;
    public const int NumberOfFaces = 6;
    public const int NumberOfVerticesToDrawAVoxelFace = 6;
    
    public static readonly Vector3[] VoxelVertices = new Vector3[NumberOfVertices]
    {
        new Vector3(0.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 1.0f, 0.0f),
        new Vector3(0.0f, 1.0f, 0.0f),
        new Vector3(0.0f, 0.0f, 1.0f),
        new Vector3(1.0f, 0.0f, 1.0f),
        new Vector3(1.0f, 1.0f, 1.0f),
        new Vector3(0.0f, 1.0f, 1.0f)
    };
    
    public static readonly int[,] VoxelMeshTriangles = new int[NumberOfFaces, NumberOfVerticesToDrawAVoxelFace]
    {
        {5, 6, 4, 4, 6, 7}, //Back Face
        {0, 3, 1, 1, 3, 2}, //Front face
        {3, 7, 2, 2, 7, 6}, //Top Face
        {1, 5, 0, 0, 5, 4}, //Bottom Face
        {4, 7, 0, 0, 7, 3}, //Left Face
        {1, 2, 5, 5, 2, 6}  //Right Face
    };

    public static readonly Vector2[] VoxelUvs = new Vector2[NumberOfVerticesToDrawAVoxelFace]
    {
        new Vector2(0.0f, 0.0f),
        new Vector2(0.0f, 1.0f),
        new Vector2(1.0f, 0.0f),
        new Vector2(1.0f, 0.0f),
        new Vector2(0.0f, 1.0f),
        new Vector2(1.0f, 1.0f)
    };

    public static readonly Vector3[] VoxelFaceDirections = new Vector3[NumberOfFaces]
    {
        new Vector3(0.0f, 0.0f, 1.0f),  //Back Face
        new Vector3(0.0f, 0.0f, -1.0f), //Front Face
        new Vector3(0.0f, 1.0f, 0.0f),  //Top Face
        new Vector3(0.0f, -1.0f, 0.0f),  //Bottom Face
        new Vector3(-1.0f, 0.0f, 0.0f), //Left Face
        new Vector3(1.0f, 0.0f, 0.0f)   //Right Face
    };
}
