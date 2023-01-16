using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksManager : MonoBehaviour
{
    [SerializeField] private GameObject[] blocks;

    public GameObject[] GetBlocks()
    {
        return blocks;
    }

}
