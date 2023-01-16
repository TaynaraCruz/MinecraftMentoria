using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] private BlocksManager blocksManager;
    [SerializeField] private NatureGenerator natureGenerator;
    [SerializeField] private Vector3 chunkSize;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private CombineMeshes combineMeshes;

    private GameObject[] _blocks;
    private Map _map;
    private float _spawnBlockChance;

    private void Awake()
    {
        _blocks = blocksManager.GetBlocks();
        if (SaveManager.HasSavedMap())
        {
            // StartCoroutine(LoadSavedMap());
            StartCoroutine(GenerateChunk());
        }
        else
        {
            StartCoroutine(GenerateChunk());
        }
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

    IEnumerator GenerateChunk()
    {
        loadingScreen.SetActive(true);
        Time.timeScale = 0;
        _map = new Map();
        natureGenerator.NatureGeneration(chunkSize);
        if (combineMeshes)
            combineMeshes.Combine();
        yield return new WaitForEndOfFrame();
        SaveManager.Save(_map);
        Time.timeScale = 1;
        loadingScreen.SetActive(false);

    }

    IEnumerator LoadSavedMap()
    {
        loadingScreen.SetActive(true);
        Time.timeScale = 0;
        _map = SaveManager.Load();
        if (_map != null)
        {
            for (int i = 0; i < chunkSize.x; i++)
            {
                for (int j = 0; j < chunkSize.z; j++)
                {
                    for (int k = 0; k < chunkSize.y; k++)
                    {
                        var blockIndex = _map.GetBlock(i, k, j);
                        if (blockIndex >= 0)
                            Instantiate(_blocks[blockIndex], new Vector3(i, k, j), Quaternion.identity,
                                gameObject.transform);
                    }
                }

                yield return new WaitForEndOfFrame();
            }
        }

        Time.timeScale = 1;
        loadingScreen.SetActive(false);
    }

    public void AddBlockInMap(int blockType, int posX, int posY, int posZ)
    {
        if (blockType >= _blocks.Length || blockType < 0)
        {
            Debug.LogError("Invalid Block");
            return;
        }

        Instantiate(_blocks[blockType], gameObject.transform.TransformPoint(new Vector3(posX, posY, posZ)),
            Quaternion.identity, gameObject.transform);
        _map.SetBlock(posX, posY, posZ, blockType);

    }
    
    public void AddEmptyBlockInMap(int posX, int posY, int posZ)
    {
       _map.SetBlock(posX, posY, posZ, -1);
    }
}
