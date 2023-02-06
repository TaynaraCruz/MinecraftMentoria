using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float Get2DPerlin(Vector2 position, int chunkSize, float offset, float scale)
    {
        var x = (position.x + 0.1f) / chunkSize * scale + offset;
        var y = (position.y + 0.1f) / chunkSize * scale + offset;
        
        return Mathf.PerlinNoise(x,y);
    }
}
