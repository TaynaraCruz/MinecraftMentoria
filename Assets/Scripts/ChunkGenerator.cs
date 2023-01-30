using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] private NatureGenerator natureGenerator;
    [SerializeField] private Vector3 chunkSize;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private CombineMeshes combineMeshes;

    private float _spawnBlockChance;
    
    
    public void CreateChunk(Map map)
    {
        StartCoroutine(GenerateChunk(map));
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.gameObject.CompareTag("Player"))
        {
            combineMeshes.Detach();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");
        if (other.gameObject.CompareTag("Player"))
        {
            combineMeshes.Combine();
        }
    }

    IEnumerator GenerateChunk(Map map)
    {
        loadingScreen.SetActive(true);
        Time.timeScale = 0;
        natureGenerator.NatureGeneration(chunkSize, map);
        if (combineMeshes)
            combineMeshes.Combine();
        yield return new WaitForEndOfFrame();
        Time.timeScale = 1;
        loadingScreen.SetActive(false);
    }
}
