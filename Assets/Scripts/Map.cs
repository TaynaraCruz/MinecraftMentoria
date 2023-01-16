using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Map
{
    private const int MaxMapSize = 100;

    private int[,,] _map;

    public Map()
    {
        _map = new int[MaxMapSize, MaxMapSize, MaxMapSize];
    }

    public void SetBlock(int posX, int posY, int posZ, int blockType)
    {
        if (posX >= MaxMapSize || posY >= MaxMapSize || posZ >= MaxMapSize
            || posX < 0 || posY < 0 || posZ < 0)
        {
            Debug.LogError("Index out of limits to save in map");
            return;
        }
        _map[posX, posY, posZ] = blockType;
    }
    
    public int GetBlock(int posX, int posY, int posZ)
    {
        if (posX >= MaxMapSize || posY >= MaxMapSize || posZ >= MaxMapSize
            || posX < 0 || posY < 0 || posZ < 0)
        {
            Debug.LogError("Index out of limits to save in map");
            return -1;
        }
        return _map[posX, posY, posZ];
    }
}
