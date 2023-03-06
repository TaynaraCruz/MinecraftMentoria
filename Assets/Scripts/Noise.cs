using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float Get2DPerlin(Vector2 position, float scale)
    {
        var x = position.x * scale;
        var y = position.y * scale;

        return  Mathf.PerlinNoise(x,y);;
    }
}
