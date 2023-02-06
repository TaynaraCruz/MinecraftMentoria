using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//map 6.000 x 40 x 6.000
public class WorldMapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Vector3 mapSize;
    [SerializeField] private int mapMaxSize;
    [SerializeField] private int chunkSize;
    [SerializeField] private BlocksManager blocksManager;
    [SerializeField] private GameObject chunkPrefab;

    private GameObject[] _blocks;
    private Map _map;
    private void Awake()
    {
        _map = new Map(mapMaxSize);
        _blocks = blocksManager.GetBlocks();
        if (SaveManager.HasSavedMap())
        {
            // StartCoroutine(LoadSavedMap());
            StartCoroutine(CreateWorldMap());
        }
    }
    IEnumerator LoadSavedMap()
    {
        loadingScreen.SetActive(true);
        Time.timeScale = 0;
        _map = SaveManager.Load();
        if (_map != null)
        {
            for (int i = 0; i < mapSize.x; i++)
            {
                for (int j = 0; j < mapSize.z; j++)
                {
                    for (int k = 0; k < mapSize.y; k++)
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

    IEnumerator CreateWorldMap()
    {
        for (int i = 0; i < mapSize.x; i += chunkSize)
        {
            for (int j = 0; j < mapSize.z; j += chunkSize)
            {
                var chunkObj = Instantiate(chunkPrefab, new Vector3(i, 0, j), Quaternion.identity, gameObject.transform);
                var generationObj = chunkObj.GetComponent<ChunkGenerator>();
                generationObj.CreateChunk(_map);
            }
            yield return new WaitForEndOfFrame();
        }
        Time.timeScale = 1;
        loadingScreen.SetActive(false);
        SaveManager.Save(_map);
    }
    
}
