using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float Get2DPerlin(Vector2 position, float scale, float seed)
    {
        var x = position.x * scale + seed;
        var y = position.y * scale + seed;

        return  Mathf.PerlinNoise(x,y);
    }
}
