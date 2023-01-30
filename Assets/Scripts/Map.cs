using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Map
{
    private int _maxMapSize;
    private int[,,] _map;

    public Map(int maxMapSize = 100)
    {
        _maxMapSize = maxMapSize;
        _map = new int[_maxMapSize, 6, _maxMapSize];
    }

    public void SetBlock(int posX, int posY, int posZ, int blockType)
    {
        if (posX >= _maxMapSize || posY >= _maxMapSize || posZ >= _maxMapSize
            || posX < 0 || posY < 0 || posZ < 0)
        {
            Debug.LogError("Index out of limits to save in map");
            return;
        }
        _map[posX, posY, posZ] = blockType;
    }
    
    public int GetBlock(int posX, int posY, int posZ)
    {
        if (posX >= _maxMapSize || posY >= _maxMapSize || posZ >= _maxMapSize
            || posX < 0 || posY < 0 || posZ < 0)
        {
            Debug.LogError("Index out of limits to save in map");
            return -1;
        }
        return _map[posX, posY, posZ];
    }
    
    public void AddBlockInMap(GameObject[] blocks, GameObject parent, int blockType, int posX, int posY, int posZ)
    {
        if (blockType >= blocks.Length || blockType < 0)
        {
            Debug.LogError("Invalid Block");
            return;
        }
        GameObject.Instantiate(blocks[blockType], parent.transform.TransformPoint(new Vector3(posX, posY, posZ)),
            Quaternion.identity, parent.transform);
        SetBlock(posX, posY, posZ, blockType);

    }
    
    public void AddEmptyBlockInMap(int posX, int posY, int posZ)
    {
        SetBlock(posX, posY, posZ, -1);
    }
}
