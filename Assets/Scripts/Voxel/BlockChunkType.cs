using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockChunkType
{
    [SerializeField] private string name;
    [SerializeField] private bool isSolid;
    [SerializeField] private Material material;
    
    public bool IsSolid
    {
        get => isSolid;
        set => isSolid = value;
    }
    public Material Material
    {
        get => material;
        set => material = value;
    }
}
