using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksChunkManager : MonoBehaviour
{
    [SerializeField] private BlockChunkType[] blocks;

    public BlockChunkType[] GetBlocks()
    {
        return blocks;
    }
}