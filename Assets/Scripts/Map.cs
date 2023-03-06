using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Map
{
    private int _maxMapSize;
    private int[,,] _map;
    private int[,,] _terrainHeight;

    public Map(int maxMapSize = 100)
    {
        _maxMapSize = maxMapSize;
        _map = new int[_maxMapSize, _maxMapSize, _maxMapSize];
        _terrainHeight = new int[_maxMapSize, _maxMapSize, _maxMapSize];
    }

    public void SetBlock(int posX, int posY, int posZ, int blockType, int terrainHeight)
    {
        if (posX >= _maxMapSize || posY >= _maxMapSize || posZ >= _maxMapSize
            || posX < 0 || posY < 0 || posZ < 0)
        {
            Debug.LogError("Index out of limits to save in map");
            return;
        }
        try
        {
            _map[posX, posY, posZ] = blockType;
            _terrainHeight[posX, posY, posZ] = terrainHeight;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        
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
    
    public void AddBlockInMap(GameObject[] blocks, int blockType, int posX, int posY, int posZ, int terrainHeight)
    {
        if (blockType >= blocks.Length || blockType < 0)
        {
            Debug.LogError("Invalid Block");
            return;
        }
        SetBlock(posX, posY, posZ, blockType, terrainHeight);
    }
    
    public void AddEmptyBlockInMap(int posX, int posY, int posZ, int terrainHeight)
    {
        SetBlock(posX, posY, posZ, -1, terrainHeight);
    }

    public int GetTerrainHeight(Vector3 pos)
    {
        return _terrainHeight[(int) pos.x, (int) pos.y, (int) pos.z];
    }
}
